using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnSection : GameSection
{
    [SerializeField] private List<Monster> _spawnMonsters;

    public override void ActiveSection(PlayerController player)
    {
        SpawnMonsters(player);
    }

    private void SpawnMonsters(PlayerController player)
    {
        var centerPosition = transform.position;
        foreach (var monster in _spawnMonsters)
        {
            GameEventManager.Attach(GameEventType.MonsterDead, monster, OnDeadMonster);
            monster.gameObject.SetActive(true);
            monster.SetTarget(player.transform);
        }
    }

    private void OnDeadMonster(object eventEmitter)
    {
        GameEventManager.Detach(GameEventType.MonsterDead, eventEmitter, OnDeadMonster);

        bool isAllDead = _spawnMonsters.All(t => t.IsDead);
        if (isAllDead)
        {
            ClearSection();
        }
    }
}
