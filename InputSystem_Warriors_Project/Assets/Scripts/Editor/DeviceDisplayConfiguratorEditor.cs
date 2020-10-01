using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DeviceDisplayConfigurator))]
public class DeviceDisplayConfiguratorEditor : Editor
{

    private ReorderableList listDevices;
    private SerializedProperty disconnectedDeviceProperty;

    private Rect rawPathColumn;
    private Rect displayNameColumn;
    private Rect displayColorColumn;

    private void OnEnable()
    {
        DrawListOfDevices();
        disconnectedDeviceProperty = serializedObject.FindProperty("disconnectedDeviceSettings");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Device Settings", EditorStyles.boldLabel);
        listDevices.DoLayoutList();

        EditorGUILayout.PropertyField(disconnectedDeviceProperty);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawListOfDevices()
    {
        listDevices = new ReorderableList(serializedObject, serializedObject.FindProperty("listDeviceSettings"), true, true, true, true);

        listDevices.drawHeaderCallback = (Rect rect) => {

            DrawColumnHeaders(rect);
        };

        listDevices.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

	        var element = listDevices.serializedProperty.GetArrayElementAtIndex(index);

	        rect.y += 2;

            CalculateColumnSizes(rect);

            
	        EditorGUI.PropertyField(rawPathColumn, element.FindPropertyRelative("deviceRawPath"), GUIContent.none);
            EditorGUI.PropertyField(displayNameColumn, element.FindPropertyRelative("deviceDisplayName"), GUIContent.none);
            EditorGUI.PropertyField(displayColorColumn, element.FindPropertyRelative("deviceDisplayColor"), GUIContent.none);
            
        };

        //listDevices.elementHeight = EditorGUIUtility.singleLineHeight;
        

    }

    void DrawColumnHeaders(Rect rect)
    {

        CalculateColumnSizes(rect);

        EditorGUI.LabelField(rawPathColumn, "Raw Path Name");
        EditorGUI.LabelField(displayNameColumn, "Display Name");
        EditorGUI.LabelField(displayColorColumn, "Display Color");
    }

    void CalculateColumnSizes(Rect rect)
    {
        rawPathColumn = new Rect(rect.x, rect.y, rect.width/2 - 50 - 10, EditorGUIUtility.singleLineHeight);
        displayNameColumn = new Rect(rect.x + rect.width/2 - 50, rect.y, rect.width/2 - 60, EditorGUIUtility.singleLineHeight);
        displayColorColumn = new Rect(rect.x + rect.width - 100, rect.y, 100, EditorGUIUtility.singleLineHeight);
    }

   
}