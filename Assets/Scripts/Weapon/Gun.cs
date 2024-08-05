using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Gun : Weapon
{
    [SerializeField] private ParticleSystem _fireEffect;

    private float _attackRange = 10;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _attackRange, Color.red);
    }

    public override void Attack()
    {
        bool isHit = Physics.Raycast(transform.position, transform.forward, out var hit, _attackRange, LayerManager.CombatTargetLayer);
        if (isHit)
        {
            var damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null )
            {
                damageable.TakeDamage(1);
            }
        }

        _fireEffect.Play();

    }
}
