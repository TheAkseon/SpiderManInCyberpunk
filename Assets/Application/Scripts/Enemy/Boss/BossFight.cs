using Cinemachine;
using System.Collections;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField] private Transform _playerTargetPosition;
    [SerializeField] private float _speedChangePlayerPosition;
    [SerializeField] private GameObject _effectHitPrefab;
    [SerializeField] private Transform _particleHitPosition;
    [SerializeField] private ParticleSystem _effectDiePrefab;
    public CinemachineVirtualCamera _bossFightCamera;
    [SerializeField] private string _nameBossDiedSound;

    [Header("Player references")]
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    private Boss _boss;
    private Animator _animator;
    public bool _isFight = false;
    private bool _isPreFinish = false;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMove>().transform;
        _playerMove = FindObjectOfType<PlayerMove>();
        _playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_isPreFinish)
        {
            // ѕозици€ X постепенно мен€етс€ от текущего значени€ до 0
            float x = Mathf.MoveTowards(_player.transform.position.x, _player.transform.position.y, Time.deltaTime * 2f);
            _player.transform.position = new Vector3(x, _player.transform.position.y, _player.transform.position.z);

            // ѕоворот по Y постепенно мен€етс€ от текущего значени€ до 0
            float rotation = Mathf.MoveTowardsAngle(_player.transform.eulerAngles.y, 0, Time.deltaTime * 100f);
            _player.transform.localEulerAngles = new Vector3(0, rotation, 0);
        }
    }

    private void OnEnable()
    {
        _bossFightCamera.gameObject.SetActive(false);
        _playerCamera.gameObject.SetActive(true);

        _boss = FindObjectOfType<Boss>();
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _boss.Fight += OnBossFighted;
        _boss.Die += OnBossDied;
    }

    private void OnDisable()
    {
        _boss.Fight -= OnBossFighted;
        _boss.Die -= OnBossDied;
    }

    public void Hit()
    {
        SoundsManager.Instance.PlaySound("BossHit");
        Instantiate(_effectHitPrefab, _particleHitPosition.position, transform.rotation);
    }

    // —ражение началось
    private void OnBossFighted(Boss boss)
    {
        UIBehaviour.Instance.BossFight();
        _isPreFinish = true;
        _playerMove.StopMovement();
        PlayerAnimationController.Instance.Idle();
        SwitchCamera();
        Invoke(nameof(SetFight), 1f);
    }

    private void OnBossDied()
    {
        SoundsManager.Instance.FadeOut();
        SoundsManager.Instance.PlaySound(_nameBossDiedSound);
        _effectDiePrefab.Play();
        _animator.SetTrigger("Die");
        _isFight = false;
        PlayerAnimationController.Instance.Dance();
        Invoke(nameof(SetVictory), 2f);
    }

    private IEnumerator MoveTowardsTarget(Transform startPosition, Transform targetPosition, float speedChangePosition)
    {
        while (startPosition != targetPosition)
        {
            startPosition.position = Vector3.MoveTowards(startPosition.position, targetPosition.position, speedChangePosition);
            yield return null;
        }
    }

    void SwitchCamera()
    {
        _bossFightCamera.gameObject.SetActive(true);
        _playerCamera.gameObject.SetActive(false);
    }

    private void SetFight()
    {
        _isFight = true;
        UIBehaviour.Instance.BossFight();
    }

    private void SetVictory()
    {
        UIBehaviour.Instance.Victory();
    }
}
