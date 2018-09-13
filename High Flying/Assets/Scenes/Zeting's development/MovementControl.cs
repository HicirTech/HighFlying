using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovementControl : MonoBehaviour
{

    [Tooltip("Move speed of the character to move, unit: M/s")]
    [SerializeField]
    private float moveSpeed = 8;
    [Tooltip("Enable this checkbox, will enable pitch system in the object")]
    [SerializeField]
    private bool PitchEnable = false;
    [Tooltip("Enable this checkbox, will enable Yaw system in the object")]
    [SerializeField]
    private bool YawEnable = false;
    [Tooltip("Enable this checkbox, will enable Roll system in the object")]
    [SerializeField]
    private bool RollEnable = false;
    [Tooltip("This value will be the max distantce of the model move from original location in x axis")]
    [SerializeField]
    private float MaxXMovement = 3.23f;
    [Tooltip("This value will be the max distantce of the model move from original location in Y axis")]
    [SerializeField]
    private float MaxYMovement = 2.5f;
    [Tooltip("How much character can pitch dur to its position changes")]
    [SerializeField]
    private float positionPitchFactor = -5f;
    [Tooltip("How much character can pitch dur to player's control")]
    [SerializeField]
    private float controlPitchFactor = -20f;
    [Tooltip("How much chararacter can yaw dur to its left and right movement")]
    [SerializeField]
    private float positionYawFactor = 5f;
    [Tooltip("How much chararacter can roll dur to its left and right movement")]
    [SerializeField]
    private float controlRollFactor = -30f; //how much chararacter can roll

    //---------Zeting: Question  this is how fast can jump or how high it can jump?
    [Tooltip("How fast chararacter can jump ")]
    [SerializeField]
    private float force = 100f; //how fast chararacter can jump
    [Tooltip("How high chararacter can jump ")]
    [SerializeField]
    private float MaxJumpHeight = 2; //how height chararacter can jump

    [Tooltip("Enable this checkbox, will enable jump system in the object")]
    [SerializeField]
    private bool enableJump = false;
    public bool EnableJump { get { return enableJump; } }

    private bool isJumping = false; // the spaceship is in jump state or not
    private float maxCurrentJumpHeight; // depend on the current position of spaceship
    private float xThrow;
    private float yThrow;
    private Rigidbody rigidbodyPlayer;

    // Use this for initialization
    void Start()
    {
        this.setupControl();
        rigidbodyPlayer = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();// move the character left right up down
        rotate(); //  rotate the chatacter whey moving
    }

    /// <summary>
    /// This method will check the Checkbox of yaw, pitch, roll
    /// </summary>
    private void setupControl()
    {
        this.positionPitchFactor = (this.PitchEnable) ? this.positionPitchFactor : 0;
        this.controlPitchFactor = (this.PitchEnable) ? this.controlPitchFactor : 0;
        this.controlRollFactor = (this.RollEnable) ? this.controlRollFactor : 0;
        this.positionYawFactor = (this.YawEnable) ? this.positionYawFactor : 0;
    }

    /// <summary>
    /// this method will will control yaw, pitch, and roll, combine them to make the rotation system
    /// </summary>
    private void rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;//different position when you move in Y asix

        float pitchDueToControlThrow = yThrow * controlPitchFactor; // y Throw is the joysick (if keyboard is a line) how far from the origin
                                                                    //  point in y asix

        float pitch = (pitchDueToPosition + pitchDueToControlThrow) / 1.5f;// pitch can be calculate, 1.5 is changable, higher value more rotate

        float yaw = transform.localPosition.x * positionYawFactor;//different yaw when you move in x asix
        float roll = xThrow * controlRollFactor;// roll with your joysick

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);// apply the change to che character
    }

    /// <summary>
    /// this method will get input from CrossPlatformInputManager to move the attached object
    /// </summary>
    private void Move()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            Jump();
        }

        if (!isJumping)
        {
            NormalMove();
        }
    }

    private void UpdatePosition(float xOffSet, float yOffSet)
    {
        float rowY = Mathf.Clamp(transform.localPosition.y + yOffSet, -MaxYMovement, MaxYMovement);//limited the x y way can go
        float rowX = Mathf.Clamp(transform.localPosition.x + xOffSet, -MaxXMovement, MaxXMovement);//limited the x y way can go

        transform.localPosition = new Vector3(rowX, rowY, transform.localPosition.z);
    }

    public void NormalMove()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//get data from CrossPlatformInputManager
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffSet = xThrow * moveSpeed * Time.deltaTime;
        float yOffSet = yThrow * moveSpeed * Time.deltaTime;
        UpdatePosition(xOffSet, yOffSet);
    }

    public void Jump()
    {
        if (enableJump && isJumping == false)
        {
            Debug.Log("Jump");
            isJumping = true;
            StartCoroutine("IJump");
        }
    }

    /// <summary>
    /// IE will run ultil it get the target height. Max jump or maxY
    /// </summary>
    /// <returns></returns>
    private IEnumerator IJump()
    {
        // recalculate the highest from current position of space ship
        maxCurrentJumpHeight = Mathf.Min(MaxJumpHeight + transform.localPosition.y, MaxYMovement);

        while (true) // loop each frame, out of Fixed Update
        {
            // check if current position is out of valid area
            if (transform.localPosition.y >= maxCurrentJumpHeight)
            {
                UpdatePosition(transform.localPosition.x, transform.localPosition.y);
                StopAllCoroutines();
                isJumping = false;

            }

            // move ship if isJumping ultil equal true
            if (isJumping)
                transform.Translate(Vector3.up * force * Time.smoothDeltaTime);
            // wait ultil end of frame
            yield return new WaitForEndOfFrame();
        }
    }
}
