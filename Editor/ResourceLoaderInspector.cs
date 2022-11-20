using UnityEditor;
using UnityEngine;
using NduGames.ResourceLoader;

[CustomEditor(typeof(ResourceLoader))]
public class ResourceLoaderInspector : Editor
{
    private ResourceLoader _loader;
    private SerializedProperty _subDirectoryPathProperty;
    private SerializedProperty _resourcesProperty;

    public override void OnInspectorGUI()
    {
        Debug.Log("ResourceLoaderInspector.OnInspectorGUI() called!");
        serializedObject.Update();
        EditorGUILayout.PropertyField(_subDirectoryPathProperty, new GUIContent("path"));
        EditorGUILayout.PropertyField(_resourcesProperty, new GUIContent("resources"), false);
        serializedObject.ApplyModifiedProperties();
    }

    private void Awake()
    {
        _subDirectoryPathProperty = serializedObject.FindProperty("_subDirectoryPath");
        _resourcesProperty = serializedObject.FindProperty("_resources");
        _loader = this.target as ResourceLoader;
    }
}
