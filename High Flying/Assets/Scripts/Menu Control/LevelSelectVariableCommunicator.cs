using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectVariableCommunicator : MonoBehaviour {



	// Use this for initialization
	void Start () {
        Slider theDifficultySlider = FindObjectOfType<Slider>();

        sendNewDifficultyRating(theDifficultySlider.value);
	}
	
	
	public void sendNewDifficultyRating (float value)
    {
        VariableContainer thePasser = FindObjectOfType<VariableContainer>();
        int roundedValue = Mathf.RoundToInt(value);
        thePasser.difficultyRating = roundedValue;
	}
}
