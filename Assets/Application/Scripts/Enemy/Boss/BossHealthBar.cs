using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private float _speedChangeValue = 0.8f;
    private Coroutine _changeHealth;

    private void Start()
    {
        _healthBar.maxValue = _boss.MaxHealth;
        _healthBar.value = _boss.Health;
        _healthBar.minValue = _boss.MinHealth;
    }

    private void OnEnable()
    {
        _boss = FindObjectOfType<Boss>();
        _boss.HealthChanged += OnHealthChanged;
    }


    private void OnDisable()
    {
        _boss.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        if (_changeHealth != null)
        {
            StopCoroutine(_changeHealth);
        }

        _changeHealth = StartCoroutine(ChangeHealth(health));
    }

    private IEnumerator ChangeHealth(float target)
    {
        while (_healthBar.value != target)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, target, _speedChangeValue);
            yield return null;
        }
    }
}
