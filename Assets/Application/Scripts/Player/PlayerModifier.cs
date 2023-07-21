using Agava.YandexGames;
using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    public static PlayerModifier Instance;

    [SerializeField] private int _width;
    [SerializeField] private int _height;
    private float _widthMultiplier = 0.003f;
    private float _heightMultiplier = 0.003f;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _colliderTransform;
    [SerializeField] private Transform _playerModel;

    //[SerializeField] private AudioSource _increaseSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        _playerModel.localScale = new Vector3(1.0f + _width * _widthMultiplier, 1.0f + _height * _heightMultiplier, 1.0f + _width * _widthMultiplier);
        _colliderTransform.localScale = new Vector3(1.0f + _width * _widthMultiplier, 1.0f + _height * _heightMultiplier, 1.0f + _width * _widthMultiplier);

        if (_width <= 0 || _height <= 0)
        {
            Die();
        }
    }

    public void AddWidth(int value)
    {
        _width += value;
        if (value > 0)
        {
            //_increaseSound.Play();
        }
    }

    public void AddHeight(int value)
    {
        _height += value;
        if (value > 0)
        {
            //_increaseSound.Play();
        }
    }

    public void 
        Barrier()
    {
        if (_height > 0)
        {
            _height -= 50;
        }
        else if (_width > 0)
        {
            _width -= 50;
            UpdateWidth();
        }
        else
        {
            Die();
        }
    }

    void UpdateWidth()
    {
        _renderer.material.SetFloat("_PushValue", _width * _widthMultiplier);
    }

    public void Die()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexAds.Instance.ShowInterstitial();
#endif
        UIBehaviour.Instance.GameOver(false);
        gameObject.SetActive(false);
    }

    public void Reberth()
    {
        gameObject.SetActive(true);
    }
}
