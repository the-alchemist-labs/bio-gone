
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

[Serializable]
public class UpdateBattlePhaseCommandPayload
{ 
    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    public bool? HasEscaped { get; set; }
    
    public UpdateBattlePhaseCommandPayload(BattlePhase battlePhase, BattleItemUsed battleItemUsed, bool? hasEscaped)
    {
        BattlePhase = battlePhase;
        BattleItemUsed = battleItemUsed;
        HasEscaped = hasEscaped;
    }
}

public class UpdateBattlePhaseCommand : ICommand
{
    public static event Action<BattlePhase, BattleItemUsed, bool?> OnBattlePhaseChanged;

    public BattlePhase BattlePhase { get; set; }
    [CanBeNull] public BattleItemUsed BattleItemUsed { get; set; }
    public bool? HasEscaped { get; set; }

    public UpdateBattlePhaseCommand(string payloadString)
    {
        JObject obj = JObject.Parse(payloadString);
        int battlePhaseValue = (int)obj["BattlePhase"];
        BattlePhase battlePhase = (BattlePhase)battlePhaseValue;
        
        
        UpdateBattlePhaseCommandPayload payload = JsonConvert.DeserializeObject<UpdateBattlePhaseCommandPayload>(payloadString);
        BattlePhase = payload.BattlePhase;
        BattleItemUsed = payload.BattleItemUsed;
        HasEscaped = payload.HasEscaped;
    }
    
    public void Execute()
    {
        OnBattlePhaseChanged?.Invoke(BattlePhase, BattleItemUsed, HasEscaped);
    }
}