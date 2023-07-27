using System;
using UnityEngine;

public class ImprovementsBehaviour : MonoBehaviour
{
    public static ImprovementsBehaviour Instance;

    public float CostMultiplier = 1.2f;
    
    public int CostOfDamageImprovements = 10;  // Нужно сохранять
    public int CostOfFiringRateImprovements = 20;  // Нужно сохранять

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void IncreaseCostOfDamageImprovements()
    {
        CostOfDamageImprovements = Convert.ToInt32(CostOfDamageImprovements * CostMultiplier);
        UpdateView();
    }

    public void IncreaseCostOfFiringRateImprovements()
    {
        CostOfFiringRateImprovements = Convert.ToInt32(CostOfFiringRateImprovements * CostMultiplier);
        UpdateView();
    }

    public void UpdateView() => UIBehaviour.Instance.UpdateImprovements(CostOfFiringRateImprovements, CostOfDamageImprovements);
}
