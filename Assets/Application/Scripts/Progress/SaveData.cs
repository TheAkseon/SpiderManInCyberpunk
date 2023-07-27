using System;
using Agava.YandexGames;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    [SerializeField] public DataHolder _data;

    public DataHolder Data => _data;

    private const string _leaderboardTxt = "Leaderboard";
    private const string _saveKey = "SaveData";

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
    }

    public void NewData()
    {
        _data = new DataHolder();
        _data.CurrentLevel = 1;
        _data.FakeLevel = 1;
        _data.CostOfDamageImprovements = 10;
        _data.CostOfFiringRateImprovements = 20;
        _data.BaseDamage = 1;
        _data.BaseFiringRate = 1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _data = new DataHolder();

            SaveManager.Reset(_saveKey, _data);
            SaveYandex();
        }
    }

    private void OnDisable()
    {
        SaveYandex();
        Save();
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, _data);
    }

    public void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);
        _data = data;
    }

    public void SetLeaderboardScore()
    {
        int current = _data.Score;

#if UNITY_WEBGL && !UNITY_EDITOR
        Leaderboard.GetPlayerEntry(_leaderboardTxt, (result) =>
        {
            if (current >= result.score)
                SaveBestScore(current);
        });
#endif
    }

    public void SaveYandex()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string jsonDataString = JsonUtility.ToJson(_data, true);

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetCloudSaveData(jsonDataString);
#endif
    }

    private void SaveBestScore(int bestScore)
    {
        Leaderboard.SetScore(_leaderboardTxt, bestScore);
    }
}

[Serializable]
public class DataHolder
{
    public int Coins;
    public int Score;
    public int CurrentLevel;
    public int FakeLevel;
    public bool muteMusic;
    public bool muteEffects;
    public int CostOfDamageImprovements;
    public int CostOfFiringRateImprovements;
    public int BaseDamage;
    public float BaseFiringRate;
}
