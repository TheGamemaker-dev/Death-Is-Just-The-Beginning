using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    [SerializeField] List<AudioClip> music = new List<AudioClip>();
    [SerializeField] List<AudioClip> sfx = new List<AudioClip>();

    static AudioManager singleton;

    AudioSource source;

    private void Awake()
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
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (AudioClip clip in music)
        {
            Debug.Log(clip.name);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeMusicOnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeMusicOnSceneChange;
    }

    void ChangeMusicOnSceneChange(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        switch (sceneName)
        {
            case "Start":
                PlayMusic("Start Menu Music");
                break;
            default:
                System.Random rand = new System.Random(Mathf.RoundToInt(Time.time));
                int index = rand.Next(0, music.Count);
                PlayMusic(index);
                break;
        }
    }

    public void PlayMusic(string name)
    {
        AudioClip clipToPlay = music.FirstOrDefault(x => x.name == name);

        if (clipToPlay == default(AudioClip))
        {
            Debug.LogError("Clip not found by name: " + name);
            return;
        }

        source.clip = clipToPlay;
        source.Play();
    }

    public void PlayMusic(int index)
    {
        AudioClip clipToPlay = music[index];

        source.clip = clipToPlay;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }

    public void PlaySoundEffect(string name)
    {
        AudioClip sfxToPlay = sfx.FirstOrDefault(x => x.name == name);

        if (sfxToPlay == default(AudioClip))
        {
            Debug.LogError("Clip not found by name: " + name);
            return;
        }

        AudioSource.PlayClipAtPoint(sfxToPlay, Camera.main.gameObject.transform.position + Vector3.forward);
    }

}