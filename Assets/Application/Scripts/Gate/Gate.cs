using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField] private int _value;
    [SerializeField] private DeformationType _deformationType;
    [SerializeField] private GateAppearaence _gateAppearaence;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private Transform _particlePosition;
    [SerializeField] private WebBulletBehaviour webBullet;

    private void OnValidate()
    {
        _gateAppearaence.UpdateVisual(_deformationType, _value);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerModifier player = other.attachedRigidbody.GetComponent<PlayerModifier>();

        if (player)
        {
            switch (_deformationType)
            {
                case DeformationType.Damage:
                    if (webBullet != null)
                    {
                        webBullet.ChangeBulletDamage(_value);
                        Debug.Log("Damage Changed");
                    }
                    break;
                case DeformationType.LifeTime:
                    if (webBullet != null)
                    {
                        webBullet.ChangeBulletLifeTime(_value);
                        Debug.Log("Life Time Changed");
                    }
                    break;
                /*case DeformationType.ShootMode:
                    WebShooting.instance.GetComponent<WebShooting>().ChangeShootMode();
                    Debug.Log("ShootMode Changed");
                    break;*/
                case DeformationType.BulletSpeed:
                    if (webBullet != null)
                    {
                        webBullet.ChangeBulletSpeed(_value);
                        Debug.Log("Life Time Changed");
                    }
                    break;
            }
            Instantiate(_effectPrefab, _particlePosition.position, transform.rotation);
            Destroy(gameObject);
        }

    }

}
