using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Device Display Configurator", menuName = "Scriptable Objects/Device Display Configurator", order = 1)]
public class DeviceDisplayConfigurator : ScriptableObject
{
    
    [System.Serializable]
    public struct DeviceSet
    {
        public string deviceRawPath;
        public DeviceDisplaySettings deviceDisplaySettings;
    }

    [System.Serializable]
    public struct DisconnectedSettings
    {
        public string disconnectedDisplayName;
        public Color disconnectedDisplayColor;
    }

    public List<DeviceSet> listDeviceSets = new List<DeviceSet>();

    public DisconnectedSettings disconnectedDeviceSettings;

    private Color fallbackDisplayColor = Color.white;


    public string GetDeviceName(PlayerInput playerInput)
    {

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        string newDisplayName = null;

        for(int i = 0; i < listDeviceSets.Count; i++)
        {

            if(listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
            {   
                newDisplayName = listDeviceSets[i].deviceDisplaySettings.deviceDisplayName;
            }
        }

        if(newDisplayName == null)
        {
            newDisplayName = currentDeviceRawPath;
        }

        return newDisplayName;

    }

    
    public Color GetDeviceColor(PlayerInput playerInput)
    {  

        string currentDeviceRawPath = playerInput.devices[0].ToString();
        
        Color newDisplayColor = fallbackDisplayColor;

        for(int i = 0; i < listDeviceSets.Count; i++)
        {

            if(listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
            {   
                newDisplayColor = listDeviceSets[i].deviceDisplaySettings.deviceDisplayColor;
            }
        }

        return newDisplayColor;
        
    }

    public Sprite GetDeviceBindingIcon(PlayerInput playerInput, string playerInputDeviceInputBinding)
    {

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        Sprite displaySpriteIcon = null;

        for(int i = 0; i < listDeviceSets.Count; i++)
        {
            if(listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
            {
                if(listDeviceSets[i].deviceDisplaySettings.deviceHasContextIcons != null)
                {
                    displaySpriteIcon = FilterForDeviceInputBinding(listDeviceSets[i], playerInputDeviceInputBinding);
                }
            }
        }

        return displaySpriteIcon;
    }

    Sprite FilterForDeviceInputBinding(DeviceSet targetDeviceSet, string inputBinding)
    {
        Sprite spriteIcon = null;

        switch(inputBinding)
        {
            case "Button North":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonNorthIcon;  
                break;

            case "Button South":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonSouthIcon;
                break;

            case "Button West":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonWestIcon;
                break;

            case "Button East":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonEastIcon;
                break;

            case "Right Shoulder":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightFrontIcon;
                break;

            case "Right Trigger":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightBackIcon;
                break;

            case "rightTriggerButton":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightBackIcon;
                break;

            case "Left Shoulder":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftFrontIcon;
                break;

            case "Left Trigger":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftBackIcon;
                break;

            case "leftTriggerButton":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftBackIcon;
                break;

            default:

                for(int i = 0; i < targetDeviceSet.deviceDisplaySettings.customContextIcons.Count; i++)
                {
                    if(targetDeviceSet.deviceDisplaySettings.customContextIcons[i].customInputContextString == inputBinding)
                    {
                        if(targetDeviceSet.deviceDisplaySettings.customContextIcons[i].customInputContextIcon != null)
                        {
                            spriteIcon = targetDeviceSet.deviceDisplaySettings.customContextIcons[i].customInputContextIcon;
                        }
                    }
                }
                
                
                break;

        }

        return spriteIcon;
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