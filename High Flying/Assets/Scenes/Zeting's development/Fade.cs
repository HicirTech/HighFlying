﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    // Use this for initialization
    [Tooltip("How fast the object fade")] [SerializeField] private float fadeSpeed = 2.5f;
    private bool fadable = false;
    private void Update()
    {
        gameObject.isStatic = false;
        fade();
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("C");
        fadable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("T");
        fadable = true;
    }
    /// <summary>
    /// a fade function will fade a object, use message call from outside
    /// </summary>
    public void fade()
    {
        if (fadable)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(
               gameObject.GetComponent<Renderer>().material.color.r,
                gameObject.GetComponent<Renderer>().material.color.g,
                 gameObject.GetComponent<Renderer>().material.color.b
                 , Mathf.Clamp(gameObject.GetComponent<Renderer>().material.color.a -
                  (fadeSpeed * Time.deltaTime), 0.00f, 1.0f));

            if (gameObject.GetComponent<SpriteRenderer>() != null)
                gameObject.GetComponent<SpriteRenderer>().color = new Color(
                gameObject.GetComponent<Renderer>().material.color.r,
                 gameObject.GetComponent<Renderer>().material.color.g,
                  gameObject.GetComponent<Renderer>().material.color.b
                  , Mathf.Clamp(gameObject.GetComponent<Renderer>().material.color.a -
                   (10 * fadeSpeed * Time.deltaTime), 0.00f, 1.0f));
            if(gameObject.GetComponent<Renderer>().material.color.a==0)
            {
                Destroy(gameObject);
            }
        }
    }
}

