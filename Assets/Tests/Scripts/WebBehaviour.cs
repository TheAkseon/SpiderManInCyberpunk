using System;
using UnityEngine;

public class WebBehaviour : MonoBehaviour
{
    private float _lifeTime = WebBullet.GetLifeTime();

    private void FixedUpdate()
    { 
        transform.Translate(Time.deltaTime * WebBullet.GetSpeed() * Vector3.forward);

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

[Serializable]
public static class WebBullet
{
    private static int _baseDamage = 1; // Нужно сохранять
    private static float _baseLifeTime = 0.5f;
    private static readonly float _baseSpeed = 30f;

    private static int _damage;
    private static float _lifeTime;
    private static float _speed = 30f;

    public static int GetDamage() => _damage;
    public static void SetDamage(int value) => _damage = value;
    public static void ChangeDamage(int value) => _damage = _damage + value < _baseDamage ? _baseDamage : _damage + value;
    
    public static int GetBaseDamage() => _baseDamage;
    public static void ChangeBaseDamage(int value)
    {
        _baseDamage += value;
        SetDamage(_baseDamage);
    }

    public static void SetBaseDamage(int value)
    {
        _baseDamage = value;
    }

    public static float GetLifeTime() => _lifeTime;
    public static void SetLifeTime(float value) => _lifeTime = value;
    public static void ChangeLifeTime(float value) => _lifeTime = _lifeTime + value < _baseLifeTime ? _baseLifeTime : _lifeTime + value;

    public static float GetBaseLifeTime() => _baseLifeTime;

    public static float GetSpeed() => _speed;
    public static void ChangeSpeed(float value) => _speed = _speed + value < _baseSpeed ? _baseSpeed : _speed + value;
}
