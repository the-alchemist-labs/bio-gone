
using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class BattleItemUsed
{
    public string UserPlayerId { get; set; }
    public ItemId ItemId { get; set; }
    public BattleTarget Target { get; set; }

    public BattleItemUsed(string userPlayerId, ItemId itemId, BattleTarget target)
    {
        UserPlayerId = userPlayerId;
        ItemId = itemId;
        Target = target;
    }
}

public class FleeBattle
{
    public int PlayerFleeValue { get; set; }
    public int MonsterFleeValue { get; set; }

    public FleeBattle(int playerFleeValue, int monsterFleeValue)
    {
        PlayerFleeValue = playerFleeValue;
        MonsterFleeValue = monsterFleeValue;
    }
}

[Serializable]
public class UpdateBattlePhaseCommandPayload
{ 
    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    [CanBeNull] public FleeBattle FleeBattle { get; set; }
    
    public UpdateBattlePhaseCommandPayload(BattlePhase battlePhase, BattleItemUsed battleItemUsed, FleeBattle fleeBattle)
    {
        BattlePhase = battlePhase;
        BattleItemUsed = battleItemUsed;
        FleeBattle = fleeBattle;
    }
}

public class UpdateBattlePhaseCommand : ICommand
{
    public static event Action<BattlePhase, BattleItemUsed, FleeBattle> OnBattlePhaseChanged;

    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    [CanBeNull] public FleeBattle FleeBattle { get; set; }

    public UpdateBattlePhaseCommand(string payloadString)
    {
        UpdateBattlePhaseCommandPayload payload = JsonConvert.DeserializeObject<UpdateBattlePhaseCommandPayload>(payloadString);
        BattlePhase = payload.BattlePhase;
        BattleItemUsed = payload.BattleItemUsed;
        FleeBattle = payload.FleeBattle;
    }
    
    public void Execute()
    {
        OnBattlePhaseChanged?.Invoke(BattlePhase, BattleItemUsed, FleeBattle);
    }
}