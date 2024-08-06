using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnSection : GameSection
{
    [SerializeField] private List<Transform> _spawnTransforms;
    [SerializeField] private MonsterFactory _monsterPool;

    private List<Monster> _spawnedMonsters;

    public override void ActiveSection(PlayerController player)
    {
        SpawnMonsters(player);
    }

    private void SpawnMonsters(PlayerController player)
    {
        _spawnedMonsters = new List<Monster>(_spawnTransforms.Count);

        foreach (var targetTransform in _spawnTransforms)
        {
            var monster = _monsterPool.SpawnMonster(targetTransform.position);
            monster.SetTarget(player.transform);
            monster.MonsterDeadEvent += OnDeadMonster;

            _spawnedMonsters.Add(monster);
        }
    }

    private void OnDeadMonster(Monster monster)
    {
        monster.MonsterDeadEvent -= OnDeadMonster;

        bool isAllDead = _spawnedMonsters.All(monster => monster.IsDead);
        if (isAllDead)
        {
            ClearSection();
        }
    }
}
