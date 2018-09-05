using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]

public class BackgroundMove : MonoBehaviour {
	[SerializeField] Vector3 moveVector = new Vector3(15f,0f,0f);
	float moveFactor;
	[SerializeField] float peroid =2f;
	// Use this for initialization
	Vector3 startPos;
	void Start () {
		startPos = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		float cycle = Time.time/(peroid+1);
		const float tau = Mathf.PI*2f;
		float sinWave=Mathf.Sin(cycle*tau);
		moveFactor = sinWave;
		print(sinWave);
			Vector3 offset = moveVector*moveFactor;
			transform.position = startPos+offset;
	}
}
