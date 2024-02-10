using Agava.YandexGames;
using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    public static PlayerModifier Instance;

    [SerializeField] private Transform _colliderTransform;
    [SerializeField] private Transform _playerModel;

    //[SerializeField] private AudioSource _increaseSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Die()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //здесь нужно добавить плашку через 3 2 1 реклама
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
