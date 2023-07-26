using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private GateType _deformationType;
    [SerializeField] private GateAppearaence _gateAppearaence;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private Transform _particlePosition;

    private void OnValidate() => _gateAppearaence.UpdateVisual(_deformationType, _value);

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModifier>())
        {
            switch (_deformationType)
            {
                case GateType.Damage:
                        WebBullet.ChangeDamage(Convert.ToInt32(_value));
                    break;
                case GateType.LifeTime:
                    _value /= 2;    
                    WebBullet.ChangeLifeTime(_value);
                    break;
                case GateType.FiringFrequency:
                    other.GetComponent<WebShooting>().ChangeFiringFrequency(_value);
                    break;
                default:
                    other.GetComponent<WebShooting>().ChangeShootMode(_deformationType);
                    break;
            }
            Instantiate(_effectPrefab, _particlePosition.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
