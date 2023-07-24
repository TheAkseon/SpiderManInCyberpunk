using UnityEngine;

public class WebShooting : MonoBehaviour
{
    public static WebShooting Instance;

    [SerializeField] private GameObject Web;
    
    [SerializeField] private float _baseFiringFrequency = 1f; // Нужно сохранять
    [SerializeField] private float _firingFrequency = 1f;

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
        _firingFrequency = _baseFiringFrequency;
        _currentTimeBetweenShots = 1 / _firingFrequency;
    }

    public void ChangeShootMode(GateType mode) => _shootMode = mode.ToString();
    public void ChangeBaseFiringFrequency(float value)
    {
        _baseFiringFrequency += value;
        SetFiringFrequency(_baseFiringFrequency);
    }
    public void SetFiringFrequency(float value) => _firingFrequency = value;
    public void ChangeFiringFrequency(float value) => _firingFrequency = _firingFrequency + value < _baseFiringFrequency ? _baseFiringFrequency : _firingFrequency + value;

    void FixedUpdate()
    {
        print("Damage: " + WebBullet.GetDamage() + "   LifeTime: " + WebBullet.GetLifeTime() + "\nFrequency: " + _firingFrequency + "   ShootMode: " + _shootMode);

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

                _currentTimeBetweenShots = 1 / _firingFrequency;
            }
            _currentTimeBetweenShots -= Time.fixedDeltaTime;
        } 
    }
}