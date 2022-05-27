using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public readonly int levelCount = 2;
    public static GameManager singleton;

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
        }
        else
        {
            SceneManager.LoadScene("Finish");
        }
    }
}
