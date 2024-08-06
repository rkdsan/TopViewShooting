using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : MonoBehaviour, IDamageable
{
    public Transform Target { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public Animator Animator { get; private set; }
    public bool IsDead { get; private set; }
    public float AttackRange { get; private set; }

    public delegate void MonsterEventHandler(Monster sender);
    public event MonsterEventHandler MonsterDeadEvent;

    [SerializeField] private ProgressBar _healthBar;

    private StateMachine<Monster> _stateMachine;
    private MonsterFactory _pool;

    private int _currentHealth;
    private int _maxHealth;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void Init()
    {
        AttackRange = 4;
        _currentHealth = _maxHealth = 5;
        _stateMachine = new StateMachine<Monster>(this);
        _stateMachine.ChangeState(new MonsterIdleState(_stateMachine));
        SetHealth(_currentHealth);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetPool(MonsterFactory pool)
    {
        _pool = pool;
    }

    public void TakeDamage(int damage)
    {
        SetHealth(_currentHealth - damage);
    }

    public void AnimationEvent_Attack()
    {
        Attack();
    }

    private void Attack()
    {
        var attackRange = Vector3.one + Vector3.forward * 2;
        var hits = Physics.BoxCastAll(transform.position, attackRange, transform.forward);
        var players = hits.Select(hit => hit.transform.GetComponent<PlayerController>()).Where(player => player != null);

        foreach (var player in players)
        {
            player.TakeDamage(1);
        }
    }

    private void SetHealth(int newHealth)
    {
        _currentHealth = Math.Max(0, newHealth);
        _healthBar.UpdateBar(_currentHealth, _maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        MonsterDeadEvent(this);
        GameEventManager.TriggerEvent(this, GameEventType.OnMonsterDead);

        if(_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
