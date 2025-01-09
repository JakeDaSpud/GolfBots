using UnityEngine;

namespace GolfBots.Level {
    [CreateAssetMenu(fileName = "New Level Bot Inventory", menuName = "Level Bot Inventory")]
    public class LevelInventorySetup : ScriptableObject {

        [SerializeField] public int LevelID = 0;
        [SerializeField] public int MineBotCount = 1;
        [SerializeField] public int JumpBotCount = 1;

        public int BotCount() {
            return MineBotCount + JumpBotCount;
        }
    }
}
