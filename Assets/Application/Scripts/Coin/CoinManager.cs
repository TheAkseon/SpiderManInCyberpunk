using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private int _forceAmount = 15;

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
    public void SpendMoney(int value, string typeOfReduction) {
        if (value <= SaveData.Instance.Data.Coins)
        {
            SaveData.Instance.Data.Coins -= value;
            if (typeOfReduction.Equals("width"))
            {
                ImprovementsBehaviour.Instance.IncreaseCostOfWidthImprovements();
                PlayerModifier.Instance.AddWidth(_forceAmount);
            }
            else if (typeOfReduction.Equals("height"))
            {
                ImprovementsBehaviour.Instance.IncreaseCostOfHeightImprovements();
                PlayerModifier.Instance.AddHeight(_forceAmount);
            }
            ForceManager.Instance.AddForce(_forceAmount);

            UpdateView();
        }
    }

    public void UpdateView()
    {
        UIBehaviour.Instance.UpdateCoins(SaveData.Instance.Data.Coins);
    }
}
