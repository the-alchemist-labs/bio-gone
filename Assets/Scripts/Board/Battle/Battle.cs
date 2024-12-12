using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BattleTarget
{
    Player,
    Monster,
}

public enum BattlePhase
{
    Encounter,
    Flee,
    OpponentInterrupt,
    PlayerAction,
    Result,
}

public class PlayerInBattle : Player
{
    public int TempPower { get; set; }
    public new int BattlePower => base.BattlePower + TempPower;

    public PlayerInBattle(string playerId, string name, PlayerProfilePicture profilePicture, TileId position)
        : base(playerId, name, profilePicture, position)
    {
        TempPower = 0;
    }
}


public class MonsterInBattle : Monster
{
    public int TempPower { get; set; }
    public new int BattlePower => base.BattlePower + TempPower;

    public new void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TempPower = 0; // Set any additional fields specific to MonsterInBattle
    }
}

public class Battle
{
    public BattlePhase Phase { get; private set; }
    public MonsterInBattle Monster { get; }
    public PlayerInBattle Player { get; }

    public Battle(string playerId, MonsterId monsterId)
    {
        Player player = GameManager.Instance.GameState.GetPlayer(playerId);
        Monster monster = MonsterCatalog.Instance.GetMonster(monsterId);

        Monster = ScriptableObject.CreateInstance<MonsterInBattle>();
        Monster.Initialize(monster);
        Player = new PlayerInBattle(player.Id, player.Name, player.ProfilePicture, player.Position);
        Phase = BattlePhase.Encounter;
    }

    public static MonsterId GetMonster(int playerLevel)
    {
        List<Monster> monsters = MonsterCatalog.Instance.Monsters
            .Where(m => m.Level >= playerLevel - Consts.MonsterLevelDiff)
            .Where(m => m.Level <= playerLevel + Consts.MonsterLevelDiff)
            .ToList();

        return monsters[Random.Range(0, monsters.Count)].Id;
    }

    public void UpdateBattlePhase(BattlePhase battlePhase)
    {
        Phase = battlePhase;
        switch (battlePhase)
        {
            case BattlePhase.Flee:
                PopupManager.Instance.battlePopup.TryToFlee();
                return;
            case BattlePhase.OpponentInterrupt:
                PopupManager.Instance.battlePopup.Interrupt();
                return;
            case BattlePhase.PlayerAction:
                PopupManager.Instance.battlePopup.PlayerAction();
                return;
            case BattlePhase.Result:
                PopupManager.Instance.battlePopup.Result();
                return;
        }
    }

    // Probably should be on the ItemService
    public void UseItem(BattleItemUsed usedItem)
    {
        ConsumableItem item = ItemCatalog.Instance.GetItem<ConsumableItem>(usedItem.ItemId);
        switch (item.Effect.EffectId)
        {
            case ItemEffectId.BonusPower:
                ModifyBattlePower(usedItem.Target, item.Effect.Value);
                return;
            case ItemEffectId.HealUser:
                Player.ModifyLives(item.Effect.Value);
                return;
        }
    }

    private void ModifyBattlePower(BattleTarget target, int modifier)
    {
        if (target == BattleTarget.Player) Player.TempPower += modifier;
        else Monster.TempPower += modifier;
    }
}