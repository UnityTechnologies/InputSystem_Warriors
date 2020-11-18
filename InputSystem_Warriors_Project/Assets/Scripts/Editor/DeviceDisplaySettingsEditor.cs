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

    //Icon - Custom Contexts
    private ReorderableList customInputContextIconList;

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

        DrawCustomContextList();
    }

    void DrawCustomContextList()
    {
        customInputContextIconList = new ReorderableList(serializedObject, serializedObject.FindProperty("customContextIcons"), true, true, true, true);
        
        customInputContextIconList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(CalculateColumn(rect, 1, 15, 0), "Input Binding String");
            EditorGUI.LabelField(CalculateColumn(rect, 2, 15, 0), "Display Icon");
        };

        customInputContextIconList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = customInputContextIconList.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 2;

            EditorGUI.PropertyField(CalculateColumn(rect, 1, 0, 0), element.FindPropertyRelative("customInputContextString"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumn(rect, 2, 10, 10), element.FindPropertyRelative("customInputContextIcon"), GUIContent.none);

        };
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

            DrawSpaceGUI(3);

            EditorGUILayout.LabelField("Icons - Custom Contexts", EditorStyles.boldLabel);
            customInputContextIconList.DoLayoutList();
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

    Rect CalculateColumn(Rect rect, int columnNumber, float xPadding, float xWidth)
    {
        float xPosition = rect.x; 
        switch (columnNumber)
        {
            case 1:
                xPosition = rect.x + xPadding;
                break;

            case 2:
                xPosition = rect.x + rect.width/2 + xPadding;
                break;
        }


        return new Rect(xPosition, rect.y, rect.width / 2 - xWidth, EditorGUIUtility.singleLineHeight);
    }


}
