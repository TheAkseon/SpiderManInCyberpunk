using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBehaviour : MonoBehaviour
{
    public static LevelBehaviour Instance;

    [SerializeField] CoinManager _coinManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings && SaveData.Instance.Data.FakeLevel < 30)
        {
            SaveData.Instance.Data.FakeLevel = next;
            SaveData.Instance.Data.CurrentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SaveData.Instance.Save();
        }
        else
        {
            SaveData.Instance.Data.FakeLevel += 1;
            next = Random.Range(1, SceneManager.sceneCountInBuildSettings);
            SaveData.Instance.Data.CurrentLevel = next;
            SaveData.Instance.Save();
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexAds.Instance.ShowInterstitial();
#endif
        SceneManager.LoadScene(next);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
