using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Color Schemes")]
    public DeviceColorScheme deviceColorScheme;

    [Header("UI")]
    public TextMeshProUGUI playerDisplayText;

    private int currentPlayerID;
    private string currentPlayerDevice;
    private Color currentDeviceColor;

    void Start()
    {
        GetPlayerInputID();
        GetPlayerInputDevice();
        SetDisplayText();
    }

    void GetPlayerInputID()
    {
        currentPlayerID = playerInput.playerIndex;
    }

    void GetPlayerInputDevice()
    {
        currentPlayerDevice = ConfigureDevice(playerInput.devices[0].ToString());
    }

    string ConfigureDevice(string currentDevice)
    {

        switch(currentDevice)
        {
            case "Keyboard:/Keyboard":
                currentDeviceColor = deviceColorScheme.keyboard;
                return "Keyboard";
                break;

            case "XInputControllerWindows:/XInputControllerWindows":
                currentDeviceColor = deviceColorScheme.xboxController;
                return "Xbox Controller";
                break;

            case "DualShock4GamepadHID:/DualShock4GamepadHID":
                currentDeviceColor = deviceColorScheme.playstationController;
                return "PlayStation Controller";
                break;

            case "SwitchProControllerHID:/SwitchProControllerHID":
                currentDeviceColor = deviceColorScheme.nintendoSwitchController;
                return "Nintendo Switch Controller";
                break;

            default:

                currentDeviceColor = deviceColorScheme.fallbackController;
                return currentDevice;

                break;

        }
    }

    void SetDisplayText()
    {

        playerDisplayText.SetText("Player: " + (currentPlayerID + 1) + "\n" + currentPlayerDevice);
        playerDisplayText.color = currentDeviceColor;

    }

}
