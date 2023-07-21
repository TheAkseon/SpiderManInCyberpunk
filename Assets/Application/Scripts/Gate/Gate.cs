using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private GateType _deformationType;
    [SerializeField] private GateAppearaence _gateAppearaence;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private Transform _particlePosition;

    private void OnValidate() => _gateAppearaence.UpdateVisual(_deformationType, _value);

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.GetComponent<PlayerModifier>())
        {
            switch (_deformationType)
            {
                case GateType.Damage:
                        WebBullet.ChangeDamage(_value);
                    break;
                case GateType.LifeTime:
                        WebBullet.ChangeLifeTime(_value);
                    break;
                case GateType.BulletSpeed:
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
