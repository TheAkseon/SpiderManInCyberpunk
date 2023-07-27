using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _bossPrefabs;

    private GameObject _boss;

    private void Start()
    {
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        GameObject _randomBossPrefab = _bossPrefabs[Random.Range(0, _bossPrefabs.Length)];
        _boss = Instantiate(_randomBossPrefab, gameObject.transform);
        _boss.transform.position = new Vector3(0f, _randomBossPrefab.transform.position.y, 91f);
    }
}
