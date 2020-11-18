using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMenuPlayerDeviceDisplayBehaviour : MonoBehaviour
{

    [Header("Device Display")]
    public DeviceDisplayConfigurator deviceDisplayConfigurator;

    [Header("Display References")]
    public TextMeshProUGUI playerIDDisplay;
    public TextMeshProUGUI playerDeviceDisplay;
    public Image playerDeviceDisplayIcon;

    public void SetPlayerDeviceDisplay(int playerID, string playerDevicePath)
    {
        playerIDDisplay.SetText((playerID + 1).ToString());
        UpdatePlayerDeviceDisplay(playerDevicePath);
    }

    public void UpdatePlayerDeviceDisplay(string playerDevicePath)
    {   
        /*
        playerDeviceDisplay.SetText(deviceDisplayConfigurator.GetDeviceDisplayName(playerDevicePath));
        playerDeviceDisplayIcon.color = deviceDisplayConfigurator.GetDeviceDisplayColor(playerDevicePath);
        */  
    }

}
