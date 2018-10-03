using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	// Use this for initialization
   [Tooltip("How fast the object fade")] [SerializeField] private float fadeSpeed = 2.5f;
    private bool fadable = false;
    
    
    private void Update()
    {
        this.fade();
    }
    private void OnCollisionEnter(Collision e)
    {
        this.fadable=true;
    }

    private void OnTriggerEnter(Collider e)
    {
        this.fadable=true;
        print("T");
    }
    
    /// <summary>
    /// a fade function will fade a object
    /// </summary>
    private void fade()
    {
        if(fadable)
        {gameObject.GetComponent<Renderer>().material.color= new Color(
            gameObject.GetComponent<Renderer>().material.color.r,
             gameObject.GetComponent<Renderer>().material.color.g,
              gameObject.GetComponent<Renderer>().material.color.b
              , Mathf.Clamp(gameObject.GetComponent<Renderer>().material.color.a -
               (fadeSpeed* Time.deltaTime),0.00f,1.0f));
            print("fade");}
       
    }
}
