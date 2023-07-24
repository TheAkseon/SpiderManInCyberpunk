using System;
using UnityEngine;

public class WebBehaviour : MonoBehaviour
{
    private float _lifeTime = WebBullet.GetLifeTime();

    private void Start()
    {
        WebBullet.SetDamage(WebBullet.GetBaseDamage());
    }

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
    private static int _baseDamage = 5; // Íóæíî ñîõðàíÿòü
    private static readonly float _baseLifeTime = 1f;
    private static readonly float _baseSpeed = 30f;

    private static int _damage = 5;
    private static float _lifeTime = 1f;
    private static float _speed = 30f;

    public static int GetDamage() => _damage;
    public static int GetBaseDamage() => _baseDamage;
    public static void SetDamage(int value) => _damage = value;
    public static void ChangeBaseDamage(int value)
    {
        _baseDamage += value;
        SetDamage(_baseDamage);
    }
    public static void ChangeDamage(int value) /*ÊÈÐÈËËÓ ÍÅ ÏÎÊÀÇÛÂÀÒÜ*/ => /* <--- âîò ýòî*/ _damage = _damage + value < _baseDamage ? _baseDamage : _damage + value;

    public static float GetLifeTime() => _lifeTime;
    public static void ChangeLifeTime(float value) => _lifeTime = _lifeTime + value < _baseLifeTime ? _baseLifeTime : _lifeTime + value;

    public static float GetSpeed() => _speed;
    public static void ChangeSpeed(float value) => _speed = _speed + value < _baseSpeed ? _baseSpeed : _speed + value;
}
