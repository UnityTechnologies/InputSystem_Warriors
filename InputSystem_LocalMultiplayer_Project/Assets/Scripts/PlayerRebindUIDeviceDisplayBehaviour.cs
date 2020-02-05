using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRebindUIDeviceDisplayBehaviour : MonoBehaviour
{

    [Header("Device Display")]
    public DeviceDisplayConfigurator deviceDisplayConfigurator;

    [Header("Display References")]
    public TextMeshProUGUI playerIDDisplay;
    public TextMeshProUGUI playerDeviceDisplay;

    public void SetupPanelDisplays(int playerID, string playerDevicePath)
    {
        playerIDDisplay.SetText("Player " + (playerID + 1));

        playerDeviceDisplay.SetText(deviceDisplayConfigurator.GetDeviceDisplayName(playerDevicePath));
        playerDeviceDisplay.color = deviceDisplayConfigurator.GetDeviceDisplayColor(playerDevicePath);

    }
}
