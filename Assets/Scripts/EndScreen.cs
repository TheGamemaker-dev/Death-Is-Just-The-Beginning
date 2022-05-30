using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndScreen : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }
}
