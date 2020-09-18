using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsBehaviour : MonoBehaviour
{

    //Player ID
    private int playerID;

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    [Header("Sub Behaviours")]
    public PlayerUIDisplayBehaviour playerUIDisplayBehaviour;

    [Header("Player Material")]
    public SkinnedMeshRenderer playerSkinnedMeshRenderer;

    private int clothingTintShaderID;

    public void SetupBehaviour(int newPlayerID, string newDeviceRawPath)
    {
        playerID = newPlayerID;

        SetupShaderIDs();

        UpdatePlayerVisuals(newDeviceRawPath);
    }

    void SetupShaderIDs()
    {
        clothingTintShaderID = Shader.PropertyToID("_Clothing_Tint");
    }

    public void UpdatePlayerVisuals(string deviceRawPath)
    {
        UpdateUIDisplay(deviceRawPath);
        UpdateCharacterShader(deviceRawPath);
    }

    void UpdateUIDisplay(string deviceRawPath)
    {
        playerUIDisplayBehaviour.UpdatePlayerIDDisplayText(playerID);
        
        string deviceName = deviceDisplaySettings.GetDeviceName(deviceRawPath);
        playerUIDisplayBehaviour.UpdatePlayerDeviceNameDisplayText(deviceName);

        Color deviceColor = deviceDisplaySettings.GetDeviceColor(deviceRawPath);
        playerUIDisplayBehaviour.UpdatePlayerIconDisplayColor(deviceColor);
    }

    void UpdateCharacterShader(string deviceRawPath)
    {
        Color deviceColor = deviceDisplaySettings.GetDeviceColor(deviceRawPath);
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
