using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {


    [SerializeField] Camera camera;
    Vector3 cameraLocation;

    private void Start()
    {
        //camera.GetComponent<Camera>()
        cameraLocation = camera.transform.position;
    }

    public void MoveCameraToComputer()
    {
        camera.transform.position = cameraLocation;
    }
    public void MoveCameraToTurrets()
    {
        camera.transform.position = cameraLocation + new Vector3(40, 0, 0);
    }
    public void MoveCameraToEngineerer()
    {
        camera.transform.position = cameraLocation + new Vector3(80, 0, 0);
    }
    public void MoveCameraToMeeting()
    {
        camera.transform.position = cameraLocation - new Vector3(40, 0, 0);
    }
    public void MoveCameraToTinker()
    {
        camera.transform.position = cameraLocation - new Vector3(80, 0, 0);
    }

}
