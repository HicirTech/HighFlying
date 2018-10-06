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
    private float MaxXRightMovement = 3.23f, MaxXLeftMovement = -3.23f; //this one can make the character fly horizontally further
    [Tooltip("This value will be the max distantce of the model move from original location in Y axis")]
    [SerializeField]
    private float MaxYTopMovement = 2.5f, MaxYBottomMovement = -5f;
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
    private float force = 10f; //how fast chararacter can jump
    [Tooltip("How high chararacter can jump ")]
    [SerializeField]
    private float MaxJumpHeight = 2; //how height chararacter can jump

    [Tooltip("Enable this checkbox, will enable jump system in the object")]
    [SerializeField]
    private bool enableJump = false; // if this set to false. jump button wont disappear but will not work
    public bool EnableJump { get { return enableJump; } }

    private bool isJumping = false; // the character is in jump state or not
    private float maxCurrentJumpHeight; // depend on the current position of character
    private float xThrow;
    private float yThrow;

    private bool isValidUpdatePosition = true;

    private CharacterMovement characterMovement;
    private HitBuildingHandler hitBuildingHandler;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        this.SetupControl();
        this.InitCharacterMovement();
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
    private void SetupControl()
    {
        this.positionPitchFactor = (this.PitchEnable) ? this.positionPitchFactor : 0;
        this.controlPitchFactor = (this.PitchEnable) ? this.controlPitchFactor : 0;
        this.controlRollFactor = (this.RollEnable) ? this.controlRollFactor : 0;
        this.positionYawFactor = (this.YawEnable) ? this.positionYawFactor : 0;
    }

    private void InitCharacterMovement()
    {
        characterMovement = new CharacterMovement(transform);
        characterMovement.SetMaxMovement(MaxXRightMovement, MaxXLeftMovement, MaxYTopMovement, MaxYBottomMovement);

        hitBuildingHandler = GetComponentInParent<HitBuildingHandler>();
        Debug.AssertFormat(hitBuildingHandler != null, "hitBuildingHandler cant be null");
        hitBuildingHandler.onLevelComplete += () =>
        {
            moveSpeed = 0;
            enableJump = false;
        };
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

    public void NormalMove()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//get data from CrossPlatformInputManager
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffSet = xThrow * moveSpeed * Time.deltaTime;
        float yOffSet = yThrow * moveSpeed * Time.deltaTime;
        characterMovement.UpdatePosition(xOffSet, yOffSet, ref isValidUpdatePosition);
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
        // recalculate the highest from current position of character
        maxCurrentJumpHeight = Mathf.Min(MaxJumpHeight + transform.localPosition.y, MaxYTopMovement);
        bool isFloatBack = false;
        while (isJumping) // loop each frame, out of Fixed Update
        {
            
            // move character if isJumping ultil equal true
                characterMovement.UpdatePosition(0, force * Time.deltaTime, ref isValidUpdatePosition);
                isJumping = isValidUpdatePosition;
                // check if current position is out of valid area
                if (transform.localPosition.y >= maxCurrentJumpHeight)
                {
                    isJumping = false;
                characterMovement.UpdatePosition(0, maxCurrentJumpHeight - transform.localPosition.y, ref isValidUpdatePosition);
                }
            // wait ultil end of frame
            yield return new WaitForEndOfFrame();
        }
        isFloatBack = true;
        float minFloatBack = Mathf.Max(-MaxJumpHeight + transform.localPosition.y, MaxYBottomMovement);
        while (isFloatBack)
        {
            // move character if isJumping ultil equal true
            characterMovement.UpdatePosition(0, force * Time.deltaTime, ref isValidUpdatePosition);
            isFloatBack = isValidUpdatePosition;
            // check if current position is out of valid area
            if (transform.localPosition.y >= maxCurrentJumpHeight)
            {
                isFloatBack = false;
                characterMovement.UpdatePosition(0, minFloatBack - transform.localPosition.y, ref isValidUpdatePosition);
            }
            // wait ultil end of frame
            yield return new WaitForEndOfFrame();
        }
    }
}
