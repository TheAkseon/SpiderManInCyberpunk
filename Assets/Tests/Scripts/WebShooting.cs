using UnityEngine;

public class WebShooting : MonoBehaviour
{
    public static WebShooting Instance;

    [SerializeField] private GameObject Web;
    
    [SerializeField] private float _baseFiringRate = 1f; // Нужно сохранять
    [SerializeField] private float _firingRate = 1f;

    private Transform _playertransform;
    private string _shootMode = "SingleShootMode";
    private float _currentTimeBetweenShots;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _playertransform = GetComponent<Transform>();
        _firingRate = _baseFiringRate;
        _currentTimeBetweenShots = 1 / _firingRate;

        WebBullet.SetBaseDamage(SaveData.Instance.Data.BaseDamage);
        Debug.Log("Сейчас дамаг равен - " + SaveData.Instance.Data.BaseDamage);
        _firingRate = SaveData.Instance.Data.BaseFiringRate;
        Debug.Log("Сейчас скорость стрельбы равна - " + _firingRate);
        _baseFiringRate = _firingRate;
        WebBullet.SetDamage(WebBullet.GetBaseDamage());
        WebBullet.SetLifeTime(WebBullet.GetBaseLifeTime());

        ImprovementsBehaviour.Instance.UpdateView();
    }

    public void ChangeShootMode(GateType mode) => _shootMode = mode.ToString();
    public void ChangeBaseFiringRate(float value)
    {
        Debug.Log("обычный firing rate " + _firingRate);
        Debug.Log("базовый firing rate " + _baseFiringRate);
        _baseFiringRate += value;
        SetFiringRate(_baseFiringRate);
    }
    public void SetFiringRate(float value) => _firingRate = value;
    public float GetFiringRate() => _firingRate;
    public void ChangeFiringRate(float value)
    {
        _firingRate = _firingRate + value < _baseFiringRate ? _baseFiringRate : _firingRate + value;
    }

    void FixedUpdate()
    {
        //print("Damage: " + WebBullet.GetDamage() + "   LifeTime: " + WebBullet.GetLifeTime() + "\nFrequency: " + _firingRate + "   ShootMode: " + _shootMode);

        if (GetComponent<PlayerMove>().CanMove())
        {
            if (_currentTimeBetweenShots <= 0)
            {
                Quaternion rotation = _playertransform.rotation;
                Quaternion leftRotation = Quaternion.Euler(0f, -3f, 0f) * rotation;
                Quaternion rightRotation = Quaternion.Euler(0f, 3f, 0f) * rotation;

                switch (_shootMode)
                {
                    case "SingleShootMode":
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), rotation);
                        break;
                    case "DoubleShootMode":
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), rotation);
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), leftRotation);
                        break;
                    case "TripleShootMode":
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), rotation);
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), leftRotation);
                        Instantiate(Web, _playertransform.position + new Vector3(0f, 1f, 0.5f), rightRotation);
                        break;
                    default:
                        Debug.LogError("Uncorrect shoot mode!");
                        break;
                }

                _currentTimeBetweenShots = 1 / _firingRate;
            }
            _currentTimeBetweenShots -= Time.fixedDeltaTime;
        } 
    }
}