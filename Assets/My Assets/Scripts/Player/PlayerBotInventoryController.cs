using System.Collections.Generic;
using UnityEngine;
using GolfBots.Bots;

namespace GolfBots.Player {

public class PlayerBotInventoryController : MonoBehaviour {

    [SerializeField] private GameObject MineBotPrefab;
    [SerializeField] private GameObject JumpBotPrefab;

    private Dictionary<BotType, GameObject> botPrefabs;

    private Dictionary<BotType, int> currentBots = new Dictionary<BotType, int> {
        {BotType.MineBot, 1},
        {BotType.JumpBot, 0}
    };

    private BotType currentBot = BotType.MineBot;

    void Awake() {
        botPrefabs = new Dictionary<BotType, GameObject> {
            {BotType.MineBot, MineBotPrefab},
            {BotType.JumpBot, JumpBotPrefab}
        };
    }

    void OnEnable() {
        GolfBots.State.EventManager.Instance.OnSetupBot += SendCurrentBot;
        GolfBots.State.EventManager.Instance.OnNextBot += NextBot;
        GolfBots.State.EventManager.Instance.OnBotAimPointsSet += SpawnBot;
    }

    void OnDisable() {
        GolfBots.State.EventManager.Instance.OnSetupBot -= SendCurrentBot;
        GolfBots.State.EventManager.Instance.OnNextBot -= NextBot;
        GolfBots.State.EventManager.Instance.OnBotAimPointsSet -= SpawnBot;
    }

    private void NextBot() {
        if (currentBot == BotType.MineBot) {
            currentBot = BotType.JumpBot;
            Debug.Log("Current Bot is now JumpBot");
        }

        else {
            currentBot = BotType.MineBot;
            Debug.Log("Current Bot is now MineBot");
        }
    }

    private void SendCurrentBot() {
        // Check if enough of current selected bot
        if (currentBots[currentBot] > 0) {
            Debug.Log("Sending " + currentBot + ".");
        
            // Hardcoded "for now"
            // Raise GameEvent with bot type
            GolfBots.State.EventManager.Instance.RaiseBotTypeSet(currentBot);
            
            // Remove currentBot
            currentBots[currentBot]--;
        }

        else {
            Debug.Log("There are no " + currentBot + "s to send.");
            // Play deny sfx?
        }
    }

    private void SpawnBot(Vector3[] aimPoints) {

        GameObject botToSpawn = botPrefabs[currentBot];
        
        GameObject spawnedBot = Instantiate(botToSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);
        spawnedBot.GetComponent<BotMovement>().SetAimPoints(aimPoints);
    }
}

}
