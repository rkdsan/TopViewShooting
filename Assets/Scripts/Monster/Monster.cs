using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable, IMonsterModelListener
{
    public Transform Target { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public Animator Animator { get; private set; }
    public Collider Collider { get; private set; }
    public bool IsAlive => _monsterModel.IsAlive;
    public float AttackRange => _monsterModel.AttackRange;

    public delegate void MonsterEventHandler(Monster sender);
    public event MonsterEventHandler MonsterDeadEvent;

    [SerializeField] private MaterialPropertySetter _dissolveSetter;
    [SerializeField] private ProgressBar _healthBar;

    private MonsterModel _monsterModel;
    private StateMachine<Monster> _stateMachine;
    private MonsterPool _pool;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavAgent = GetComponent<NavMeshAgent>();
        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void Init(MonsterSO monsterData)
    {
        _monsterModel = new MonsterModel(monsterData, this);

        NavAgent.Warp(transform.position);

        _stateMachine = new StateMachine<Monster>(this);
        _stateMachine.ChangeState(new MonsterSpawnState(_stateMachine));
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetPool(MonsterPool pool)
    {
        _pool = pool;
    }

    public void TakeDamage(int damage)
    {
        _monsterModel.TakeDamage(damage);
    }

    public void AnimationEvent_Attack()
    {
        Attack();
    }

    public void SetDissolve(float value)
    {
        _dissolveSetter.SetValue("_Dissolve", value);
    }

    public void OnChangeHP(int currentHP, int maxHP)
    {
        _healthBar.UpdateBar(currentHP, maxHP);
    }

    public void OnDead()
    {
        MonsterDeadEvent(this);
        GameEventManager.TriggerEvent(GameEventType.MonsterDead, this);
        _stateMachine.ChangeState(new MonsterDeadState(_stateMachine));
    }

    public void InActive()
    {
        if (_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        var attackRange = Vector3.one + Vector3.forward * 2;
        var hits = Physics.BoxCastAll(transform.position, attackRange, transform.forward);
        var players = hits.Select(hit => hit.transform.GetComponent<PlayerController>()).Where(player => player != null);

        foreach (var player in players)
        {
            player.TakeDamage(_monsterModel.AttackPower);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
