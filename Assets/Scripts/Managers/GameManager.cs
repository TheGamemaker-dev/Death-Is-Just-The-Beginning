using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly string LEVEL_KEY = "CURRENT_LEVEL";
    public static GameManager singleton;
    public readonly int levelCount = 3;

    public int currentLevel;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex < levelCount)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            currentLevel++;
        }
        else
        {
            SceneManager.LoadScene("Finish");
        }
        SaveProgress();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
    }
}
