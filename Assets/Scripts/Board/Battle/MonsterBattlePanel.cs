
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class MonsterBattlePanel: MonoBehaviour
    {
        [SerializeField] private Image monsterImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text battlePowerText;
        
        public void Initialize(Battle battle)
        {
            MonsterInBattle monster = battle.Monster;
            monsterImage.sprite = Resources.Load<Sprite>($"Sprites/Monsters/{monster.Id}");
            nameText.text = monster.Name;
            levelText.text = monster.Level.ToString();
            battlePowerText.text = monster.BattlePower.ToString();
        }
    }