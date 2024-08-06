using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Gun : Weapon
{
    public float AttackRange { get; private set; }
    public int AttackDamage { get; private set; }

    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private ParticleSystem _bulletEffect;

    private void Awake()
    {
        AttackDamage = 1;
        SetAttackRange(10);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * AttackRange, Color.red);
    }

    public override void Attack()
    {
        bool isHit = Physics.Raycast(transform.position, transform.forward, out var hit, AttackRange, LayerManager.CombatTargetLayer);
        if (isHit)
        {
            var damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null )
            {
                damageable.TakeDamage(AttackDamage);
            }
        }

        _fireEffect.Play();
    }

    private void SetAttackRange(int newAttackRange)
    {
        AttackRange = newAttackRange;
        var mainModule = _bulletEffect.main;

        float newLifeTime = AttackRange / mainModule.startSpeed.constant;
        mainModule.startLifetime = newLifeTime;
    }
}
