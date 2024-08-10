using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private Weapon _weapon;
    private PlayerView _playerView;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;

    private Camera _mainCamera;
    private PlayerModel _playerModel;

    private IPlayerUI _playerUI;
    private Vector2 _moveInputVector;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerView = GetComponent<PlayerView>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Move();
    }

    public void SetPlayerUI(IPlayerUI playerUI)
    {
        _playerUI = playerUI;
        _playerUI?.UpdateHP(_playerModel.CurrentHP, _playerModel.MaxHP);
    }

    public void TakeDamage(int damage)
    {
        _playerModel.TakeDamage(damage);
        _playerUI?.UpdateHP(_playerModel.CurrentHP, _playerModel.MaxHP);
    }

    public void Init(PlayerSO playerData)
    {
        _playerModel = new PlayerModel(playerData);
        GameEventManager.Attach(GameEventType.SetActivePlayerInput, OnSetActivePlayerInput);
    }

    private void Move()
    {
        var moveVector = _moveInputVector * _playerModel.MoveSpeed * Time.deltaTime;
        var addPosition = new Vector3(moveVector.x, 0, moveVector.y);

        _rigidbody.MovePosition(transform.position + addPosition);
        _playerView.OnPlayerMove(moveVector);
    }

    private void Rotation(Vector2 mousePosition)
    {
        var myPosition = _mainCamera.WorldToScreenPoint(transform.position);

        var angle = Mathf.Atan2(mousePosition.y - myPosition.y, mousePosition.x - myPosition.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
    }

    #region PlayerInput

    private void OnSetActivePlayerInput(object isActive)
    {
        if (_playerInput != null)
        {
            _playerInput.enabled = (bool)isActive;
        }
    }
    
    private void OnMove(InputValue inputValue)
    {
        _moveInputVector = inputValue.Get<Vector2>().normalized;
    }

    private void OnMousePosition(InputValue inputValue)
    {
        var mousePosition = inputValue.Get<Vector2>();
        Rotation(mousePosition);
    }

    private void OnAttack()
    {
        _weapon.Attack();
        _playerView.OnPlayerAttack();
    }

    #endregion
}
