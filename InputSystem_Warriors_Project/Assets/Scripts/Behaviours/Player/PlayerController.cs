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
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerVisualsBehaviour playerVisualsBehaviour;

    [Header("Physics")]
    public Rigidbody playerRigidbody;

    [Header("Input")]
    public bool useOldInputManager = false;
    public PlayerInput playerInput;
    private string actionMapGameplay = "Player Controls";
    private string actionMapMenu = "Menu Controls";
    
    private Vector3 inputDirection;
    private Vector2 movementInput;
    private bool currentInput = false;

    //Camera
    private Camera mainCamera;

    [Header("Movement Settings")]
    public float movementSpeed = 3;
    public float smoothingSpeed = 1;
    private Vector3 currentDirection;
    private Vector3 rawDirection;
    private Vector3 smoothDirection;
    private Vector3 movement;

    //This is called from the GameManager; after the player has been spawned
    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;
        playerAnimationBehaviour.SetupBehaviour();
        playerVisualsBehaviour.SetupBehaviour(playerID, playerInput.devices[0].ToString());
        FindGameplayCamera();

    }

    void FindGameplayCamera()
    {
        mainCamera = CameraManager.Instance.GetGameplayCamera();
    }


    void Update()
    {
        CalculateMovementInput();
        CalculateAttackInput();
    }

    void FixedUpdate()
    {
        CalculateDesiredDirection();
        ConvertDirectionFromRawToSmooth();
        MoveThePlayer();
        AnimatePlayerMovement();
        TurnThePlayer();
    }

    void CalculateMovementInput()
    {

        if(useOldInputManager)
        {
            var v = Input.GetAxisRaw("Vertical");
            var h = Input.GetAxisRaw("Horizontal");
            inputDirection = new Vector3(h, 0, v);
        }
        
        if(inputDirection == Vector3.zero)
        {
            currentInput = false;
        }
        else if(inputDirection != Vector3.zero)
        {
            currentInput = true;
        }
    }

    void CalculateAttackInput()
    {
        
        if(useOldInputManager)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimationBehaviour.PlayAttackAnimation();
            }
        }
        
    }

    void CalculateDesiredDirection()
    {
        //Camera Direction
		var cameraForward = mainCamera.transform.forward;
		var cameraRight = mainCamera.transform.right;

		cameraForward.y = 0f;
		cameraRight.y = 0f;

        rawDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
    }

    void ConvertDirectionFromRawToSmooth()
    {   
        if(currentInput == true)
        {
            smoothDirection = Vector3.Lerp(smoothDirection, rawDirection, Time.deltaTime * smoothingSpeed);
        } else if(currentInput == false)
        {
            smoothDirection = Vector3.zero;
        }
    }

    void MoveThePlayer()
    {
        if(currentInput == true)
        {
            movement.Set(smoothDirection.x, 0f, smoothDirection.z);
            movement = movement.normalized * movementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    void TurnThePlayer()
    {
        if(currentInput == true)
        {
            Quaternion newRotation = Quaternion.LookRotation(-smoothDirection);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void AnimatePlayerMovement()
    {
        playerAnimationBehaviour.UpdateMovementAnimation(inputDirection.sqrMagnitude);
    }




    //Action Callbacks from the new Input System ----

    private void OnMovement(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        inputDirection = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    private void OnAttack(InputValue value)
    {
        playerAnimationBehaviour.PlayAttackAnimation();
    }






    private void OnTogglePause(InputValue value)
    {
        GameManager.Instance.TogglePauseState(this);
    }
    

    public void OnControlsChanged()
    {
        playerVisualsBehaviour.UpdatePlayerVisuals(playerInput.devices[0].ToString());
    }

    public void OnDeviceLost()
    {
        playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
    }

    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
       
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
        playerVisualsBehaviour.UpdatePlayerVisuals(playerInput.devices[0].ToString());
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

    public string GetRawDevicePath()
    {
        return playerInput.devices[0].ToString();
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
