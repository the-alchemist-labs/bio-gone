
using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

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

[Serializable]
public class UpdateBattlePhaseCommandPayload
{ 
    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    
    public UpdateBattlePhaseCommandPayload(BattlePhase battlePhase, BattleItemUsed battleItemUsed)
    {
        BattlePhase = battlePhase;
        BattleItemUsed = battleItemUsed;
    }
}

public class UpdateBattlePhaseCommand : ICommand
{
    public static event Action<BattlePhase, BattleItemUsed> OnBattlePhaseChanged;

    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    
    public UpdateBattlePhaseCommand(string payloadString)
    {
        UpdateBattlePhaseCommandPayload payload = JsonConvert.DeserializeObject<UpdateBattlePhaseCommandPayload>(payloadString);
        BattlePhase = payload.BattlePhase;
        BattleItemUsed = payload.BattleItemUsed;
    }
    
    public void Execute()
    {
        OnBattlePhaseChanged?.Invoke(BattlePhase, BattleItemUsed);
    }
}