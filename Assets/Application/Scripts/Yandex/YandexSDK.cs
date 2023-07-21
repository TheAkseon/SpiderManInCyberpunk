using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private Localization _localization;

    private LevelLoader _levelLoader;
    private const string _saveKey = "SaveData";
    private string _language;
    public bool IsAdRunning;

    public static YandexSDK Instance;

    public string CurrentLanguage => _language;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _levelLoader = FindObjectOfType<LevelLoader>();
        SaveData.Instance.Load();

        if (SaveData.Instance.Data == null)
        {
            SaveData.Instance.NewData();
        }

        if (SaveData.Instance.Data.CurrentLevel == 0 && SaveData.Instance.Data.FakeLevel == 0)
        {
            SaveData.Instance.Data.CurrentLevel = 1;
            SaveData.Instance.Data.FakeLevel = 1;
        }

        _levelLoader.LoadLevel(SaveData.Instance.Data.CurrentLevel);
        yield return null;
#else
        yield return YandexGamesSdk.Initialize();

        _language = YandexGamesSdk.Environment.i18n.lang;
        _localization.SetLanguage(_language);

        yield return GetData();

        InterstitialAd.Show(null, (bool _) => StartGame());
#endif
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (!IsAdRunning)
            MuteAudio(inBackground);
    }

    private void MuteAudio(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
        AudioListener.pause = value;
        AudioListener.volume = value ? 0f : 1f;
        SoundsManager.Instance.Mute("music", value);
        SoundsManager.Instance.Mute("effects", value);
    }

    private void StartGame()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            _levelLoader = FindObjectOfType<LevelLoader>();
            SaveData.Instance.Load();

            if (SaveData.Instance.Data == null)
            {
                SaveData.Instance.NewData();
            }

            if (SaveData.Instance.Data.CurrentLevel == 0 && SaveData.Instance.Data.FakeLevel == 0)
            {
                SaveData.Instance.Data.CurrentLevel = 1;
                SaveData.Instance.Data.FakeLevel = 1;
            }

            //SaveData.Instance.SetLeaderboardScore();
            _levelLoader.LoadLevel(SaveData.Instance.Data.CurrentLevel);
        }
    }

    private IEnumerator GetData()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            string loadedString = "None";

            PlayerAccount.GetCloudSaveData((data) =>
            {
                loadedString = data;
            });

            while (loadedString == "None")
            {
                yield return null;
            }

            if (loadedString == "{}")
            {
                yield break;
            }

            SaveData.Instance._data = JsonUtility.FromJson<DataHolder>(loadedString);
            SaveManager.Save(_saveKey, SaveData.Instance._data);
        }
        else
        {
            yield return YandexGamesSdk.Initialize();
        }
    }
}