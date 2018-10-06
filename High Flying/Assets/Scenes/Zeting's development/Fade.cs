using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	// Use this for initialization
   [Tooltip("How fast the object fade")] [SerializeField] private float fadeSpeed = 2.5f;
    private bool fadable = false;
    private Color color;
    private void OnCollisionEnter(Collision e)
    {
        this.fadable=true;
    }

    private void OnTriggerEnter(Collider e)
    {
        this.fadable=true;
        print("T");
    }
    private void Update()
    {
        this.fade();
    }
    
    /// <summary>
    /// a fade function will fade a object
    /// </summary>
    private void fade()
    {
        color = gameObject.GetComponent<Renderer>().material.color;
        if(fadable){
            color = new Color(color.r,color.g,color.b, Mathf.Clamp(color.a - (fadeSpeed* Time.deltaTime),0.00f,1.0f));
        }
    }

}
