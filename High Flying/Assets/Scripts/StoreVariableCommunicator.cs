using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterSceneCommunication;

public class StoreVariableCommunicator : MonoBehaviour {

    private VariableContainer thePasser = null;
    private ColorControl theController = null;
    private Dropdown thePartSelector = null;

    private enum SuitPart {MainBody, ArmFlaps, BodyLines, HelmetAccents, Helmet, AnklesWrists};

    // Use this for initialization
    void Start ()
    {
        //get the objects that are required for this script to run from the scene
        thePasser = VariableContainer.FindObjectOfType<VariableContainer>();
        theController = ColorControl.FindObjectOfType<ColorControl>();
        thePartSelector = Dropdown.FindObjectOfType<Dropdown>();

        if (thePasser == null)
        {
            //if the VariableContainer could not be found then report this in the debugger
            Debug.Log("VariableContainer could not be located");
        }
        else
        {
            //otherwise syncronise the container and the character preview 
            sendColourSelections();
        }
		
	}
	
	public void sendColourSelections()
    {
        //syncronise the container and the character preview 
        theController.anklesWrists = thePasser.anklesWrists;
        theController.armFlaps = thePasser.armFlaps;
        theController.bodyLines = thePasser.bodyLines;
        theController.helmet = thePasser.helmet;
        theController.helmetAccents = thePasser.helmetAccents;
        theController.mainBody = thePasser.mainBody;
    }

    public void changeColourPreview()
    {
        // when colour buttons are pressed do not attempt to chnage the colour of the character unless they 
        //can be retained (this means that the VariableContainer is available)
        if (thePasser != null)
        {
            //get the colour of the button that was pressed 
            Color colourToChnage = gameObject.GetComponent<Image>().material.color;

            //get the index of the part selection dropdown box 
            int partToChnage = thePartSelector.value;

            //change the colour of the part selected in the drop down box on the character preview 
            switch (partToChnage)
            {
                case (int)SuitPart.AnklesWrists:
                    theController.anklesWrists = colourToChnage;
                    break;
                case (int)SuitPart.ArmFlaps:
                    theController.armFlaps = colourToChnage;
                    break;
                case (int)SuitPart.BodyLines:
                    theController.bodyLines = colourToChnage;
                    break;
                case (int)SuitPart.Helmet:
                    theController.helmet = colourToChnage;
                    break;
                case (int)SuitPart.HelmetAccents:
                    theController.helmetAccents = colourToChnage;
                    break;
                case (int)SuitPart.MainBody:
                    theController.mainBody = colourToChnage;
                    break;
                default:
                    //if the part selected does not match an enum value then something has gone wrong, report it
                    Debug.Log("Tried to change colour of invalid suit part");
                    break;
            }

        }
    }

}
