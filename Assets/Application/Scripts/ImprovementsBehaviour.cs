using UnityEngine;

public class ImprovementsBehaviour : MonoBehaviour
{
    public static ImprovementsBehaviour Instance;

    public int CostOfHeightImprovements = 10;
    public int CostOfWidthImprovements = 20;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UpdateView();
    }

    public void IncreaseCostOfHeightImprovements()
    {
        CostOfHeightImprovements *= 2;
        UIBehaviour.Instance.UpdateHeightCost(CostOfHeightImprovements);
    }

    public void IncreaseCostOfWidthImprovements()
    {
        CostOfWidthImprovements *= 2;
        UIBehaviour.Instance.UpdateWidthCost(CostOfWidthImprovements);
    }

    public void UpdateView()
    {
        UIBehaviour.Instance.UpdateHeightCost(CostOfHeightImprovements);
        UIBehaviour.Instance.UpdateWidthCost(CostOfWidthImprovements);
    }
}
