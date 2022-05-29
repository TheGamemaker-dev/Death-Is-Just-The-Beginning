using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerPrefsEditor : EditorWindow
{
    enum Types
    {
        Bool,
        Int,
        Float
    }

    int listLength = 0;
    bool showKeys = false;
    List<string> keys = new List<string>();

    [MenuItem("Tools/PlayerPrefs Editor")]
    private static void ShowWindow()
    {
        var window = GetWindow<PlayerPrefsEditor>();
        window.titleContent = new GUIContent("PlayerPrefs Editor");
        window.Show();
    }

    private void OnGUI()
    {
        listLength = EditorGUILayout.IntField("Size:", listLength);
        showKeys = EditorGUILayout.BeginFoldoutHeaderGroup(showKeys, "Keys");
        if (showKeys)
        {
            for (int i = 0; i < listLength; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.TextField((i + 1).ToString(), "");
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
