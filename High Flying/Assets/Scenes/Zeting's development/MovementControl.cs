using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovementControl : MonoBehaviour {

    [Tooltip("Move speed of the character to move, unit: M/s")] [SerializeField] 
	private float moveSpeed = 8; 
	[Tooltip("Enable this checkbox, will enable pitch system in the object")][SerializeField] 
	private bool PitchEnable = false;
	[Tooltip("Enable this checkbox, will enable Yaw system in the object")][SerializeField] 
	private bool YawEnable = false;
	[Tooltip("Enable this checkbox, will enable Roll system in the object")][SerializeField] 
	private bool RollEnable = false;
	[Tooltip("This value will be the max distantce of the model move from original location in x axis")][SerializeField] 
	private float MaxXMovement = 3.23f;
	[Tooltip("This value will be the max distantce of the model move from original location in Y axis")][SerializeField] 
	private float MaxYMovement = 2.5f;
    [Tooltip("How much character can pitch dur to its position changes")][SerializeField] 
	private float positionPitchFactor = -5f; 
    [Tooltip("How much character can pitch dur to player's control")][SerializeField] 
	private float controlPitchFactor = -20f; 
	[Tooltip("How much chararacter can yaw dur to its left and right movement")][SerializeField] 
	private float positionYawFactor = 5f; 
   	[Tooltip("How much chararacter can roll dur to its left and right movement")][SerializeField] 
	private float controlRollFactor = -30f; //how much chararacter can roll
    [Tooltip("How high chararacter can jump ")][SerializeField]
    private float force = 3f; //how high chararacter can jump
    bool jump = false;
    private float xThrow;
	private float yThrow;
    private Rigidbody rigidbodyPlayer;
    
	// Use this for initialization
	void Start () {
		this.setupControl();
        rigidbodyPlayer = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		Move();// move the character left right up down
        rotate(); //  rotate the chatacter whey moving
    }


    /// <summary>
    /// This method will check the Checkbox of yaw, pitch, roll
    /// </summary>
    private void setupControl()
	{
		this.positionPitchFactor=(this.PitchEnable)?this.positionPitchFactor:0;
		this.controlPitchFactor=(this.PitchEnable)?this.controlPitchFactor:0;
		this.controlRollFactor=(this.RollEnable)?this.controlRollFactor:0;
		this.positionYawFactor=(this.YawEnable)?this.positionYawFactor:0;
	}

	/// <summary>
	/// this method will will control yaw, pitch, and roll, combine them to make the rotation system
	/// </summary>
	private void rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;//different position when you move in Y asix

        float pitchDueToControlThrow = yThrow * controlPitchFactor; // y Throw is the joysick (if keyboard is a line) how far from the origin
                                                                    //  point in y asix

        float pitch = (pitchDueToPosition + pitchDueToControlThrow)/1.5f;// pitch can be calculate, 1.5 is changable, higher value more rotate
        
        float yaw = transform.localPosition.x * positionYawFactor;//different yaw when you move in x asix
        float roll = xThrow * controlRollFactor;// roll with your joysick

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);// apply the change to che character
    }

	/// <summary>
	/// this method will get input from CrossPlatformInputManager to move the attached object
	/// </summary>
    private void Move()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//get data from CrossPlatformInputManager
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
       
        float xOffSet = xThrow * moveSpeed * Time.deltaTime;
        float yOffSet = yThrow * moveSpeed * Time.deltaTime;
		
        float rowY = Mathf.Clamp(transform.localPosition.y + yOffSet, -MaxYMovement, MaxYMovement);//limited the x y way can go
        float rowX = Mathf.Clamp(transform.localPosition.x + xOffSet, -MaxXMovement, MaxXMovement);//limited the x y way can go
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            print("jump");
            rowY = Mathf.Clamp(transform.localPosition.y + yOffSet*force, -MaxYMovement, MaxYMovement);
        }

        transform.localPosition = new Vector3(rowX, rowY, transform.localPosition.z);
    }

}
