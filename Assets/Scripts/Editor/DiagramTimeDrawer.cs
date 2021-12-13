using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DiagramTimeAttribute))]
public class DiagramTimeDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.PropertyField(position, property, label);

            return;
        }

        DiagramTimeAttribute timeAttribute = (DiagramTimeAttribute)attribute;

        EditorGUI.IntPopup(position, property, timeAttribute.Options, timeAttribute.Values, label);
    }
}
