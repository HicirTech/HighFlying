using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InterSceneCommunication
{
    public class VariableContainer : MonoBehaviour
    {
        private int objectAge = 0;
        public int difficultyRating = 0;
        public bool isAccelerometerEnabled = false;

        public Color mainBody = Color.HSVToRGB(0, 0, 2, true);  //Colour for main body (Default is shipped off-black)
        public Color armFlaps = Color.HSVToRGB(130, 100, 91, true); //Colour for arm flaps (Default is shipped green)
        public Color bodyLines = Color.HSVToRGB(0, 0, 100, true); //Colour for body lines (Default is shipped while)
        public Color helmetAccents = Color.HSVToRGB(0, 0, 2, true); //Colour for helmet accents (Default is shipped off-black)
        public Color helmet = Color.HSVToRGB(0, 0, 2, true); //Colour for helmet (Default is shipped off-black)
        public Color anklesWrists = Color.HSVToRGB(0, 0, 50, true); //Colour for ankles and wrists (Default is shipped grey)

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
            VariableContainer[] containersFound = GameObject.FindObjectsOfType<VariableContainer>();
            VariableContainer survivingObject = destroyYoungestDuplicate(containersFound);
            DontDestroyOnLoad(survivingObject.gameObject);
            survivingObject.objectAge++;

        }

        VariableContainer destroyYoungestDuplicate(VariableContainer[] presentObjects)
        {

            if (presentObjects.Length > 1)
            {
                foreach (VariableContainer currentObject in presentObjects)
                {
                    if (currentObject.objectAge == 0)
                    {
                        Destroy(currentObject);
                    }
                }

            }

            VariableContainer survivingObject = GameObject.FindObjectOfType<VariableContainer>();
            survivingObject.objectAge = 0;

            return survivingObject;
        }
    }
}
