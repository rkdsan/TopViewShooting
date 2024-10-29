using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawnSection : GameSection
{
    [SerializeField] private List<Transform> _spawnTransforms;

    private List<Monster> _spawnedMonsters;

    public override void ActiveSection(PlayerController player)
    {
        SpawnMonsters(player);
    }

    private void SpawnMonsters(PlayerController player)
    {
        var data = (MonsterSpawnSectionSO)SectionData;
        _spawnedMonsters = new List<Monster>(_spawnTransforms.Count);

        for(int i = 0; i < _spawnTransforms.Count; i++)
        {
            var targetPosition = _spawnTransforms[i].position;
            var monsterData = data.MonsterList[i];

            var monster = MonsterFactory.CreateMonster(monsterData, targetPosition);
            EventManager.Subcribe(monster, EventType.MonsterDead, OnDeadMonster);

            _spawnedMonsters.Add(monster);
        }
    }

    private void OnDeadMonster(object sender)
    {
        _spawnedMonsters.Remove((Monster)sender);

        bool isAllDead = _spawnedMonsters.Count == 0;
        if (isAllDead)
        {
            ClearSection();
        }
    }
}
