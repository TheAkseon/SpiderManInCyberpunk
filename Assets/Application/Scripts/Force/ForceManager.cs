using TMPro;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public static ForceManager Instance;

    [SerializeField] private int _numberOfForce;
    [SerializeField] private TextMeshProUGUI _countForceText;
    public int NumberOfForce => _numberOfForce;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _countForceText.text = _numberOfForce.ToString();
    }

    public void AddForce(int value)
    {
        _numberOfForce += value;
        //SaveData.Instance.Data.Score += value;
        _countForceText.text = _numberOfForce.ToString();
        //SaveData.Instance.SaveYandex();
    }
}
