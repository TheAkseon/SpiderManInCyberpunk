using System;
using UnityEngine;

public class ImprovementsBehaviour : MonoBehaviour
{
    public static ImprovementsBehaviour Instance;

    public float CostMultiplier = 1.5f;
    public int CostOfDamageImprovements = 10;
    public int CostOfFiringRateImprovements = 20;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UpdateView();
    }

    public void IncreaseCostOfDamageImprovements()
    {
        CostOfDamageImprovements = Convert.ToInt32(CostOfDamageImprovements * CostMultiplier);

        print("Increase " + CostOfDamageImprovements);
        UIBehaviour.Instance.UpdateDamageCost(CostOfDamageImprovements);
    }

    public void IncreaseCostOfFiringRateImprovements()
    {
        CostOfFiringRateImprovements = Convert.ToInt32(CostOfFiringRateImprovements * CostMultiplier);
        UIBehaviour.Instance.UpdateFiringRateCost(CostOfFiringRateImprovements);
    }

    public void UpdateView()
    {
        UIBehaviour.Instance.UpdateDamageCost(CostOfDamageImprovements);
        UIBehaviour.Instance.UpdateFiringRateCost(CostOfFiringRateImprovements);
    }
}
