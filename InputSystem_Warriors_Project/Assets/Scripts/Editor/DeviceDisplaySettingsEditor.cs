using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DeviceDisplaySettings))]
public class DeviceDisplaySettingsEditor : Editor
{

    private DeviceDisplaySettings deviceDisplaySettings;

    //Display Name
    public SerializedProperty deviceDisplayNameProperty;

    //Display Color
    public SerializedProperty deviceDisplayColorProperty;

    //Icons
    public SerializedProperty deviceHasContextIconsProperty;

    //Icons - Action Buttons
    public SerializedProperty buttonNorthIconProperty;
    public SerializedProperty buttonSouthIconProperty;
    public SerializedProperty buttonWestIconProperty;
    public SerializedProperty buttonEastIconProperty;

    //Icon - Triggers
    public SerializedProperty triggerRightFrontIconProperty;
    public SerializedProperty triggerRightBackIconProperty;
    public SerializedProperty triggerLeftFrontIconProperty;
    public SerializedProperty triggerLeftBackIconProperty;

    void OnEnable()
    {
        //Display Name
        deviceDisplayNameProperty = serializedObject.FindProperty("deviceDisplayName");

        //Display Color
        deviceDisplayColorProperty = serializedObject.FindProperty("deviceDisplayColor");

        //Icons
        deviceHasContextIconsProperty = serializedObject.FindProperty("deviceHasContextIcons");

        //Icons - Action Buttons
        buttonNorthIconProperty = serializedObject.FindProperty("buttonNorthIcon");
        buttonSouthIconProperty = serializedObject.FindProperty("buttonSouthIcon");
        buttonWestIconProperty = serializedObject.FindProperty("buttonWestIcon");
        buttonEastIconProperty = serializedObject.FindProperty("buttonEastIcon");

        //Icon - Triggers
        triggerRightFrontIconProperty = serializedObject.FindProperty("triggerRightFrontIcon");
        triggerRightBackIconProperty = serializedObject.FindProperty("triggerRightBackIcon");
        triggerLeftFrontIconProperty = serializedObject.FindProperty("triggerLeftFrontIcon");
        triggerLeftBackIconProperty = serializedObject.FindProperty("triggerLeftBackIcon");  
    }

    public override void OnInspectorGUI()
    {

        deviceDisplaySettings = (DeviceDisplaySettings)target;

        serializedObject.Update();

        EditorGUILayout.LabelField("Display Name", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(deviceDisplayNameProperty);

        DrawSpaceGUI(2);

        EditorGUILayout.LabelField("Display Color", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(deviceDisplayColorProperty);

        DrawSpaceGUI(2);

        EditorGUILayout.LabelField("Icon Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(deviceHasContextIconsProperty);

        if(deviceDisplaySettings.deviceHasContextIcons == true)
        {

            DrawSpaceGUI(3);
            
            EditorGUILayout.LabelField("Icons - Action Buttons", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(buttonNorthIconProperty);
            EditorGUILayout.PropertyField(buttonSouthIconProperty);
            EditorGUILayout.PropertyField(buttonWestIconProperty);
            EditorGUILayout.PropertyField(buttonEastIconProperty);

            DrawSpaceGUI(3);

            EditorGUILayout.LabelField("Icons - Triggers", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(triggerRightFrontIconProperty);
            EditorGUILayout.PropertyField(triggerRightBackIconProperty);
            EditorGUILayout.PropertyField(triggerLeftFrontIconProperty);
            EditorGUILayout.PropertyField(triggerLeftBackIconProperty);
        }

        serializedObject.ApplyModifiedProperties();

    }

    void DrawSpaceGUI(int amountOfSpace)
    {
        for(int i = 0; i < amountOfSpace; i++)
        {
            EditorGUILayout.Space();
        }
    }


}
