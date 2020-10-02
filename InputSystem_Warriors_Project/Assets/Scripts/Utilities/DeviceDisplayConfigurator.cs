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
        public DeviceDisplayIconSet deviceDisplayIconSet;
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

    public Sprite GetDeviceBindingIcon(string playerInputDeviceRawPath, string playerInputDeviceInputBinding)
    {

        Sprite displaySpriteIcon = null;

        for(int i = 0; i < listDeviceSettings.Count; i++)
        {
            if(listDeviceSettings[i].deviceRawPath == playerInputDeviceRawPath)
            {
                if(listDeviceSettings[i].deviceDisplayIconSet != null)
                {
                    displaySpriteIcon = FilterForDeviceInputBinding(listDeviceSettings[i], playerInputDeviceInputBinding);
                }
            }
        }

        return displaySpriteIcon;
    }

    Sprite FilterForDeviceInputBinding(DeviceSettings targetDeviceSetting, string inputBinding)
    {
        Sprite selectedSpriteIcon = null;

        switch(inputBinding)
        {
            case "Button North":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.buttonNorthIcon;  
                break;

            case "Button South":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.buttonSouthIcon;
                break;

            case "Button West":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.buttonWestIcon;
                break;

            case "Button East":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.buttonEastIcon;
                break;

            case "Right Shoulder":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerRightFrontIcon;
                break;

            case "Right Trigger":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerRightBackIcon;
                break;

            case "rightTriggerButton":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerRightBackIcon;
                break;

            case "Left Shoulder":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerLeftFrontIcon;
                break;

            case "Left Trigger":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerLeftBackIcon;
                break;

            case "leftTriggerButton":
                selectedSpriteIcon = targetDeviceSetting.deviceDisplayIconSet.triggerLeftBackIcon;
                break;

        }

        return selectedSpriteIcon;
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