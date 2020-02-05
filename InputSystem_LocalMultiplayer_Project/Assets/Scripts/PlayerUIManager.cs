using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Device Display")]
    public DeviceDisplayConfigurator deviceDisplayConfigurator;

    [Header("UI")]
    public TextMeshProUGUI playerDisplayText;

    private int currentPlayerID;
    private string currentPlayerDevice;
    private Color currentDeviceColor;

    void Start()
    {
        GetPlayerInputID();
        GetPlayerDeviceDisplayName();
        GetPlayerDeviceDisplayColor();
        SetDisplayText();
    }

    void GetPlayerInputID()
    {
        currentPlayerID = playerInput.playerIndex;
    }

    void GetPlayerDeviceDisplayName()
    {
        currentPlayerDevice = deviceDisplayConfigurator.GetDeviceDisplayName(playerInput.devices[0].ToString());
    }

    void GetPlayerDeviceDisplayColor()
    {
        currentDeviceColor = deviceDisplayConfigurator.GetDeviceDisplayColor(playerInput.devices[0].ToString());
    }

    void SetDisplayText()
    {
        playerDisplayText.SetText("Player: " + (currentPlayerID + 1) + "\n" + currentPlayerDevice);
        playerDisplayText.color = currentDeviceColor;
    }

}
