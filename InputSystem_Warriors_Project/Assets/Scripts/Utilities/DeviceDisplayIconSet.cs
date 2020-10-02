using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Device Display Icon Set", menuName = "Scriptable Objects/Device Display Icon Set", order = 1)]
public class DeviceDisplayIconSet : ScriptableObject
{

    [Header("Action Buttons")]
    public Sprite buttonNorthIcon;
    public Sprite buttonSouthIcon;
    public Sprite buttonWestIcon;
    public Sprite buttonEastIcon;

    [Header("Triggers")]
    public Sprite triggerRightFrontIcon;
    public Sprite triggerRightBackIcon;
    public Sprite triggerLeftFrontIcon;
    public Sprite triggerLeftBackIcon;
}
