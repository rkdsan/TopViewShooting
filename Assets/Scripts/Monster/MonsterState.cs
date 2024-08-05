using System.Collections;
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
    public MonsterIdleState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnEnter(Monster monster)
    {
        monster.Animator.SetTrigger("Idle");
        m
    }

    public override void OnUpdate(Monster monster)
    {
        if (MonsterAttackState.CanAttack(monster))
        {
            _stateMachine.ChangeState(new MonsterAttackState(_stateMachine));
        }
    }
}

public class MonsterMoveState : MonsterState
{
    private Coroutine _moveRoutine;

    public MonsterMoveState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter(Monster monster)
    {
        _moveRoutine = monster.StartCoroutine(MoveRoutine(monster));
    }

    public override void OnUpdate(Monster monster)
    {
        if (MonsterAttackState.CanAttack(monster))
        {
            _stateMachine.ChangeState(new MonsterAttackState(_stateMachine));
        }
    }

    public override void OnExit(Monster monster)
    {
        if (_moveRoutine != null)
            monster.StopCoroutine(_moveRoutine);
    }

    private IEnumerator MoveRoutine(Monster monster)
    {
        while (true)
        {
            monster.NavAgent.SetDestination(monster.Target.position);
            yield return WaitTimeManager.GetWaitTime(2);
        }
    }

}


public class MonsterAttackState : MonsterState
{
    private Coroutine _checkRoutine;

    public MonsterAttackState(StateMachine<Monster> stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter(Monster monster)
    {
        var targetPos = monster.Target.position;
        targetPos.y = monster.transform.position.y;
        monster.transform.LookAt(targetPos);

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

        while (monster.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }

        _stateMachine.ChangeState(new MonsterIdleState(_stateMachine));
    }

    public static bool CanAttack(Monster monster)
    {
        var positionGap = monster.transform.position - monster.Target.position;

        return positionGap.sqrMagnitude < 9;
    }
}
