using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss Instance;
    
    [SerializeField] private int _numberOfForce;
    [SerializeField] private TextMeshProUGUI _countForceText;
    [SerializeField] private ForceManager _forceManager;
    [SerializeField] private GameObject _hitEffectPrefab;

    private bool _isNeedDie = true;

    public event UnityAction<Boss> Fight;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Die;

    public int Health { get; private set; } = 100;
    public int MaxHealth { get; private set; } = 100;
    public int MinHealth { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _forceManager = FindObjectOfType<ForceManager>();
    }

    private void Start()
    {
        _numberOfForce = 30 + SceneManager.GetActiveScene().buildIndex * 5;
        _countForceText.text = _numberOfForce.ToString();
    }

    public void TakeDamage(int amountDifference)
    {
        if (FindObjectOfType<BossFight>()._isFight)
        {
            CameraShake();
            Health -= amountDifference;

            if (Health < MinHealth)
            {
                if (_isNeedDie)
                {
                    _isNeedDie = false;
                    Health = MinHealth;
                    Die?.Invoke();
                }
            }

            HealthChanged?.Invoke(Health);
        }
    }

    private void CameraShake()
    {
        GetComponent<BossFight>()._bossFightCamera.GetComponent<Animator>().SetTrigger("Shake");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WebBehaviour webBullet))
        {
            Instantiate(_hitEffectPrefab, other.gameObject.transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.gameObject.TryGetComponent(out PlayerModifier playerModifier))
        {
            if (_numberOfForce < _forceManager.NumberOfForce)
            {
                Fight?.Invoke(this);
            }
            else if (_numberOfForce >= _forceManager.NumberOfForce)
            {
                Destroy(other.gameObject);
                UIBehaviour.Instance.GameOver(true);
            }
        }
    }
}
