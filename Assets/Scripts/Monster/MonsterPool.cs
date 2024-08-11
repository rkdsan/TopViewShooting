using Unity.VisualScripting;
using UnityEngine;

public class MonsterPool : GameObjectPool<Monster>
{
    private MonsterSO _monsterData;

    public MonsterPool(MonsterSO monsterData)
    {
        _monsterData = monsterData;
    }

    protected override Monster OnCreate()
    {
        var prefab = Resources.Load($"Prefabs/Monsters/{_monsterData.Character}");
        var go = GameObject.Instantiate(prefab);
        var monster = go.GetComponent<Monster>();
        monster.SetPool(this);

        return monster;
    }

    protected override void OnGet(Monster item)
    {
        item.gameObject.SetActive(true);
        item.Init(_monsterData);
    }

    protected override void OnRelease(Monster item)
    {
        item.gameObject.SetActive(false);
    }
}