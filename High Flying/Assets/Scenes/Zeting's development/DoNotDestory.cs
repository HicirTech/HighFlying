using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestory : MonoBehaviour {
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
