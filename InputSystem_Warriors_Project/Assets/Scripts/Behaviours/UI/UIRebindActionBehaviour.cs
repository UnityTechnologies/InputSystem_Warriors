using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIRebindActionBehaviour : MonoBehaviour
{   
    //Input System Stored Data
    private InputActionAsset focusedInputActionAsset;
    private PlayerInput focusedPlayerInput;
    private InputAction focusedInputAction;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    [Header("Rebind Settings")]
    public string actionName;

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    [Header("UI Display - Action Text")]
    public TextMeshProUGUI actionNameDisplayText;

    [Header("UI Display - Binding Text or Icon")]
    public TextMeshProUGUI bindingNameDisplayText;
    public Image bindingIconDisplayImage;

    [Header("UI Display - Buttons")]
    public GameObject rebindButtonObject;
    public GameObject resetButtonObject;

    [Header("UI Display - Listening Text")]
    public GameObject listeningForInputObject;


    public void UpdateBehaviour()
    {   
        GetFocusedPlayerInput();
        SetupFocusedInputAction();
        UpdateActionDisplayUI();
        UpdateBindingDisplayUI();
    }

    void GetFocusedPlayerInput()
    {
        PlayerController focusedPlayerController = GameManager.Instance.GetFocusedPlayerController();
        focusedPlayerInput = focusedPlayerController.GetPlayerInput();
    }

    void SetupFocusedInputAction()
    {
        focusedInputAction = focusedPlayerInput.actions.FindAction(actionName);
    }

    public void ButtonPressedStartRebind()
    {
        StartRebindProcess();
    }

    void StartRebindProcess()
    {
        
        ToggleGameObjectState(rebindButtonObject, false);
        ToggleGameObjectState(resetButtonObject, false);
        ToggleGameObjectState(listeningForInputObject, true);

        
        rebindOperation = focusedInputAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithControlsExcluding("<Keyboard>/p")
            .WithControlsExcluding("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindCompleted());

        rebindOperation.Start();
    }


    void RebindCompleted()
    {
        rebindOperation.Dispose();
        rebindOperation = null;

        ToggleGameObjectState(rebindButtonObject, true);
        ToggleGameObjectState(resetButtonObject, true);
        ToggleGameObjectState(listeningForInputObject, false);

        UpdateActionDisplayUI();
        UpdateBindingDisplayUI();
    }

    public void ButtonPressedResetBinding()
    {
        ResetBinding();
    }

    public void ResetBinding()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(focusedInputAction);
        UpdateBindingDisplayUI();
    }



    void UpdateActionDisplayUI()
    {
        actionNameDisplayText.SetText(actionName);
    }

    void UpdateBindingDisplayUI()
    {

        int controlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[0]);
        string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        Sprite currentDisplayIcon = deviceDisplaySettings.GetDeviceBindingIcon(focusedPlayerInput, currentBindingInput);

        if(currentDisplayIcon)
        {
            ToggleGameObjectState(bindingNameDisplayText.gameObject, false);
            ToggleGameObjectState(bindingIconDisplayImage.gameObject, true);
            bindingIconDisplayImage.sprite = currentDisplayIcon;
        } else if(currentDisplayIcon == null)
        {
            ToggleGameObjectState(bindingNameDisplayText.gameObject, true);
            ToggleGameObjectState(bindingIconDisplayImage.gameObject, false);
            bindingNameDisplayText.SetText(currentBindingInput);
        }

    }


    void ToggleGameObjectState(GameObject targetGameObject, bool newState)
    {
        targetGameObject.SetActive(newState);
    }



    
}
