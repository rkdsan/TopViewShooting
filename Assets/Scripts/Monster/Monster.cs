using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable, IMonsterModelListener
{
    public Animator Animator { get; private set; }

    [SerializeField] private MaterialPropertySetter _dissolveSetter;
    [SerializeField] private ProgressBar _healthBar;

    private NavMeshAgent _navAgent;
    private Collider _collider;
    private MonsterModel _monsterModel;
    private StateMachine<Monster> _stateMachine;
    private MonsterPool _pool;
    private TargetFinder _targetFinder;
    private List<ITargetable> _potentialTargets;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _stateMachine.Update();
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

    #region Public ÇÔ¼ö

    public void Init(MonsterSO monsterData)
    {
        _monsterModel = new MonsterModel(monsterData, this);

        _navAgent.Warp(transform.position);

        _stateMachine = new StateMachine<Monster>(this);
        _stateMachine.ChangeState(new MonsterSpawnState(_stateMachine));
    }

    public void WarpPosition(Vector3 newPosition)
    {
        _navAgent.Warp(newPosition);
    }

    public void SetTargetable(List<ITargetable> targets)
    {
        _potentialTargets = targets.ToList();
    }

    public ITargetable FindAttackTarget()
    {
        var target = _targetFinder.FindTarget(transform.position, _monsterModel.AttackRange, _potentialTargets);

        return target;
    }

    public ITargetable FindChaseTarget()
    {
        var target = _targetFinder.FindTarget(transform.position, _monsterModel.ChaseRange, _potentialTargets);

        return target;
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
        EventManager.TriggerEvent(EventType.MonsterDead, this);
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

    public void EnableCollider()
    {
        _collider.enabled = true;
    }
    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void SetDestination(Vector3 target)
    {
        _navAgent.SetDestination(target);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _monsterModel.AttackRange);
    }
}
