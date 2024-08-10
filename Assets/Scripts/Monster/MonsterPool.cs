using Unity.VisualScripting;
using UnityEngine;

public class MonsterPool : GameObjectPool<Monster>
{
    private MonsterSO _monsterData;

    public void SetData(MonsterSO monsterData)
    {
        _monsterData = monsterData;
    }

    protected override Monster OnCreate()
    {
        var prefab = Resources.Load($"Prefabs/Monsters/{_monsterData.Character}");
        var go = Instantiate(prefab, transform);
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