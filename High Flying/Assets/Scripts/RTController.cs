using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RTController : MonoBehaviour {

    private RenderTexture iceLevelRT;
    private RenderTexture cityLevelRT;
    [SerializeField]
    private string iceLevelName = "Ice Level";
    [SerializeField]
    private string cityLevelName = "City Level";
    [SerializeField]
    private string relativePathToIceLevelRT = "RT/IceLevelRender";
    [SerializeField]
    private string relativePathToCityLevelRT = "RT/IceLevelRender";
    [SerializeField]
    private string iceRTCameraObjectName = "Ice Render Texture Camera";
    [SerializeField]
    private string cityRTCameraObjectName = "City Render Texture Camera";


    public void Awake()
    {
        iceLevelRT = Resources.Load<RenderTexture>(relativePathToIceLevelRT);
        cityLevelRT = Resources.Load<RenderTexture>(relativePathToCityLevelRT);

        getLevel(iceLevelName);
        getLevel(cityLevelName);
        
    }

    public void Start()
    {
        getRTCameraInScene(cityRTCameraObjectName).Render();
        getRTCameraInScene(iceRTCameraObjectName).Render();
    }

    private Scene getLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
         return SceneManager.GetSceneByName(levelName);
    }

    private Camera getRTCameraInScene(string RTCameraName)
    {
       GameObject cameraObject = GameObject.Find(RTCameraName);
        return cameraObject.GetComponent<Camera>();
    }
}
