using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private int _damage;
    private float _lifeTime;
    private float _bulletSpeed;
    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    public float BulletSpeed => _bulletSpeed;

    private void Start()
    {
        _lifeTime = WebBulletBehaviour.Instance.LifeTime;
        _bulletSpeed = WebBulletBehaviour.Instance.BulletSpeed;
        _damage = WebBulletBehaviour.Instance.Damage;
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);

        if (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
