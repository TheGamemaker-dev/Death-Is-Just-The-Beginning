using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    Button continueButton;

    private void Start()
    {
        continueButton.interactable = PlayerPrefs.HasKey(GameManager.LEVEL_KEY);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        GameManager.singleton.currentLevel = 1;
    }

    public void ContinueGame()
    {
        int lastLevelDone = PlayerPrefs.GetInt(GameManager.LEVEL_KEY);
        SceneManager.LoadScene(lastLevelDone);
        GameManager.singleton.currentLevel = lastLevelDone;
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
