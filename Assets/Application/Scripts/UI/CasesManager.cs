using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CasesManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject _adsButton;
    [SerializeField] private GameObject _exitButton;

    [Header("Random")]
    [SerializeField] private int _minValue = 100;
    [SerializeField] private int _maxValue = 200;

    [SerializeField] private GameObject[] _itemGameObjects;
    [SerializeField] private List<Item> _items = new();
    [SerializeField] private int _freeCaseCount = 2;

    private int _openedCases = 0;
    private int _amount = 0;

    private void Start()
    {
        _adsButton.SetActive(false);

        foreach (var itemGameObject in _itemGameObjects)
        {
            _items.Add(new Item
            {
                openImage = itemGameObject.transform.GetChild(0).GetComponent<Image>(),
                closedImage = itemGameObject.transform.GetChild(1).GetComponent<Image>(),
                textImage = itemGameObject.transform.GetChild(2).gameObject,
                adsIcon = itemGameObject.transform.GetChild(2).GetChild(1).gameObject,
                text = itemGameObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>(),
            });
        }

        GenerateCases();
    }

    public void GenerateCases()
    {
        foreach (var item in _items)
        {
            item.openImage.gameObject.SetActive(false);
            item.textImage.SetActive(false);

            item.value = UnityEngine.Random.Range(_minValue, _maxValue);
            item.text.text = item.value.ToString();
        }
    }

    public void TryOpenCase(int buttonId)
    {
        if (!_items[buttonId].isOpened)
        {
            if (_openedCases < _freeCaseCount)
            {
                OpenCase(buttonId);

                if(_adsButton.activeSelf == false)
                {
                    _adsButton.SetActive(true);
                }

                if (_openedCases == _freeCaseCount)
                {
                    SetAds();
                }
            }
            else
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                YandexAds.Instance.ShowRewardAd();
                StartCoroutine(CheckRewarded(buttonId));
#else
                OpenCase(buttonId);
#endif

                if (_adsButton.activeSelf == false)
                {
                    _adsButton.SetActive(true);
                }
            }
        }
    }

    private IEnumerator CheckRewarded(int buttonId)
    {
        while(YandexAds.Instance.IsRewarded == false)
        {
            yield return null;
        }

        OpenCase(buttonId);
    }

    private IEnumerator CheckRewarded()
    {
        while (YandexAds.Instance.IsRewarded == false)
        {
            yield return null;
        }

        SaveData.Instance.Data.Coins += _amount;
        CoinManager.Instance.UpdateView();
        UIBehaviour.Instance.UpdateCoins(SaveData.Instance.Data.Coins);
        _adsButton.SetActive(false);
    }

    private void OpenCase(int buttonId)
    {
        _items[buttonId].closedImage.gameObject.SetActive(false);
        _items[buttonId].openImage.gameObject.SetActive(true);
        _items[buttonId].textImage.SetActive(true);
        _items[buttonId].text.gameObject.SetActive(true);
        _items[buttonId].adsIcon.SetActive(false);
        _items[buttonId].isOpened = true;

        _openedCases++;
        _amount += _items[buttonId].value;
        SaveData.Instance.Data.Coins += _items[buttonId].value;
        UIBehaviour.Instance.UpdateCoins(SaveData.Instance.Data.Coins);
    }

    public void SetAds()
    {
        foreach (var item in _items)
        {
            if (!item.isOpened)
            {
                item.textImage.SetActive(true);
                item.adsIcon.SetActive(true);
            }
        }

        _adsButton.SetActive(true);
        _exitButton.SetActive(true);
    }

    public void WatchAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexAds.Instance.ShowRewardAd();
        StartCoroutine(CheckRewarded());
#else
        SaveData.Instance.Data.Coins += _amount;
        CoinManager.Instance.UpdateView();
        UIBehaviour.Instance.UpdateCoins(SaveData.Instance.Data.Coins);
        _adsButton.SetActive(false);
#endif
    }

    public void ExitCases()
    {
        //здесь можно добавить рекламу, но только с плашкой 3 2 1 реклама
        gameObject.SetActive(false);
        LevelBehaviour.Instance.NextLevel();
        SaveData.Instance.SaveYandex();
    }
}

[Serializable]
public class Item
{
    public Image closedImage;
    public Image openImage;
    public GameObject textImage;
    public GameObject adsIcon;
    public TextMeshProUGUI text;
    public int value = 0;
    public bool isOpened = false;
}
