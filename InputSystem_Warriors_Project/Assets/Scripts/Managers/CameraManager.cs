using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Component References")]
    public GameObject gameplayCameraObject;
    public GameObject uiOverlayCameraObject;

    public void SetupManager()
    {
        SetCameraObjectNewState(gameplayCameraObject, true);
        SetCameraObjectNewState(uiOverlayCameraObject, false);
    }

    void SetCameraObjectNewState(GameObject cameraObject, bool newState)
    {
        cameraObject.SetActive(newState);
    }

    //This is called by UIBillboardBehaviour so they can orient to wherever the gameplay camera is.
    public Transform GetGameplayCameraTransform()
    {
        return gameplayCameraObject.transform;
    }

    public Camera GetGameplayCamera()
    {
        return gameplayCameraObject.GetComponent<Camera>();
    }

}
