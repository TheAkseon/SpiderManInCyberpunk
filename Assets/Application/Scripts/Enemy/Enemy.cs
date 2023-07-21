using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _numberOfHealth;
    [SerializeField] private TextMeshProUGUI _countHealthText;
    [SerializeField] private GameObject _hitEffectPrefab;
    [SerializeField] private GameObject _dieEffectPrefab;
    [SerializeField] private Transform _particlePosition;

    private int _baseNumberOfHealth;

    public int BaseNumberOfHealth => _baseNumberOfHealth;

    public event UnityAction<Enemy> Die;
    private void Start()
    {
        _baseNumberOfHealth = _numberOfHealth;
        _countHealthText.text = _numberOfHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerModifier player))
        {
            Instantiate(_dieEffectPrefab, _particlePosition.position, transform.rotation);
            PlayerModifier.Instance.Die();
            Destroy(gameObject);
        }

        if (other.gameObject.TryGetComponent(out Web webBullet))
        {
            SoundsManager.Instance.PlaySound("WebHit");
            Destroy(other.gameObject);
            Instantiate(_hitEffectPrefab, _particlePosition.position, transform.rotation);
            GetDamage(webBullet.Damage);
        }
    }

    public void GetDamage(int damage)
    {
        if (_numberOfHealth <= 0)
        {
            Instantiate(_dieEffectPrefab, _particlePosition.position, transform.rotation);
            Die?.Invoke(this);
        }
        else
        {
            _numberOfHealth -= damage;

            if (_numberOfHealth > 0)
            {
                _countHealthText.text = _numberOfHealth.ToString();
            }
        }
    }
    public void SetHealth(int value)
    {
        _numberOfHealth = value;
        _baseNumberOfHealth = value;
        _countHealthText.text = _numberOfHealth.ToString();
    }
}
