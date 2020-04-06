using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(DeviceDisplayConfigurator))]
[CanEditMultipleObjects]
public class DeviceDisplayConfiguratorEditor : Editor
{
    private ReorderableList list2;

    public override void OnInspectorGUI()
    {

        this.serializedObject.Update();

        ReorderableListUtility.DoLayoutListWithFoldout(this.list2);

        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        var property = this.serializedObject.FindProperty("deviceSettings");

        this.list2 = ReorderableListUtility.CreateAutoLayout(
            property,
            new string[] { "Raw Path Name", "Display Name", "Display Color" }
        );
    }
}