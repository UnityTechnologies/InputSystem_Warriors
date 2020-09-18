using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Device Display Configurator", menuName = "Scriptable Objects/Device Display Configurator", order = 1)]
public class DeviceDisplayConfigurator : ScriptableObject
{

    [System.Serializable]
    public struct DeviceSettings
    {
        public string deviceRawPath;
        public string deviceDisplayName;
        public Color deviceDisplayColor;
    }

    [System.Serializable]
    public struct DisconnectedSettings
    {
        public string disconnectedDisplayName;
        public Color disconnectedDisplayColor;
    }

    public List<DeviceSettings> listDeviceSettings = new List<DeviceSettings>();

    public DisconnectedSettings disconnectedDeviceSettings;

    private Color fallbackDisplayColor = Color.white;


    public string GetDeviceName(string playerInputDeviceRawPath)
    {
        string newDisplayName = null;

        for(int i = 0; i < listDeviceSettings.Count; i++)
        {

            if(listDeviceSettings[i].deviceRawPath == playerInputDeviceRawPath)
            {   
                newDisplayName = listDeviceSettings[i].deviceDisplayName;
            }
        }

        if(newDisplayName == null)
        {
            newDisplayName = playerInputDeviceRawPath;
        }

        return newDisplayName;

    }

    
    public Color GetDeviceColor(string playerInputDeviceRawPath)
    {
        
        Color newDisplayColor = fallbackDisplayColor;

        for(int i = 0; i < listDeviceSettings.Count; i++)
        {

            if(listDeviceSettings[i].deviceRawPath == playerInputDeviceRawPath)
            {   
                newDisplayColor = listDeviceSettings[i].deviceDisplayColor;
            }
        }

        return newDisplayColor;
        
    }

    public string GetDisconnectedName()
    {
        return disconnectedDeviceSettings.disconnectedDisplayName;
    }

    public Color GetDisconnectedColor()
    {
        return disconnectedDeviceSettings.disconnectedDisplayColor;
    }
    
    
}