using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattleController : MonoBehaviour {

    private GameObject character;
    private MovementControl movementControl;

    #region UI_MAIN

    private void Start()
    {
        var character = GameObject.FindGameObjectWithTag("Player");
        if (character)
        {
            // get component MovementControl in character
            movementControl = character.GetComponentInChildren<MovementControl>();
        }
        else
        {
            throw new System.Exception("Can't find 'character' in your scene");
        }
    }
}
