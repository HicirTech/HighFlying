using InterSceneCommunication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class allows the difficulty rating slider to communicate with the VariableContainer
public class LevelSelectVariableCommunicator : MonoBehaviour {



	// Use this for initialization
	void Start () {

        //grab the slider from the scene 
        Slider theDifficultySlider = FindObjectOfType<Slider>();

        //send the initial value to the VariableContainer (in case the user dosn't change it before starting a level) 
        sendNewDifficultyRating(theDifficultySlider.value);
	}
	
	//Called whenever the user changes the position of the slider. Also called once in the Start method to send the initial value 
	public void sendNewDifficultyRating (float value)
    {
        //grab the VariableContainer from the scene 
        VariableContainer thePasser = FindObjectOfType<VariableContainer>();
        //get the integer value of the value passed in from the slider 
        int roundedValue = Mathf.RoundToInt(value);
        // send the integer value to the VariableContainer
        thePasser.difficultyRating = roundedValue;
	}
}
