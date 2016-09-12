using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 

using System.Collections;

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