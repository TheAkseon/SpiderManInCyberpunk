using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public static UIBehaviour Instance;

    [Header("Panels")]
    [SerializeField] GameObject _startMenuPanel;
    [SerializeField] GameObject _inGamePanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _casesPanel;
    [SerializeField] GameObject _bossFightPanel;
    [SerializeField] GameObject _inputSlider;

    [Header("Player")]
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] TextMeshProUGUI _coinText;

    [Header("Improvements")]
    public TextMeshProUGUI _damageImprovementCount;
    public TextMeshProUGUI _firingRateImprovementCount;
    [SerializeField] TextMeshProUGUI _damageCostText;
    [SerializeField] TextMeshProUGUI _firingRateCostText;
    private readonly string Damage = "width";
    private readonly string FiringRate = "height";

    [Header("Sound")]
    [SerializeField] Button musicButton;
    [SerializeField] Button effectsButton;
    [SerializeField] Sprite notSprite;
    [SerializeField] Sprite yesSprite;

    [Header("Game Over Panel")]
    [SerializeField] GameObject _continueButton;
    [SerializeField] GameObject _restartButton;

    private bool muteMusic;
    private bool muteEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _startMenuPanel.SetActive(true);
        PlayerMove.Instance.StopMovement();
        _levelText.text = SaveData.Instance.Data.FakeLevel.ToString();
        muteEffects = SaveData.Instance.Data.muteEffects;
        muteMusic = SaveData.Instance.Data.muteMusic;

        if (SaveData.Instance.Data.muteMusic == true)
        {
            Image image;
            bool state;
            image = musicButton.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            state = muteMusic;
            SoundsManager.Instance.Mute("music", muteMusic);

            if (!state)
                image.sprite = yesSprite;
            else
                image.sprite = notSprite;
        }

        if (SaveData.Instance.Data.muteEffects == true)
        {
            Image image;
            bool state;
            image = effectsButton.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            state = muteEffects;
            SoundsManager.Instance.Mute("effects", muteEffects);

            if (!state)
                image.sprite = yesSprite;
            else
                image.sprite = notSprite;
        }
    }

    public void Play()
    {
        _startMenuPanel.SetActive(false);
        _inGamePanel.SetActive(true);
        PlayerMove.Instance.ResumeMovement();
        FindObjectOfType<PlayerBehaviour>().Play();
    }

    public void Mute(string type)
    {
        UnityEngine.UI.Image image;
        bool state;
        if (type.Equals("music"))
        {
            image = musicButton.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            muteMusic = !muteMusic;
            SaveData.Instance.Data.muteMusic = muteMusic;
            state = muteMusic;
            SoundsManager.Instance.Mute(type, muteMusic);
        }
        else
        { 
            image = effectsButton.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            muteEffects = !muteEffects;
            SaveData.Instance.Data.muteEffects = muteEffects;
            state = muteEffects;
            SoundsManager.Instance.Mute(type, muteEffects);
        }

        if (!state)
            image.sprite = yesSprite;
        else
            image.sprite = notSprite;

        SaveData.Instance.Save();
#if UNITY_WEBGL && !UNITY_EDITOR
        SaveData.Instance.SaveYandex();
#endif
    }

    public void Victory()
    {
        _bossFightPanel.SetActive(false);
        _casesPanel.SetActive(true);
    }

    public void BossFight()
    {
        _bossFightPanel.SetActive(true);
        _inputSlider.SetActive(false);
    }

    private IEnumerator CheckRewarded()
    {
        while (YandexAds.Instance.IsRewarded == false)
        {
            yield return null;
        }

        _gameOverPanel.SetActive(false);
        _inputSlider.SetActive(true);
        PlayerModifier.Instance.Reberth();
        PlayerMove.Instance.ResumeMovement();
        PlayerMove.Instance.ApplyInvulnerable();
        PlayerAnimationController.Instance.Run();
    }

    public void Continue()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Time.timeScale = 0f;
        YandexAds.Instance.ShowRewardAd();
        StartCoroutine(CheckRewarded());
#else
        _gameOverPanel.SetActive(false);
        _inputSlider.SetActive(true);
        PlayerModifier.Instance.Reberth();
        PlayerMove.Instance.ResumeMovement();
        PlayerMove.Instance.ApplyInvulnerable();
        PlayerAnimationController.Instance.Run();
#endif
    }

    public void GameOver(bool _isBoss)
    {
        if (_isBoss)
        {
            BlockContinueButton();
            SoundsManager.Instance.FadeOut();
            SoundsManager.Instance.PlaySound("GameOver");
        }

        _gameOverPanel.SetActive(true);
        _inputSlider.SetActive(false);
        PlayerMove.Instance.StopMovement();
    }

    private void BlockContinueButton()
    {
        _continueButton.SetActive(false);
        _restartButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -440, 0);
    }

    public void Restart()
    {
        LevelBehaviour.Instance.Restart();
    }

    public void Advertisement()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexAds.Instance.ShowRewardAd();
#endif
    }

    public void UpdateCoins(int count)
    {
        _coinText.text = count.ToString();
    }

    public void UpdateFiringRateCost(int cost) => _firingRateCostText.text = cost.ToString();

    public void UpdateDamageCost(int cost) => _damageCostText.text = cost.ToString();

    public void WebBulletDamageIncrease() => CoinManager.Instance.SpendMoney(ImprovementsBehaviour.Instance.CostOfDamageImprovements, Damage);

    public void WebBulletFiringRateIncrease() => CoinManager.Instance.SpendMoney(ImprovementsBehaviour.Instance.CostOfFiringRateImprovements, FiringRate);

    public void HitBoss(int _damageCount) 
    {
        Boss.Instance.TakeDamage(_damageCount);

        if (FindObjectOfType<BossFight>()._isFight == true)
        {
            PlayerAnimationController.Instance.BossHit();
            FindObjectOfType<BossFight>().Hit();
        }
    }
}
