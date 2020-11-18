using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisualsBehaviour : MonoBehaviour
{

    //Player ID
    private int playerID;
    private PlayerInput playerInput;

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    [Header("Sub Behaviours")]
    public PlayerUIDisplayBehaviour playerUIDisplayBehaviour;

    [Header("Player Material")]
    public SkinnedMeshRenderer playerSkinnedMeshRenderer;

    private int clothingTintShaderID;

    public void SetupBehaviour(int newPlayerID, PlayerInput newPlayerInput)
    {
        playerID = newPlayerID;
        playerInput = newPlayerInput;

        SetupShaderIDs();

        UpdatePlayerVisuals();
    }

    void SetupShaderIDs()
    {
        clothingTintShaderID = Shader.PropertyToID("_Clothing_Tint");
    }

    public void UpdatePlayerVisuals()
    {
        UpdateUIDisplay();
        UpdateCharacterShader();
    }

    void UpdateUIDisplay()
    {
        playerUIDisplayBehaviour.UpdatePlayerIDDisplayText(playerID);
        
        string deviceName = deviceDisplaySettings.GetDeviceName(playerInput);
        playerUIDisplayBehaviour.UpdatePlayerDeviceNameDisplayText(deviceName);

        Color deviceColor = deviceDisplaySettings.GetDeviceColor(playerInput);
        playerUIDisplayBehaviour.UpdatePlayerIconDisplayColor(deviceColor);
    }

    void UpdateCharacterShader()
    {
        Color deviceColor = deviceDisplaySettings.GetDeviceColor(playerInput);
        playerSkinnedMeshRenderer.material.SetColor(clothingTintShaderID, deviceColor);
    }

    public void SetDisconnectedDeviceVisuals()
    {
        string disconnectedName = deviceDisplaySettings.GetDisconnectedName();
        playerUIDisplayBehaviour.UpdatePlayerDeviceNameDisplayText(disconnectedName);

        Color disconnectedColor = deviceDisplaySettings.GetDisconnectedColor();
        playerUIDisplayBehaviour.UpdatePlayerIconDisplayColor(disconnectedColor);
        playerSkinnedMeshRenderer.material.SetColor(clothingTintShaderID, disconnectedColor);
        
    }
}
