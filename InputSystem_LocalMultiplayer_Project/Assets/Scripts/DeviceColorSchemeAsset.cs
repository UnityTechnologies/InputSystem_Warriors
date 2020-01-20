using UnityEngine;

[CreateAssetMenu(fileName = "Device Color Scheme", menuName = "Scriptable Objects/Device Color Scheme", order = 1)]
public class DeviceColorScheme : ScriptableObject
{
    [Header("Desktop")]
    public Color keyboard;

    [Header("Console")]
    public Color xboxController;
    public Color playstationController;
    public Color nintendoSwitchController;

    [Header("Fallback")]
    public Color fallbackController;
}