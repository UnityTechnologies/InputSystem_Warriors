using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIDisplayBehaviour : MonoBehaviour
{

    public TextMeshProUGUI IDDisplayText;
    public TextMeshProUGUI deviceNameDisplayText;
    public Image deviceDisplayIcon;

    public void UpdatePlayerIDDisplayText(int newPlayerID)
    {
        IDDisplayText.SetText((newPlayerID + 1).ToString());
    }

    public void UpdatePlayerDeviceNameDisplayText(string newDeviceName)
    {
        deviceNameDisplayText.SetText(newDeviceName);
    }

    public void UpdatePlayerIconDisplayColor(Color newColor)
    {
        deviceDisplayIcon.color = newColor;
    }

}