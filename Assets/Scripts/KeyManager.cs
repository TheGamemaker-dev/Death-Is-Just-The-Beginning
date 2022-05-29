using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager singleton;
    public static int keyTotal;

    private void Start()
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        keyTotal = 0;
        if (!scene.name.Contains("Level"))
        {
            Destroy(gameObject);
            singleton = null;
        }
    }
}
