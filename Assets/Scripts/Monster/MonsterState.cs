using DG.Tweening;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class MonsterState : IState<Monster>
{
    protected StateMachine<Monster> _stateMachine;

    public MonsterState(StateMachine<Monster> stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void OnEnter(Monster monster) { }
    public virtual void OnExit(Monster monster) { }
    public virtual void OnUpdate(Monster monster) { }
}

public class MonsterIdleState : MonsterState
{
    private Coroutine _changeMoveStateRoutine;

    public MonsterIdleState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnEnter(Monster monster)
    {
        monster.Animator.SetTrigger("Idle");
        _changeMoveStateRoutine = monster.StartCoroutine(ChangeMoveState());
    }

    public override void OnUpdate(Monster monster)
    {
        var target = monster.FindAttackTarget();
        if(target != null)
        {
            _stateMachine.ChangeState(new MonsterAttackState(_stateMachine, target));
        }
    }

    public override void OnExit(Monster monster)
    {
        if (_changeMoveStateRoutine != null)
        {
            monster.StopCoroutine(_changeMoveStateRoutine);
        }
    }

    private IEnumerator ChangeMoveState()
    {
        yield return WaitTimeManager.GetWaitTime(1);
        _stateMachine.ChangeState(new MonsterMoveState(_stateMachine));
    }
}

public class MonsterMoveState : MonsterState
{

    public MonsterMoveState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {

    }

    public override void OnUpdate(Monster monster)
    {
        var attackTarget = monster.FindAttackTarget();
        if (attackTarget != null)
        {
            _stateMachine.ChangeState(new MonsterAttackState(_stateMachine, attackTarget));
            return;
        }

        var chaseTarget = monster.FindChaseTarget();
        if (chaseTarget != null)
        {
            _stateMachine.ChangeState(new MonsterIdleState(_stateMachine));
        }
        else
        {
            var targetPosition = (chaseTarget as MonoBehaviour).transform.position;
            monster.SetDestination(targetPosition);
        }
    }
}


public class MonsterAttackState : MonsterState
{
    private Coroutine _checkRoutine;
    private ITargetable _target;

    public MonsterAttackState(StateMachine<Monster> stateMachine, ITargetable target) : base(stateMachine)
    {

    }

    public override void OnEnter(Monster monster)
    {
        monster.SetDestination(monster.transform.position); //∏ÿ√·√§∑Œ ∞¯∞›

        var targetPosition = (_target as MonoBehaviour).transform.position;
        targetPosition.y = monster.transform.position.y;
        monster.transform.LookAt(targetPosition);

        monster.Animator.SetTrigger("Attack");
        _checkRoutine = monster.StartCoroutine(CheckAttackEndRoutine(monster));
    }

    public override void OnExit(Monster monster)
    {
        if(_checkRoutine != null)
        {
            monster.StopCoroutine(_checkRoutine);
        }
    }

    private IEnumerator CheckAttackEndRoutine(Monster monster)
    {
        while (!monster.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }

        var attackAnim = monster.Animator.GetCurrentAnimatorStateInfo(0);
        if (attackAnim.IsName("Attack"))
            yield return WaitTimeManager.GetWaitTime(attackAnim.length);

        _stateMachine.ChangeState(new MonsterIdleState(_stateMachine));
    }
}

public class MonsterSpawnState : MonsterState
{
    public MonsterSpawnState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter(Monster monster)
    {
        monster.DisableCollider();
        DOTween.To(dissolve => monster.SetDissolve(dissolve), 0f, 1f, 2f)
               .OnComplete(() => 
               {
                   _stateMachine.ChangeState(new MonsterMoveState(_stateMachine));
                });
    }

    public override void OnExit(Monster monster)
    {
        monster.EnableCollider();
    }
}

public class MonsterDeadState : MonsterState
{
    public MonsterDeadState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter(Monster monster)
    {
        monster.DisableCollider();
        monster.SetDestination(monster.transform.position);

        DOTween.To(() => 1f, dissolve => monster.SetDissolve(dissolve), 0, 2)
               .OnComplete(() => monster.InActive());
    }
}