using System.Collections.Generic;
using UnityEngine;

public class TargetFinder
{
    public ITargetable FindTarget(Vector3 position, float range, List<ITargetable> potentialTargets)
    {
        ITargetable nearestTarget = null;
        var rangePow = range * range;
        float minDis = Mathf.Infinity;

        foreach (var target in potentialTargets)
        {
            var gap = position - target.GetPosition();
            var gapDis = gap.magnitude;
            if(gapDis <= range && gapDis < minDis)
            {
                nearestTarget = target;
                minDis = gapDis;
            }
        }

        return nearestTarget;
    }
}

public interface ITargetable
{
    public Vector3 GetPosition();
}

public interface IDamageable
{
    public void TakeDamage(int damage);

}
