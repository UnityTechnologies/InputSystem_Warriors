using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIRebindActionBehaviour : MonoBehaviour
{   

    private InputActionAsset focusedInputActionAsset;
    private PlayerInput focusedPlayerInput;
    private InputAction focusedInputAction;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    [Header("Rebind Settings")]
    public string actionName;

    [Header("UI Display - Action and Binding Text")]
    public TextMeshProUGUI actionNameDisplayText;
    public TextMeshProUGUI bindingNameDisplayText;

    [Header("UI Display - Rebind Button")]
    public GameObject rebindButtonObject;

    [Header("UI Display - Listening Text")]
    public GameObject listeningForInputObject;


    public void UpdateBehaviour()
    {   
        GetFocusedPlayerInput();
        SetupFocusedInputAction();
        UpdateActionAndBindingDisplayUI();
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

    public void RebindButtonPressed()
    {
        StartRebindProcess();
    }

    void StartRebindProcess()
    {
        
        ToggleGameObjectState(rebindButtonObject, false);
        ToggleGameObjectState(listeningForInputObject, true);

        rebindOperation?.Dispose();
        rebindOperation = focusedInputAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithControlsExcluding("<Keyboard>/p")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ButtonRebindCompleted());

        rebindOperation.Start();
    }


    void ButtonRebindCompleted()
    {
        rebindOperation.Dispose();
        rebindOperation = null;

        ToggleGameObjectState(rebindButtonObject, true);
        ToggleGameObjectState(listeningForInputObject, false);

        UpdateActionAndBindingDisplayUI();
    }



    void UpdateActionAndBindingDisplayUI()
    {
        actionNameDisplayText.SetText(actionName);

        int controlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[0]);
        string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingNameDisplayText.SetText(currentBindingInput);
    }


    void ToggleGameObjectState(GameObject targetGameObject, bool newState)
    {
        targetGameObject.SetActive(newState);
    }



    
}
