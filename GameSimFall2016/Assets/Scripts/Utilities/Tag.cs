using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 

using System.Collections;

//------------------------------------------------------------------------------------------------
// Tag is used to provide a interface for using Unity's tagging system.
[System.Serializable]
public struct Tag
{
    public string value;

    public Tag(string tag)
    {
        value = tag;
    }

    public static implicit operator Tag(string tag)
    {
        return new Tag(tag);
    }

    public static implicit operator string(Tag tag)
    {
        return tag.value;
    }
}

#if UNITY_EDITOR
//------------------------------------------------------------------------------------------------
// A Custom Drawer for displaying a Tag in the Inspector similar to the LayerMask's drop down menu.
[CustomPropertyDrawer(typeof(Tag))]
public class TagDrawer : PropertyDrawer
{
    

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);

        string tag = property.FindPropertyRelative("value").stringValue;

        tag = EditorGUI.TagField(position, label, tag);
        property.FindPropertyRelative("value").stringValue = tag;

        EditorGUI.EndProperty();
    }
}
#endif