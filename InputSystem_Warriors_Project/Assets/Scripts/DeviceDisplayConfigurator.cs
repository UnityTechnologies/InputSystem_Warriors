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

    public List<DeviceSettings> deviceSettings = new List<DeviceSettings>();

    private Color fallbackDisplayColor = Color.white;


    public string GetDeviceDisplayName(string playerInputDeviceRawPath)
    {
        string newDisplayName = null;

        for(int i = 0; i < deviceSettings.Count; i++)
        {

            if(deviceSettings[i].deviceRawPath == playerInputDeviceRawPath)
            {   
                newDisplayName = deviceSettings[i].deviceDisplayName;
            }
        }

        if(newDisplayName == null)
        {
            newDisplayName = playerInputDeviceRawPath;
        }

        return newDisplayName;

    }

    
    public Color GetDeviceDisplayColor(string playerInputDeviceRawPath)
    {
        
        Color newDisplayColor = fallbackDisplayColor;

        for(int i = 0; i < deviceSettings.Count; i++)
        {

            if(deviceSettings[i].deviceRawPath == playerInputDeviceRawPath)
            {   
                newDisplayColor = deviceSettings[i].deviceDisplayColor;
            }
        }

        return newDisplayColor;
        
    }
    
    public Color GetFallbackDisplayColor()
    {
        return fallbackDisplayColor;
    }
}