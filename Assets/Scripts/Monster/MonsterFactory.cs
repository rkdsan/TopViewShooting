using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject _monsterCreateEffect;

    private static MonsterFactory _Instance;
    private Dictionary<MonsterSO, MonsterPool> _monsterPool = new Dictionary<MonsterSO, MonsterPool>();

    private void Awake()
    {
        _Instance = this;
    }

    public static Monster CreateMonster(MonsterSO monsterData, Vector3 spawnPosition)
    {
        var pool = _Instance.GetPool(monsterData);
        var monster = pool.Pool.Get();
        monster.NavAgent.Warp(spawnPosition);
        GameObject.Instantiate(_Instance._monsterCreateEffect, spawnPosition, Quaternion.identity);

        return monster;
    }

    private MonsterPool GetPool(MonsterSO monsterData)
    {
        if (!_monsterPool.ContainsKey(monsterData))
        {
            var newPool = new MonsterPool(monsterData);
            _monsterPool[monsterData] = newPool;
        }

        return _monsterPool[monsterData];
    }
}
