﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    // Use this for initialization
    [Tooltip("How fast the object fade")] [SerializeField] public float fadeSpeed = 2.5f;
    public bool fadable = false;

    private void Start()
    {
        Color setupColor = gameObject.GetComponent<Renderer>().sharedMaterial.color;
        setupColor.a=1.0f;
    }
    private void Update()
    {
        gameObject.isStatic = false;
        fade();
    }
    private void OnCollisionEnter(Collision collision)
    {
        fadable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        fadable = true;
    }
    /// <summary>
    /// a fade function will fade a object, use message call from outside
    /// </summary>
    public void fade()
    {   
        if(fadable){
            //print("Player"+gameObject.GetComponent<Renderer>().material.color);
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
                gameObject.GetComponent<Renderer>().material.color=new Color(
                gameObject.GetComponent<Renderer>().material.color.r,
                gameObject.GetComponent<Renderer>().material.color.g,
                gameObject.GetComponent<Renderer>().material.color.b,
                1.0f
                );
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// fade function used in edit mode
    /// </summary>
    public void EditFade()
    {
        if (fadable&&Application.isEditor)
        {
            print("Edit Mode!!!!"+gameObject.GetComponent<Renderer>().sharedMaterial.color);
            gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color(
            gameObject.GetComponent<Renderer>().sharedMaterial.color.r,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.g,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.b
                , Mathf.Clamp(gameObject.GetComponent<Renderer>().sharedMaterial.color.a -
                (fadeSpeed * Time.deltaTime), 0.00f, 1.0f));

            if (gameObject.GetComponent<SpriteRenderer>() != null)
                gameObject.GetComponent<SpriteRenderer>().color = new Color(
                gameObject.GetComponent<Renderer>().sharedMaterial.color.r,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.g,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.b
                , Mathf.Clamp(gameObject.GetComponent<Renderer>().sharedMaterial.color.a -
                (10 * fadeSpeed * Time.deltaTime), 0.00f, 1.0f));
            if(gameObject.GetComponent<Renderer>().sharedMaterial.color.a==0)
            {
                gameObject.GetComponent<Renderer>().sharedMaterial.color=new Color(
                gameObject.GetComponent<Renderer>().sharedMaterial.color.r,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.g,
                gameObject.GetComponent<Renderer>().sharedMaterial.color.b,
                1.0f
                );
                DestroyImmediate(gameObject);
            }
        }
    }
}

