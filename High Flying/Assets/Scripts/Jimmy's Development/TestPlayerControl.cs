using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControl : MonoBehaviour {

    Rigidbody rigidbodyPlayer;
	// Use this for initialization
	void Start () {
        rigidbodyPlayer = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbodyPlayer.AddRelativeForce(Vector3.forward);
            print("Jump. Space pressed");
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("A, turn left");
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("D, turn right");
        }
    }
}
