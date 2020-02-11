//  
// Copyright (c) 2016 Siyuan Wang.
// Licensed under the Apache License  Version 2.0. See LICENSE file in the project root for full license information.  
//

using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class ReorderableListUtility
{
    public static ReorderableList CreateAutoLayout(SerializedProperty property, float columnSpacing = 10f)
    {
        return CreateAutoLayout(property, true, true, true, true, null, null, columnSpacing);
    }

    public static ReorderableList CreateAutoLayout(SerializedProperty property, string[] headers, float?[] columnWidth = null, float columnSpacing = 10f)
    {
        return CreateAutoLayout(property, true, true, true, true, headers, columnWidth, columnSpacing);
    }

    public static ReorderableList CreateAutoLayout(SerializedProperty property, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton, float columnSpacing = 10f)
    {
        return CreateAutoLayout(property, draggable, displayHeader, displayAddButton, displayRemoveButton, null, null, columnSpacing);
    }

    public static ReorderableList CreateAutoLayout(SerializedProperty property, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton, string[] headers, float?[] columnWidth = null, float columnSpacing = 10f)
    {
        var list = new ReorderableList(property.serializedObject, property, draggable, displayHeader, displayAddButton, displayRemoveButton);
        var colmuns = new List<Column>();

        list.drawElementCallback = DrawElement(list, GetColumnsFunc(list, headers, columnWidth, colmuns), columnSpacing);
        list.drawHeaderCallback = DrawHeader(list, GetColumnsFunc(list, headers, columnWidth, colmuns), columnSpacing);

        return list;
    }

    public static bool DoLayoutListWithFoldout(ReorderableList list, string label = null)
    {
        var property = list.serializedProperty;
        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label != null ? label : property.displayName);
        if (property.isExpanded)
        {
            list.DoLayoutList();
        }

        return property.isExpanded;
    }

    private static ReorderableList.ElementCallbackDelegate DrawElement(ReorderableList list, System.Func<List<Column>> getColumns, float columnSpacing)
    {
        return (rect, index, isActive, isFocused) =>
        {
            var property = list.serializedProperty;
            var columns = getColumns();
            var layouts = CalculateColumnLayout(columns, rect, columnSpacing);

            var arect = rect;
            arect.height = EditorGUIUtility.singleLineHeight;
            for (var ii = 0; ii < columns.Count; ii++)
            {
                var c = columns[ii];

                arect.width = layouts[ii];
                EditorGUI.PropertyField(arect, property.GetArrayElementAtIndex(index).FindPropertyRelative(c.PropertyName), GUIContent.none);
                arect.x += arect.width + columnSpacing;
            }
        };
    }

    private static ReorderableList.HeaderCallbackDelegate DrawHeader(ReorderableList list, System.Func<List<Column>> getColumns, float columnSpacing)
    {
        return (rect) =>
        {
            var columns = getColumns();
                
            if (list.draggable)
            {
                rect.width -= 15;
                rect.x += 15;
            }

            var layouts = CalculateColumnLayout(columns, rect, columnSpacing);
            var arect = rect;
            arect.height = EditorGUIUtility.singleLineHeight;
            for (var ii = 0; ii < columns.Count; ii++)
            {
                var c = columns[ii];

                arect.width = layouts[ii];
                EditorGUI.LabelField(arect, c.DisplayName);
                arect.x += arect.width + columnSpacing;
            }
        };
    }

    private static System.Func<List<Column>> GetColumnsFunc(ReorderableList list, string[] headers, float?[] columnWidth, List<Column> output)
    {
        var property = list.serializedProperty;
        return () =>
        {
            if (output.Count <= 0 || list.serializedProperty != property)
            {
                output.Clear();
                property = list.serializedProperty;

                if (property.isArray && property.arraySize > 0)
                {
                    var it = property.GetArrayElementAtIndex(0).Copy();
                    var prefix = it.propertyPath;
                    var index = 0;
                    if (it.Next(true))
                    {
                        do
                        {
                            if (it.propertyPath.StartsWith(prefix))
                            {
                                var c = new Column();
                                c.DisplayName = (headers != null && headers.Length > index) ? headers[index] : it.displayName;
                                c.PropertyName = it.propertyPath.Substring(prefix.Length + 1);
                                c.Width = (columnWidth != null && columnWidth.Length > index) ? columnWidth[index] : null;

                                output.Add(c);
                            }
                            else
                            {
                                break;
                            }

                            index += 1;
                        }
                        while (it.Next(false));
                    }
                }
            }

            return output;
        };
    }

    private static List<float> CalculateColumnLayout(List<Column> columns, Rect rect, float columnSpacing)
    {
        var autoWidth = rect.width;
        var autoCount = 0;
        foreach (var column in columns)
        {
            if (column.Width.HasValue)
            {
                autoWidth -= column.Width.Value;
            }
            else
            {
                autoCount += 1;
            }
        }

        autoWidth -= (columns.Count - 1) * columnSpacing;
        autoWidth /= autoCount;

        var widths = new List<float>(columns.Count);
        foreach (var column in columns)
        {
            if (column.Width.HasValue)
            {
                widths.Add(column.Width.Value);
            }
            else
            {
                widths.Add(autoWidth);
            }
        }

        return widths;
    }

    private struct Column
    {
        public string DisplayName;
        public string PropertyName;
        public float? Width;
    }
}