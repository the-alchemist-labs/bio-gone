using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterCatalog : MonoBehaviour
{
    private const string SCRIPTABLE_OBJECT_MONSTERS_PATH = "ScriptableObjects/Monsters";

    public static MonsterCatalog Instance { get; private set; }

    public List<Monster> Monsters { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        Monsters = Resources.LoadAll<Monster>(SCRIPTABLE_OBJECT_MONSTERS_PATH)
            .Select(Instantiate)
            .ToList();
    }

    public Monster GetMonster(MonsterId id)
    {
        return Monsters.Find(el => el.Id == id);
    }
}