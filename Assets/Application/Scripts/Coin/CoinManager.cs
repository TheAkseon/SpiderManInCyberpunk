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
        print(value + " " + SaveData.Instance.Data.Coins);
        if (value <= SaveData.Instance.Data.Coins)
        {
            SaveData.Instance.Data.Coins -= value;
            if (typeOfReduction.Equals(Damage))
            {
                ImprovementsBehaviour.Instance.IncreaseCostOfDamageImprovements();
                WebBullet.ChangeDamage(Convert.ToInt32(UIBehaviour.Instance._damageImprovementCount.text));
            }
            else if (typeOfReduction.Equals(FiringRate))
            {
                ImprovementsBehaviour.Instance.IncreaseCostOfFiringRateImprovements();
                WebShooting.Instance.ChangeFiringFrequency(Convert.ToSingle(UIBehaviour.Instance._firingRateImprovementCount.text));
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
