using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private ForceManager _forceManager;

    private List<Enemy> _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>().ToList();

        foreach (Enemy enemy in _enemies)
        {
            enemy.Die += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Die -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        PlayerModifier playerModifier = FindObjectOfType<PlayerModifier>();
        if (playerModifier)
        {
            playerModifier.AddWidth(enemy.BaseNumberOfHealth);
            playerModifier.AddHeight(enemy.BaseNumberOfHealth);
            _forceManager.AddForce(enemy.BaseNumberOfHealth);
            /*SoundsManager.Instance.PlaySound("EnemyHit");
            SoundsManager.Instance.PlaySound("Teleport");*/
        }

        enemy.Die -= OnEnemyDied;
        SoundsManager.Instance.PlaySound("EnemyDie");
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
