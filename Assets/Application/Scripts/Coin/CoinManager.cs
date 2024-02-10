using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private readonly string Damage = "width";
    private readonly string FiringRate = "height";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateView();
    }

    public void AddMoney(int value)
    {
        SaveData.Instance.Data.Coins += value;
        SaveData.Instance.SaveYandex();
        UpdateView();
    }

    // 1-width 2-height
    public void SpendMoney(int value, string typeOfReduction)
    {
        if (value <= SaveData.Instance.Data.Coins)
        {
            SaveData.Instance.Data.Coins -= value;
            if (typeOfReduction.Equals(Damage))
            {
                string text = UIBehaviour.Instance._damageImprovementCount.text;
                text = text[1..];
                print(text);
                WebBullet.ChangeBaseDamage(Convert.ToInt32(text));
                SaveData.Instance.Data.BaseDamage = WebBullet.GetBaseDamage();
                SaveData.Instance.SaveYandex();
                ImprovementsBehaviour.Instance.IncreaseCostOfDamageImprovements();
            }
            else if (typeOfReduction.Equals(FiringRate))
            {
                string text = UIBehaviour.Instance._firingRateImprovementCount.text;
                text = text[1..];
                print(text);
                WebShooting.Instance.ChangeBaseFiringRate(Convert.ToSingle(text));
                SaveData.Instance.Data.BaseFiringRate = WebShooting.Instance.GetFiringRate();
                SaveData.Instance.SaveYandex();
                ImprovementsBehaviour.Instance.IncreaseCostOfFiringRateImprovements();
            }

            SoundsManager.Instance.PlaySound("ImprovePerfomance");
            UpdateView();
        }
    }

    public void UpdateView()
    {
        UIBehaviour.Instance.UpdateCoins(SaveData.Instance.Data.Coins);
    }
}
