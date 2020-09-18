using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIRebindActionBehaviour : MonoBehaviour
{   

    private InputActionAsset focusedInputActionAsset;

    private bool listeningForInput;

    [Header("Rebind Settings")]
    public string actionName;

    [Header("UI Display")]
    public Image bindingIconDisplayImage;
    public GameObject listeningObject;


    public void UpdateBehaviour()
    {   
        GetFocusedInputActionAsset();
    }

    void GetFocusedInputActionAsset()
    {
        focusedInputActionAsset = EventSystemManager.Instance.GetInputActionAsset();
    }

    public void RebindButtonPressed()
    {
        StartListeningForRebind();
    }

    void StartListeningForRebind()
    {
        listeningForInput = true;
    }



    
}
