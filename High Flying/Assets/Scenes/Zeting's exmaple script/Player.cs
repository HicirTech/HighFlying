using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

    // Use this for initialization
    [Tooltip("M/s")] [SerializeField] float moveSpeed = 8;
    [SerializeField] float positionPitchFactor = -5f; 
    [SerializeField] float controlPitchFactor = -20f; 
    [SerializeField] float positionYawFactor = 5f; 
    [SerializeField] float controlRollFactor = -30f; 
   
    
    
    
    float xThrow;
    float yThrow;
    //Rigidbody rigidbody;
    void Start()
    {
        // this.rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //fun();
        rotate();
    }
    private void rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = (pitchDueToPosition + pitchDueToControlThrow)/1.5f;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Move()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
       
        float xOffSet = xThrow * moveSpeed * Time.deltaTime;
        float yOffSet = yThrow * moveSpeed * Time.deltaTime;
        float rowY = Mathf.Clamp(transform.localPosition.y + yOffSet, -2.5f, +2.5f);
        float rowX = Mathf.Clamp(transform.localPosition.x + xOffSet, -3.23f, +3.23f);
        transform.localPosition = new Vector3(rowX, rowY, transform.localPosition.z);
    }



}


