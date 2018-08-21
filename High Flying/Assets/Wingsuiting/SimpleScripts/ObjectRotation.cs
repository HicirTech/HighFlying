using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
	[SerializeField] private float speed;

	void Update ()
	{
		transform.Rotate (speed * Vector3.up * Time.deltaTime);
	}
}
