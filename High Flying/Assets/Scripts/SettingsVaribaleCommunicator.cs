using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterSceneCommunication;
using UnityEngine.UI;

public class SettingsVaribaleCommunicator : MonoBehaviour {

    [SerializeField]
    private Button theAcclerometerButton;
    private VariableContainer thePasser;

    enum AccelerometerStatus {Unavailable, Enabled, Disabled}

	// Use this for initialization
	void Start () {

        thePasser = FindObjectOfType<VariableContainer>();

        if(checkIfAcclerometerSupported())
        {
            if(thePasser.isAccelerometerEnabled)
            {
                changeButtonDisplay(AccelerometerStatus.Enabled);
            }
            else
            {
                changeButtonDisplay(AccelerometerStatus.Disabled);
            }
        }
        else
        {
            changeButtonDisplay(AccelerometerStatus.Unavailable);
        }



    }
	
	// Update is called once per frame
	void Update () {
		
	}

     private bool checkIfAcclerometerSupported()
    {
        bool isSupported = false;

        if (SystemInfo.supportsGyroscope)
        {
            isSupported = true;
        }

        return isSupported;

    }

    private void changeButtonDisplay(AccelerometerStatus status)
    {
        Text theButtonText = theAcclerometerButton.GetComponentInChildren<Text>();

        if (status == AccelerometerStatus.Unavailable)
        {
            theButtonText.text = "Sorry, your device does not support acclerometer control";
            theAcclerometerButton.interactable = false;
        }
        else if (status == AccelerometerStatus.Disabled)
        {
            theButtonText.text = "Acclerometer Control is currently diabled, click to enable";
        }
        else if (status == AccelerometerStatus.Enabled)
        {
            theButtonText.text = "Acclerometer Control is currently enabled, click to disable";
        }
    }

    public void reactToClick()
    {
        if(thePasser.isAccelerometerEnabled)
        {
            changeButtonDisplay(AccelerometerStatus.Disabled);
            thePasser.isAccelerometerEnabled = false;
        }
        else
        {
            changeButtonDisplay(AccelerometerStatus.Enabled);
            thePasser.isAccelerometerEnabled = true;
        }   
    }

}
