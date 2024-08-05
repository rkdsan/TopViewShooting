using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _moveSpeed = 3;
    private PlayerView _playerView;
    private Rigidbody _rigidbody;

    private Camera _mainCamera;
    private PlayerModel _playerModel;

    private IPlayerUI _playerUI;
    private Vector2 _moveInputVector;
    
    public void SetPlayerUI(IPlayerUI playerUI)
    {
        _playerUI = playerUI;
    }

    public void TakeDamage(int damage)
    {
        _playerModel.TakeDamage(damage);
        _playerUI?.UpdateHP(_playerModel.CurrentHP, _playerModel.MaxHP);
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerView = GetComponent<PlayerView>();
        _rigidbody = GetComponent<Rigidbody>();

        Init();
    }

    private void Update()
    {
        Move();
        Rotation();
    }

    private void Init()
    {
        _playerModel = new PlayerModel();
        _playerUI?.UpdateHP(_playerModel.CurrentHP, _playerModel.MaxHP);
    }

    private void Move()
    {
        var moveVector = _moveInputVector * _moveSpeed * Time.deltaTime;
        var addPosition = new Vector3(moveVector.x, 0, moveVector.y);

        _rigidbody.MovePosition(transform.position + addPosition);
        _playerView.OnPlayerMove(moveVector);
    }

    private void Rotation()
    {
        var myPosition = _mainCamera.WorldToScreenPoint(transform.position);
        var mousePosition = Input.mousePosition;

        var angle = Mathf.Atan2(mousePosition.y - myPosition.y, mousePosition.x - myPosition.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
    }

    #region PlayerInput
    private void OnMove(InputValue inputValue)
    {
        _moveInputVector = inputValue.Get<Vector2>().normalized;
    }

    private void OnAttack()
    {
        _weapon.Attack();
        _playerView.OnPlayerAttack();
    }

    private void OnJump()
    {

    }

    #endregion
}
