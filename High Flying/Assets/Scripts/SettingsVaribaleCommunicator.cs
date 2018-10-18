using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterSceneCommunication;
using UnityEngine.UI;

//This class allows the difficulty acclorometer button to communicate with the VariableContainer
public class SettingsVaribaleCommunicator : MonoBehaviour {

    [SerializeField]
    private Button theAcclerometerButton; //this will be used to store a refernce to the botton in the scene 
    private VariableContainer thePasser; //this will be used to store a reference to the VariableContainer in the scene 

    enum AccelerometerStatus {Unavailable, Enabled, Disabled } //The potential Accelerometer states

    // Unavailable is used when the system does not support Accelerometer control
    // Enabled is used when the system supports Accelerometer control and the user has opted to enable it 
   // Disabled is used when the system supports Accelerometer control and the user has opted to disable it 

    // Use this for initialization
    void Start () {

        //Grab the VariableContainer from the scene 
        thePasser = FindObjectOfType<VariableContainer>();

        //check if the system supports Accelerometer control
        if (SystemInfo.supportsGyroscope)
        {
            //update the text in the button based on that state of the Accelerometer control stored in the VariableContainer
            if (thePasser.isAccelerometerEnabled)
            {

                //chnage the value of the button accordingly based on the value in the VariableContainer
                changeButtonDisplay(AccelerometerStatus.Enabled);
            }
            else
            {
                changeButtonDisplay(AccelerometerStatus.Disabled);
            }
        }
        else
        {
            //if the system does not support Accelerometer controller update the button accordingly 
            changeButtonDisplay(AccelerometerStatus.Unavailable);
        }



    }

    private void changeButtonDisplay(AccelerometerStatus status)
    {
        //get the text from the button 
        Text theButtonText = theAcclerometerButton.GetComponentInChildren<Text>();

        if (status == AccelerometerStatus.Unavailable)
        {
            //tell the user if their device does not support acclerometer control
            theButtonText.text = "Sorry, your device does not support acclerometer control";
            theAcclerometerButton.interactable = false;
        }
        else if (status == AccelerometerStatus.Disabled)
        {
            //if acclerometer control is disabled, tell the user 
            theButtonText.text = "Acclerometer Control is currently diabled, click to enable";
        }
        else if (status == AccelerometerStatus.Enabled)
        {
            // if acclerometer control is enabled, tell the user 
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
