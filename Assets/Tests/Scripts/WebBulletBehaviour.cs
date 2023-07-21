using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebBulletBehaviour : MonoBehaviour
{
    private int _baseDamage = 5;
    private float _baseLifeTime = 1f;
    private float _baseSpeed = 30f;

    public static WebBulletBehaviour Instance;
    private float _lifeTime = 1.5f;
    private float _bulletSpeed;
    private int _damage;

    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    public float BulletSpeed => _bulletSpeed;   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _damage = _baseDamage;
        _lifeTime = _baseLifeTime;
        _bulletSpeed = _baseSpeed;
    }

    
    public void ChangeBulletDamage(int changeValue)
    {
        _damage += changeValue;
        if (_damage < 0)
        {
            _damage = _baseDamage;
        }
    }
    public void ChangeBulletLifeTime(float changeValue)
    {
        _lifeTime += changeValue;
        if (_lifeTime < 0)
        {
            _lifeTime = _baseLifeTime;
        }
    }
    public void ChangeBulletSpeed(float changeValue)
    {
        _bulletSpeed += changeValue;
        if (_bulletSpeed < 0)
        {
            _bulletSpeed = _baseSpeed;
        }
    }
}
