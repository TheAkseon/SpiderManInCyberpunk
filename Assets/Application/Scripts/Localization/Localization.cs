using Agava.YandexGames;
using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Localization : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    public static Localization Instance;

    //public event UnityAction LanguageChanged;

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

    private Dictionary<string, string> _language = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "tr", "Turkish" },
    };

    public void SetLanguage(string value)
    {
        if (_language.ContainsKey(value))
        {
            _leanLocalization.SetCurrentLanguage(_language[value]);
            //LanguageChanged?.Invoke();
        }
    }
}
