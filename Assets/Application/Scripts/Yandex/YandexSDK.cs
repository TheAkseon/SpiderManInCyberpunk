using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using Unity.VisualScripting;
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
        InitializeGameData();
        yield break; // Завершаем корутину для не UNITY_WEBGL платформ
#else
        yield return YandexGamesSdk.Initialize();

        _language = YandexGamesSdk.Environment.i18n.lang;
        _localization.SetLanguage(_language);

        yield return GetData();

        // Показываем рекламу перед стартом игры, если необходимо
        InterstitialAd.Show(null, (bool _) => StartGame());
        //StartGame(); // Начинаем игру
#endif
    }

    private void InitializeGameData()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
        SaveData.Instance.Load();

        if (SaveData.Instance.Data == null)
        {
            CreateNewGameData();
        }
        else
        {
            _levelLoader.LoadLevel(SaveData.Instance.Data.CurrentLevel);
        }
    }

    private void CreateNewGameData()
    {
        SaveData.Instance.NewData();
        InitializeNewPlayerData();
        SaveManager.Save(_saveKey, SaveData.Instance.Data); // Сохраняем новые данные
        StartGame(); // Начинаем игру с новыми данными
    }

    private void InitializeNewPlayerData()
    {
        SaveData.Instance.Data.CurrentLevel = 1;
        SaveData.Instance.Data.FakeLevel = 1;
        SaveData.Instance.Data.CostOfDamageImprovements = 10;
        SaveData.Instance.Data.CostOfFiringRateImprovements = 20;
        SaveData.Instance.Data.BaseDamage = 1;
        SaveData.Instance.Data.BaseFiringRate = 1;
    }

    private void StartGame()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            YandexGamesSdk.GameReady();
            if (_levelLoader == null)
            {
                _levelLoader = FindObjectOfType<LevelLoader>();
            }

            if (SaveData.Instance.Data.CurrentLevel == 0 && SaveData.Instance.Data.FakeLevel == 0)
            {
                SaveData.Instance.NewData();
                InitializeNewPlayerData();
                SaveManager.Save(_saveKey, SaveData.Instance.Data);
            }

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


