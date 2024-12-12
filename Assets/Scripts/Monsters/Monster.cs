using UnityEngine;

public enum MonsterId
{
    GeneralSpot,
}

[CreateAssetMenu(fileName = "New Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public MonsterId Id;
    public string Name;
    public string Description;
    public int Level;
    public int BattlePower;
    public int  ExperienceGain;
    public int  CoinsGain;
    public int  ItemsGain;

    public virtual void Initialize(Monster monster)
    {
        Id = monster.Id;
        Name = monster.Name;
        Description = monster.Description;
        Level = monster.Level;
        BattlePower = monster.BattlePower;
        ExperienceGain = monster.ExperienceGain;
        CoinsGain = monster.CoinsGain;
        ItemsGain = monster.ItemsGain;
    }
}