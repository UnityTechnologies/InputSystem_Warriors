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
        if (playerInput == null || playerInput.devices.Count == 0 || playerInput.devices[0] == null)
        {
            return string.IsNullOrEmpty(disconnectedDeviceSettings.disconnectedDisplayName) ? "Unknown Device" : disconnectedDeviceSettings.disconnectedDisplayName;
        }

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        string newDisplayName = null;

        if (listDeviceSets != null)
        {
            for (int i = 0; i < listDeviceSets.Count; i++)
            {
                if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
                {
                    newDisplayName = listDeviceSets[i].deviceDisplaySettings != null
                        ? listDeviceSets[i].deviceDisplaySettings.deviceDisplayName
                        : null;
                    break;
                }
            }
        }

        if (string.IsNullOrEmpty(newDisplayName))
        {
            newDisplayName = currentDeviceRawPath;
        }

        return newDisplayName;
    }


    public Color GetDeviceColor(PlayerInput playerInput)
    {
        if (playerInput == null || playerInput.devices.Count == 0 || playerInput.devices[0] == null)
        {
            return disconnectedDeviceSettings.disconnectedDisplayColor;
        }

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        Color newDisplayColor = fallbackDisplayColor;

        if (listDeviceSets != null)
        {
            for (int i = 0; i < listDeviceSets.Count; i++)
            {
                if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
                {
                    if (listDeviceSets[i].deviceDisplaySettings != null)
                    {
                        newDisplayColor = listDeviceSets[i].deviceDisplaySettings.deviceDisplayColor;
                    }
                    break;
                }
            }
        }

        return newDisplayColor;
    }

    public Sprite GetDeviceBindingIcon(PlayerInput playerInput, string playerInputDeviceInputBinding)
    {
        if (playerInput == null || playerInput.devices.Count == 0 || playerInput.devices[0] == null)
        {
            return null;
        }

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        Sprite displaySpriteIcon = null;

        if (listDeviceSets != null)
        {
            for (int i = 0; i < listDeviceSets.Count; i++)
            {
                if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
                {
                    var settings = listDeviceSets[i].deviceDisplaySettings;
                    if (settings?.deviceHasContextIcons == true)
                    {
                        displaySpriteIcon = FilterForDeviceInputBinding(listDeviceSets[i], playerInputDeviceInputBinding);
                    }
                    break;
                }
            }
        }

        return displaySpriteIcon;
    }

    Sprite FilterForDeviceInputBinding(DeviceSet targetDeviceSet, string inputBinding)
    {
        if (targetDeviceSet.deviceDisplaySettings == null)
            return null;

        Sprite spriteIcon = null;

        switch (inputBinding)
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
            case "rightTriggerButton":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightBackIcon;
                break;

            case "Left Shoulder":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftFrontIcon;
                break;

            case "Left Trigger":
            case "leftTriggerButton":
                spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftBackIcon;
                break;

            default:
                var customIcons = targetDeviceSet.deviceDisplaySettings.customContextIcons;
                if (customIcons != null)
                {
                    for (int i = 0; i < customIcons.Count; i++)
                    {
                        var entry = customIcons[i];
                        if (!string.IsNullOrEmpty(entry.customInputContextString) && entry.customInputContextString == inputBinding)
                        {
                            if (entry.customInputContextIcon != null)
                            {
                                spriteIcon = entry.customInputContextIcon;
                                break;
                            }
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