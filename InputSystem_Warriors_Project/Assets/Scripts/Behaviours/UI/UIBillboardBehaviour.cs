using UnityEngine;
using System.Collections;
 
[ExecuteAlways]
public class UIBillboardBehaviour : MonoBehaviour
{
    private Transform gameplayCameraTransform;

    void OnEnable()
    {
        gameplayCameraTransform = CameraManager.Instance.GetGameplayCameraTransform();
    }

    void OnDisable()
    {
        gameplayCameraTransform = null;
    }
 
    void LateUpdate()
    {

        if(gameplayCameraTransform)
        {
            transform.LookAt(transform.position + gameplayCameraTransform.rotation * Vector3.forward, gameplayCameraTransform.rotation * Vector3.up);
        }


    }
}