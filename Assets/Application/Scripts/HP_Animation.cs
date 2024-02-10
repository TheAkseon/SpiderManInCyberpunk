using TMPro;
using UnityEngine;

public class HP_Animation : MonoBehaviour
{
    [SerializeField] private GameObject _flyingHPCanvasPrefab;

    [Header("Position")]
    [SerializeField] private float _xPositionOffset = 0f;
    [SerializeField] private float _yPositionOffset = 0.1f;
    [SerializeField] private float _zPositionOffset = 0.1f;
    [SerializeField] private float _maxPosition = 0.25f;

    private TextMeshProUGUI _hpText;

    private Vector3 _randomPosition;

    public void SpawnCanvas(Transform _player, int value)
    {
        GameObject canvas = Instantiate(_flyingHPCanvasPrefab, _player);

        _randomPosition = new Vector3(_xPositionOffset + Random.Range(-_maxPosition, _maxPosition), _yPositionOffset, _zPositionOffset);

        _hpText = canvas.transform.GetComponentInChildren<TextMeshProUGUI>();
        _hpText.text = value.ToString();

        canvas.transform.localPosition = _randomPosition;
    }
}
