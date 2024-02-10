using System.Collections.Generic;
using UnityEngine;

public class HealthGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _healthMultiplier = new(1.3f, 1.6f);

    private readonly List<GameObject> _enemies = new();

    private float _health = 4;

    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            _enemies.Add(gameObject.transform.GetChild(i).gameObject);
        }

        GenerateHealth();
    }

    public void GenerateHealth()
    {
        foreach(GameObject enemy in _enemies)
        {
            enemy.GetComponent<Enemy>().SetHealth(Mathf.CeilToInt(_health));
            _health = Random.Range(_health * _healthMultiplier.x, _health * _healthMultiplier.y);
        }
    }
}
