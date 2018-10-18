using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InterSceneCommunication
{
    //this class maintains copies of variables that are needed betweeen scene changes. 
    //It also contains methods that manage the number of VariableContainer object so 
    //that only one copy ever moves between scenes  
    public class VariableContainer : MonoBehaviour
    {
        //The object age variable tracks the age of the object so that we the user comes 
        //back to the main menu after entering another menu we can delete the object that 
        //is the youngest. Thus, we can retain the one with the most recently chnaged settings 
        private int objectAge = 0;  
        //the difficulty rating stores the difficulty rating that is selected by the user 
        public int difficultyRating = 0; 
        public bool isAccelerometerEnabled = true; // stores if the acelerometer is enabled or not 

        public Color mainBody = Color.HSVToRGB(0, 0, 2);  //Colour for main body (Default is shipped off-black)
        public Color armFlaps = Color.HSVToRGB(130, 100, 91); //Colour for arm flaps (Default is shipped green)
        public Color bodyLines = Color.HSVToRGB(0, 0, 100); //Colour for body lines (Default is shipped while)
        public Color helmetAccents = Color.HSVToRGB(0, 0, 2); //Colour for helmet accents (Default is shipped off-black)
        public Color helmet = Color.HSVToRGB(0, 0, 2); //Colour for helmet (Default is shipped off-black)
        public Color anklesWrists = Color.HSVToRGB(0, 0, 50); //Colour for ankles and wrists (Default is shipped grey)

        void OnEnable()
        {
            //Make sure that the 'OnSceneLoaded' function is subscribed to the sceneLoaded event when the script is enabled.
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            //Make sure that the 'OnSceneLoaded' function is unsubscribed to the sceneLoaded event when the script is disabled.
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //An array of VariableContainer objects so that we can loop through them and delete the youngest one  
            VariableContainer[] containersFound = GameObject.FindObjectsOfType<VariableContainer>();
            //get the youngest object (returned from the destroyYoungestDuplicate method) 
            VariableContainer survivingObject = destroyYoungestDuplicate(containersFound);
            //mark the surviving object as DontDestroyOnLoad so that it is mantained between scene chnages
            DontDestroyOnLoad(survivingObject.gameObject);
            //increment that age of the object 
            survivingObject.objectAge++;

        }

        VariableContainer destroyYoungestDuplicate(VariableContainer[] presentObjects)
        {
            //if there is only one object in the array then we don't need to run the loop because there is only one object
            if (presentObjects.Length > 1)
            {
                //loop through the array to find the newly created object (its age will be 0)
                foreach (VariableContainer currentObject in presentObjects)
                {
                    if (currentObject.objectAge == 0)
                    {
                        //when the object is found destroy it
                        Destroy(currentObject);
                    }
                }

            }

            //get the only remaining object in the scene
            VariableContainer survivingObject = GameObject.FindObjectOfType<VariableContainer>();
            //reset its age to prevent integer overflow 
            survivingObject.objectAge = 0;

            //return the object
            return survivingObject;
        }
    }
}
