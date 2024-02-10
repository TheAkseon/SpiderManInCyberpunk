using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Plugins.Audio.Core;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance = null;

    [Header("Audio Sources")]
    [SerializeField] private SourceAudio _soundsDatabase;
    [SerializeField] private AudioSource effectsAudioSource;

    private float _startVolume;

    private float _fadeDuration = 0.5f;
    private string _nameSoundLevel;

    public string NameSoundLevel => _nameSoundLevel;

    //private int currentClip = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
        //Destroy(gameObject);
    }

    private void Start()
    {
        _soundsDatabase.Volume = 0.4f;

        int levelNumber = SceneManager.GetActiveScene().buildIndex;

        if (levelNumber <= 6)
        {
            _soundsDatabase.Play(levelNumber.ToString());
            _nameSoundLevel = levelNumber.ToString();
        }
        else
        {
            _nameSoundLevel = UnityEngine.Random.Range(1, 7).ToString();
            _soundsDatabase.Play(_nameSoundLevel);
        }

        _soundsDatabase.Loop = true;
    }

    public void PlaySound(string name)
    {
        _soundsDatabase.PlayOneShot(name);
    }

    public void Mute(string source, bool value)
    {
        if (source.Equals("music"))
            _soundsDatabase.Mute = value;
        else
            effectsAudioSource.mute = value;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _soundsDatabase.Volume = Mathf.Lerp(_startVolume, 0.0f, elapsedTime / _fadeDuration);
            yield return null;
        }

        _soundsDatabase.Stop();
    }
}

[Serializable]
public class Music
{
    public int levelNumber;
    public AudioClip audio;
}
