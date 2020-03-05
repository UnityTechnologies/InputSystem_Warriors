using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerVisualDisplayBehaviour : MonoBehaviour
{

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Device Display")]
    public DeviceDisplayConfigurator deviceDisplayConfigurator;

    [Header("UI")]
    public GameObject playerDisplay;
    public TextMeshProUGUI playerIDDisplayText;
    public TextMeshProUGUI playerDeviceDisplayText;
    public Image playerDeviceDisplayIcon;

    [Header("Material")]
    public SkinnedMeshRenderer playerSkinnedMeshRenderer;

    //Current Values
    private int currentPlayerID;
    private string currentPlayerDevice;
    private Color currentDeviceColor;
    
    //Shader IDs
    private string armorTintString = "_Tint_Armor";
    private int armorTintShaderID;

    void Start()
    {
        SetupShaderIDs();

        if(playerInput.enabled)
        {
            TogglePlayerVisualDisplay(true);
            SetPlayerVisualDisplay();

        } else if(!playerInput.enabled)
        {

            TogglePlayerVisualDisplay(false);
            
        }
    }

    void SetupShaderIDs()
    {
        armorTintShaderID = Shader.PropertyToID(armorTintString);
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
        playerIDDisplayText.SetText((currentPlayerID + 1).ToString());
        playerDeviceDisplayText.SetText(currentPlayerDevice);
    }

    void SetPlayerDisplayColor()
    {
        playerSkinnedMeshRenderer.material.SetColor(armorTintShaderID, currentDeviceColor);
        playerDeviceDisplayIcon.color = currentDeviceColor;
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
        playerIDDisplayText.SetText("X");
        playerDeviceDisplayText.SetText("Controller Disconnected!");
    }

    void SetDisconnectedPlayerColor()
    {
        playerSkinnedMeshRenderer.material.SetColor(armorTintShaderID, new Color(1, 1, 1));
        playerDeviceDisplayIcon.color = new Color(0,0,0);
    }

    //Utilities

    void TogglePlayerVisualDisplay(bool newState)
    {
        playerDisplay.SetActive(newState);
    }


    void OnControlsChanged()
    {
        SetPlayerVisualDisplay();
    }
}
