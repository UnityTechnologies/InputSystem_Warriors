using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerVisualDisplayBehaviour : MonoBehaviour
{

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Device Display")]
    public DeviceDisplayConfigurator deviceDisplayConfigurator;

    [Header("UI")]
    public TextMeshProUGUI playerDisplayText;

    [Header("Material")]
    public SkinnedMeshRenderer playerSkinnedMeshRenderer;

    //Current Values
    private int currentPlayerID;
    private string currentPlayerDevice;
    private Color currentDeviceColor;
    
    //Shader IDs
    private string armorTintString = "_ArmorTint";
    private int armorTintShaderID;

    void Start()
    {
        SetupShaderIDs();

        if(playerInput.enabled)
        {

            SetPlayerVisualDisplay();

        } else if(!playerInput.enabled)
        {

            HidePlayerVisualDisplay();
            
        }
    }

    void SetupShaderIDs()
    {
        armorTintShaderID = Shader.PropertyToID("_Tint_Armor");
    }

    void GetPlayerInputID()
    {
        currentPlayerID = playerInput.playerIndex;    
    }

    void SetPlayerVisualDisplay()
    {
        GetPlayerInputID();
        GetPlayerDeviceDisplayName();
        GetPlayerDeviceDisplayColor();
        SetPlayerDisplayText();
        SetPlayerDisplayColor();
    }

    void GetPlayerDeviceDisplayName()
    {
        currentPlayerDevice = deviceDisplayConfigurator.GetDeviceDisplayName(playerInput.devices[0].ToString());
    }

    void GetPlayerDeviceDisplayColor()
    {
        currentDeviceColor = deviceDisplayConfigurator.GetDeviceDisplayColor(playerInput.devices[0].ToString());
    }

    void SetPlayerDisplayText()
    {
        playerDisplayText.SetText("Player: " + (currentPlayerID + 1) + "\n" + currentPlayerDevice);
    }

    void SetPlayerDisplayColor()
    {
        playerSkinnedMeshRenderer.material.SetColor(armorTintShaderID, currentDeviceColor);
    }

    //Device Callbacks from the new Input System ----

    private void OnDeviceLost()
    {
        SetDisconnectedPlayerText();
        SetDisconnectedPlayerColor();
    }

    private void OnDeviceRegained()
    {

        SetPlayerDisplayText();
        SetPlayerDisplayColor();
    }

    //Disconnect Behaviours

    void SetDisconnectedPlayerText()
    {
        playerDisplayText.SetText("Player: " + (currentPlayerID + 1) + "\n" + "Device Disconnected!");
    }

    void SetDisconnectedPlayerColor()
    {
        playerSkinnedMeshRenderer.material.SetColor(armorTintShaderID, new Color(1, 1, 1));
    }

    //Utilities

    void HidePlayerVisualDisplay()
    {
        playerDisplayText.enabled = false;
    }

}
