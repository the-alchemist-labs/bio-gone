using UnityEngine;

public enum MonsterId
{
    WhiteCell = 1,
    Pill = 2,
    Antibiotic = 3,
    Alcohol = 4,
    Vaccine = 5,
    Doctor = 100,
}

[CreateAssetMenu(fileName = "New Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public MonsterId Id;
    public string Name;
    public int Level;
    public int BattlePower;
    public int ExperienceGain;
    public int CoinsGain;

    public virtual void Initialize(Monster monster)
    {
        Id = monster.Id;
        Name = monster.Name;
        Level = monster.Level;
        BattlePower = monster.BattlePower;
        ExperienceGain = monster.ExperienceGain;
        CoinsGain = monster.CoinsGain;
    }
}