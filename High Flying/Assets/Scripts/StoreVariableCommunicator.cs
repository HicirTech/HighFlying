using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InterSceneCommunication;
using UnityEngine.EventSystems;

public class StoreVariableCommunicator : MonoBehaviour {

    private VariableContainer thePasser = null;
    private ColorControl colorController = null;
    private Dropdown thePartSelector = null;

    [SerializeField][Tooltip("Drop color panel here")]
    private GameObject buttonLayout;

    private enum SuitPart {MainBody, ArmFlaps, BodyLines, HelmetAccents, Helmet, AnklesWrists};

    // Use this for initialization
    void Start ()
    {
        //get the objects that are required for this script to run from the scene
        thePasser = VariableContainer.FindObjectOfType<VariableContainer>();
        colorController = ColorControl.FindObjectOfType<ColorControl>();
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
        thePasser.anklesWrists = colorController.anklesWrists;
        thePasser.armFlaps = colorController.armFlaps;
        thePasser.bodyLines = colorController.bodyLines;
        thePasser.helmet = colorController.helmet;
        thePasser.helmetAccents = colorController.helmetAccents;
        thePasser.mainBody = colorController.mainBody;
    }

    public void changeColourPreview(Button thisButton)
    {
        // when colour buttons are pressed do not attempt to chnage the colour of the character unless they 
        //can be retained (this means that the VariableContainer is available)
        if (thePasser != null)
        {
            //get the colour of the button that was pressed 
            Color colourToChnage = thisButton.GetComponent<Image>().material.color;
            if(colourToChnage != null){
                print("found colorToChange, it is: "+thisButton.name);
            }

            //get the index of the part selection dropdown box 
            int partToChange = thePartSelector.value;
            print("part selected: "+partToChange);

            //change the colour of the part selected in the drop down box on the character preview 
            switch (partToChange)
            {
                case (int)SuitPart.AnklesWrists:
                    print("Found SuitPart to change: "+(int)SuitPart.AnklesWrists+" which is: "+SuitPart.AnklesWrists);
                    colorController.anklesWrists = colourToChnage;
                    break;
                case (int)SuitPart.ArmFlaps:
                    print("Found SuitPart to change: "+(int)SuitPart.ArmFlaps+" which is: "+SuitPart.ArmFlaps);
                    colorController.armFlaps = colourToChnage;
                    break;
                case (int)SuitPart.BodyLines:
                    print("Found SuitPart to change: "+(int)SuitPart.BodyLines+" which is: "+SuitPart.BodyLines);
                    colorController.bodyLines = colourToChnage;
                    break;
                case (int)SuitPart.Helmet:
                    print("Found SuitPart to change: "+(int)SuitPart.Helmet+" which is: "+SuitPart.Helmet);
                    colorController.helmet = colourToChnage;
                    break;
                case (int)SuitPart.HelmetAccents:
                    print("Found SuitPart to change: "+(int)SuitPart.HelmetAccents+" which is: "+SuitPart.HelmetAccents);
                    colorController.helmetAccents = colourToChnage;
                    break;
                case (int)SuitPart.MainBody:
                    print("Found SuitPart to change: "+(int)SuitPart.MainBody+" which is: "+SuitPart.MainBody);
                    colorController.mainBody = colourToChnage;
                    break;
                default:
                    //if the part selected does not match an enum value then something has gone wrong, report it
                    Debug.Log("Tried to change colour of invalid suit part");
                    break;
            }
            print(colorController.updateColors(partToChange));

        }else{
            Debug.Log("thePasser was null");
        }
    }

}
