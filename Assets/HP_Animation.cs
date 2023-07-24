using TMPro;
using UnityEngine;

public class HP_Animation : MonoBehaviour
{
    [SerializeField] private GameObject _flyingHPCanvasPrefab;

    [SerializeField] private float _xPositionOffset = -0.39f;
    [SerializeField] private float _yPositionOffset = 0.1f;
    [SerializeField] private float _maxPosition = 1f;
    [SerializeField] private float _maxRotation = 0.5f;

    private TextMeshProUGUI _hpText;

    private Vector3 _randomPosition;
    private Quaternion _randomRotation;

    public void SpawnCanvas(Transform _player, int value)
    {
        GameObject canvas = Instantiate(_flyingHPCanvasPrefab, _player);

        _randomPosition = new Vector3(_xPositionOffset + Random.Range(-_maxPosition, _maxPosition), _yPositionOffset + canvas.transform.position.y, canvas.transform.position.z);
        _randomRotation = new Quaternion(canvas.transform.rotation.x, canvas.transform.rotation.y, Random.Range(-_maxRotation, _maxRotation), canvas.transform.rotation.w);

        _hpText = canvas.transform.GetComponentInChildren<TextMeshProUGUI>();
        _hpText.text = value.ToString();

        canvas.transform.SetPositionAndRotation(_randomPosition, _randomRotation);
    }
}
