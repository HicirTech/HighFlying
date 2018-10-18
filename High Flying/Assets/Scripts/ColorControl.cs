using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterSceneCommunication;

public class ColorControl : MonoBehaviour {

    private VariableContainer theContainer = null; //a reference to the variable container 

	[SerializeField][Tooltip("Click to enable/disable the script")]
	private bool enable = true; //enable bool for entire script

	[Tooltip("Select color for Main Body")] public Color mainBody;  //Serialized color for main body
	[Tooltip("Select color for Arm Flaps")] public Color armFlaps; //Serialized color for arm flaps
	[Tooltip("Select color for Main Body")] public Color bodyLines; //Serialized color for body lines
	[Tooltip("Select color for Main Body")] public Color helmetAccents; //Serialized color for helmet accents
	[Tooltip("Select color for Main Body")] public Color helmet; //Serialized color for helmet
	[Tooltip("Select color for Main Body")] public Color anklesWrists; //Serialized color for ankles and wrists

	[SerializeField][Tooltip("Drop Pilot Model here (not character, model)")] private GameObject pilot; //Serialized drop field for pilot model
	private GameObject mainBodyGO; //Private GameObject for main body
	private GameObject armFlapsGO; //Private GameObject for arm flaps
	private GameObject bodyLinesGO; //Private GameObject for body lines
	private GameObject helmetAccentsGO; //Private GameObject for helmet accents
	private GameObject helmetGO; //Private GameObject for helmet
	private GameObject anklesLeftGO; //Private GameObject for ankles
	private GameObject wristsLeftGO; //Private GameObject for wrists
	private GameObject anklesRightGO; //Private GameObject for ankles
	private GameObject wristsRightGO; //Private GameObject for wrists

	//Main initialization
	void Awake(){
		if(enable){
            //Do not touch the child getters. Unfortunately, the only other way to do this is serialized fields and it looks ugly with a 
            //billion different variables for each object, so I'm using .Find
            //.Find only works per transform. It does not get children or grandchildren gameobjects.
            //Get component from child of child ect of Pilot and then debug it
            mainBodyGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Suit1").gameObject;
			if(mainBodyGO != null) Debug.Log("main body object found");
			else Debug.Log("main body object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			armFlapsGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Suit1").gameObject;
			if(armFlapsGO != null) Debug.Log("arm flap object found");
			else Debug.Log("arm flap object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			bodyLinesGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Suit1").gameObject;
			if(bodyLinesGO != null) Debug.Log("body lines object found");
			else Debug.Log("body lines object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			helmetGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Joints_grp").gameObject.transform.Find("skydiver_rig9:Root").gameObject.transform.Find("skydiver_rig9:Ribs").gameObject.transform.Find("skydiver_rig9:Neck_Joint").gameObject.transform.Find("skydiver_rig9:Head_Joint").gameObject.transform.Find("skydiver_rig9:Helmet").gameObject;
			if(helmetGO != null) Debug.Log("helmet object found");
			else Debug.Log("helmet object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			helmetAccentsGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Joints_grp").gameObject.transform.Find("skydiver_rig9:Root").gameObject.transform.Find("skydiver_rig9:Ribs").gameObject.transform.Find("skydiver_rig9:Neck_Joint").gameObject.transform.Find("skydiver_rig9:Head_Joint").gameObject.transform.Find("skydiver_rig9:Helmet").gameObject;
			if(helmetAccentsGO != null) Debug.Log("helmet accents object found");
			else Debug.Log("helmet accents object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			anklesLeftGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Body").gameObject.transform.Find("skydiver_rig9:LeftSneakers").gameObject;
			if(anklesLeftGO != null) Debug.Log("anklesLeft object found");
			else Debug.Log("anklesLeft object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			anklesRightGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Body").gameObject.transform.Find("skydiver_rig9:RightSneakers").gameObject;
			if(anklesRightGO != null) Debug.Log("anklesRight object found");
			else Debug.Log("anklesRight object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			wristsLeftGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Body").gameObject.transform.Find("skydiver_rig9:LeftHand").gameObject;
			if(wristsLeftGO != null) Debug.Log("wristsLeft object found");
			else Debug.Log("wristsLeft object NOT FOUND");

            //Get component from child of child ect of Pilot and then debug it
			wristsRightGO = pilot.transform.Find("skydiver_rig9:Wingsuit_rig_dont_touch").gameObject.transform.Find("skydiver_rig9:Wingsuit").gameObject.transform.Find("skydiver_rig9:Body").gameObject.transform.Find("skydiver_rig9:RightHand").gameObject;
			if(wristsRightGO != null) Debug.Log("wristsRight object found");
			else Debug.Log("wristsRight object NOT FOUND");
		}
		else{
			//If script is disabled, debug
			Debug.Log("Color Control Script is disabled");
		}
	}

	// Use this for initialization
	void Start () {
		if(enable){
			//If the variable container can not be found then tell the user, otherwise get and use the required values
            if (GameObject.FindObjectsOfType<VariableContainer>().Length != 1){
                Debug.Log("Could not find variable container");
            }else{
                //get the container 
                theContainer = GameObject.FindObjectOfType<VariableContainer>();
                
                // then, set the wingsuit colours to the ones set by the user
                anklesWrists = theContainer.anklesWrists;
                armFlaps = theContainer.armFlaps;
                bodyLines = theContainer.bodyLines;
                helmet = theContainer.helmet;
                helmetAccents = theContainer.helmetAccents;
                mainBody = theContainer.mainBody;
            }

            this.updateColors(6);
		}
	}

	public string updateColors(int changed){
		//Get the materials array from component since components sometimes have multiple materials
		var mainBodyGOmats = mainBodyGO.GetComponent<Renderer>().materials;
		mainBodyGOmats[0].color = mainBody;
		
		//Get the materials array from component since components sometimes have multiple materials
		var armFlapsGOmats = armFlapsGO.GetComponent<Renderer>().materials;
		armFlapsGOmats[1].color = armFlaps;
		
		//Get the materials array from component since components sometimes have multiple materials
		var bodyLinesGOmats = bodyLinesGO.GetComponent<Renderer>().materials;
		bodyLinesGOmats[3].color = bodyLines;
		
		//Get the materials array from component since components sometimes have multiple materials
		var helmetGOmats = helmetGO.GetComponent<Renderer>().materials;
		helmetGOmats[0].color = helmet;
		
		//Get the materials array from component since components sometimes have multiple materials
		var helmetAccentsGOmats = helmetAccentsGO.GetComponent<Renderer>().materials;
		helmetAccentsGOmats[2].color = helmetAccents;
		helmetAccentsGOmats[1].color = helmetAccents;
		helmetAccentsGOmats[3].color = helmetAccents;
		helmetAccentsGOmats[4].color = helmetAccents;
		
		//Get the materials array from component since components sometimes have multiple materials
		var anklesLeftGOmats = anklesLeftGO.GetComponent<Renderer>().materials;
		var anklesRightGOmats = anklesRightGO.GetComponent<Renderer>().materials;
		var wristsLeftGOmats = wristsLeftGO.GetComponent<Renderer>().materials;
		var wristsRightGOmats = wristsRightGO.GetComponent<Renderer>().materials;
		anklesLeftGOmats[0].color = anklesWrists;
		anklesRightGOmats[0].color = anklesWrists;
		wristsLeftGOmats[0].color = anklesWrists;
		wristsRightGOmats[0].color = anklesWrists;

		this.debugLog(changed);
		return("updated colors");
	}

	private void debugLog(int changed){
		switch(changed){
			case 0:
				print("Main body changed to: "+mainBody);
				break;
			case 1:
				print("Arm Flaps changed to: "+armFlaps);
				break;
			case 2:
				print("Body Lines changed to: "+bodyLines);
				break;
			case 3:
				print("Helmet changed to: "+helmetAccents);
				break;
			case 4:
				print("Helmet accents changed to: "+helmet);
				break;
			case 5:
				print("ankles and wrists changed to: "+anklesWrists);
				break;
			case 6:
				print("Changed all");
				break;
		}
	}
}
