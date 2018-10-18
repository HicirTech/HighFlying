using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingDestruction : MonoBehaviour {
    bool isTrigger = false;

    void OnTriggerEnter(Collider col)
    {
        if (isTrigger)
            return;
        if (col.CompareTag("Character") )
        {
            transform.DOScale(1, 1).OnComplete(()=> {
                Destroy(gameObject, 0.1f);
            });
        }    
    }
}
