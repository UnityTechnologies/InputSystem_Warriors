using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DeviceDisplayConfigurator))]
public class DeviceDisplayConfiguratorEditor : Editor
{

    private ReorderableList listDeviceSets;
    private SerializedProperty disconnectedDeviceProperty;

    private void OnEnable()
    {
        DrawListOfDevices();
        disconnectedDeviceProperty = serializedObject.FindProperty("disconnectedDeviceSettings");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Device Sets", EditorStyles.boldLabel);
        listDeviceSets.DoLayoutList();

        EditorGUILayout.PropertyField(disconnectedDeviceProperty);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawListOfDevices()
    {
        listDeviceSets = new ReorderableList(serializedObject, serializedObject.FindProperty("listDeviceSets"), true, true, true, true);

        listDeviceSets.drawHeaderCallback = (Rect rect) => {

            EditorGUI.LabelField(CalculateColumn(rect, 1, 15, 0), "Raw Path Name");
            EditorGUI.LabelField(CalculateColumn(rect, 2, 15, 0), "Device Display Settings");
        };

        listDeviceSets.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

	        var element = listDeviceSets.serializedProperty.GetArrayElementAtIndex(index);

	        rect.y += 2;
            
	        EditorGUI.PropertyField(CalculateColumn(rect, 1, 0, 0), element.FindPropertyRelative("deviceRawPath"), GUIContent.none);
            EditorGUI.PropertyField(CalculateColumn(rect, 2, 10, 10), element.FindPropertyRelative("deviceDisplaySettings"), GUIContent.none);
        
            
        };   

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