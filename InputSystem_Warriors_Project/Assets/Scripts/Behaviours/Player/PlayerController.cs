using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    //Player ID
    private int playerID;

    [Header("Sub Behaviours")]
    public PlayerMovementBehaviour playerMovementBehaviour;
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerVisualsBehaviour playerVisualsBehaviour;


    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;
    
    //Action Maps
    private string actionMapGameplay = "Player Controls";
    private string actionMapMenu = "Menu Controls";

    //Current Control Scheme
    private string currentControlScheme;


    //This is called from the GameManager; when the game is being setup.
    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;

        currentControlScheme = playerInput.currentControlScheme;

        playerMovementBehaviour.SetupBehaviour();
        playerAnimationBehaviour.SetupBehaviour();
        playerVisualsBehaviour.SetupBehaviour(playerID, playerInput);
    }


    //INPUT SYSTEM ACTION METHODS --------------

    //This is called from PlayerInput; when a joystick or arrow keys has been pushed.
    //It stores the input Vector as a Vector3 to then be used by the smoothing function.
    private void OnMovement(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    //This is called from PlayerInput, when a button has been pushed, that corresponds with the 'Attack' action
    private void OnAttack(InputValue value)
    {
        playerAnimationBehaviour.PlayAttackAnimation();
    }

    //This is called from Player Input, when a button has been pushed, that correspons with the 'TogglePause' action
    private void OnTogglePause(InputValue value)
    {
        GameManager.Instance.TogglePauseState(this);
    }





    //INPUT SYSTEM AUTOMATIC CALLBACKS --------------

    //This is automatically called from PlayerInput, when the input device has changed
    //(IE: Keyboard -> Xbox Controller)
    public void OnControlsChanged()
    {

        if(playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;

            playerVisualsBehaviour.UpdatePlayerVisuals();
            RemoveAllBindingOverrides();
        }
    }

    //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
    //IE: Device unplugged or has run out of batteries
    public void OnDeviceLost()
    {
        playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
    }

    //This is automatically called from PlayerInput, then the input device reconnected
    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
        playerVisualsBehaviour.UpdatePlayerVisuals();
    }



    //Update Loop - Used for calculating frame-based data
    void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimationMovement();
    }

    //Input's Axes values are raw
    void CalculateMovementInputSmoothing()
    {
        
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);

    }

    void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(smoothInputMovement);
    }

    void UpdatePlayerAnimationMovement()
    {
        playerAnimationBehaviour.UpdateMovementAnimation(rawInputMovement.sqrMagnitude);
    }


    public void SetInputActiveState(bool gameIsPaused)
    {
        switch (gameIsPaused)
        {
            case true:
                playerInput.DeactivateInput();
                break;

            case false:
                playerInput.ActivateInput();
                break;
        }
    }

    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }



    //Switching Action Maps ----
    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapGameplay);  
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenu);
    }


    //Get Data ----
    public int GetPlayerID()
    {
        return playerID;
    }

    public InputActionAsset GetActionAsset()
    {
        return playerInput.actions;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }


}
