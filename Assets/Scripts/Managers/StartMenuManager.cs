using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

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
