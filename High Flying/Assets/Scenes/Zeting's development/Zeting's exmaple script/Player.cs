using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

    // Use this for initialization
    [Tooltip("M/s")] [SerializeField] float moveSpeed = 8; // move speed of the character to move
    [SerializeField] float positionPitchFactor = -5f; // how much character can pitch dur by its position 
    [SerializeField] float controlPitchFactor = -20f; // how much character can pitch dur by control 
    [SerializeField] float positionYawFactor = 5f; // how much chararacter can yaw
    [SerializeField] float controlRollFactor = -30f; //how much chararacter can roll
   
    

    float xThrow;
    float yThrow;
  
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();// move the character left right up down
      
        rotate(); //  rotate the chatacter whey moving
    }
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

    private void Move()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//get data from CrossPlatformInputManager
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
       
        float xOffSet = xThrow * moveSpeed * Time.deltaTime;
        float yOffSet = yThrow * moveSpeed * Time.deltaTime;

        float rowY = Mathf.Clamp(transform.localPosition.y + yOffSet, -2.5f, +2.5f);//limited the x y way can go
        float rowX = Mathf.Clamp(transform.localPosition.x + xOffSet, -3.23f, +3.23f);//limited the x y way can go
        transform.localPosition = new Vector3(rowX, rowY, transform.localPosition.z);
    }



}


