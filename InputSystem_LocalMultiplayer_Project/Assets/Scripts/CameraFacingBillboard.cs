using UnityEngine;
using System.Collections;
 
[ExecuteAlways]
public class CameraFacingBillboard : MonoBehaviour
{
    public Camera camera;

    void OnEnable()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void OnDisable()
    {
        camera = null;
    }
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {

        if(camera)
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }


    }
}