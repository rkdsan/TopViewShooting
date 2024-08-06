using UnityEngine;

public class MonsterFactory : GameObjectPool<Monster>
{
    [SerializeField] private Monster _monsterPrefab;
    [SerializeField] private GameObject _monsterCreateEffect;
    [SerializeField] private GameObject _monsterReleaseEffect;

    public Monster SpawnMonster(Vector3 spawnPosition)
    {
        var monster = Pool.Get();
        monster.NavAgent.Warp(spawnPosition);
        Instantiate(_monsterCreateEffect, spawnPosition, Quaternion.identity);

        return monster;
    }

    protected override Monster OnCreate()
    {
        var monster = Instantiate(_monsterPrefab);
        monster.SetPool(this);

        return monster;
    }

    protected override void OnGet(Monster item)
    {
        item.gameObject.SetActive(true);
        item.Init();
    }

    protected override void OnRelease(Monster item)
    {
        item.gameObject.SetActive(false);

        Instantiate(_monsterReleaseEffect, item.transform.position, Quaternion.identity);
    }
}