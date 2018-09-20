using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class JumpButton : MonoBehaviour {

	[Tooltip("Insert the character here")][SerializeField]
	Transform target;
	[SerializeField]
	Direction dir;

	void OnMouseDown()
	{
		Debug.Log("Mouse down event");
		Vector3 pos = target.position;

		switch(dir)
		{
			case Direction.DOWN:
				Debug.Log("Down");
				pos += Vector3.down;
				break;
			case Direction.UP:
				Debug.Log("Up");
				pos += Vector3.up;
				break;
			default:
				Debug.Log("Failed");
				pos = Vector3.zero;
				break;
		}

		target.position = pos;
	}
	
}

public enum Direction
{
	UP,
	DOWN
}
