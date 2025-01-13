using System.Collections.Generic;
using UnityEngine;
using GolfBots.Bots;
using Unity.VisualScripting;

namespace GolfBots.Bots {
    public struct BotUIPacket {
        public GolfBots.Bots.BotType type;
        public int amount;

        public BotUIPacket(BotType type, int amount) {
            this.type = type;
            this.amount = amount;
        }
    }
}

namespace GolfBots.Player {

public class PlayerBotInventoryController : MonoBehaviour {

    [SerializeField] private GameObject MineBotPrefab;
    [SerializeField] private GameObject JumpBotPrefab;

    private Dictionary<BotType, GameObject> botPrefabs;

    private Dictionary<BotType, int> currentBots = new Dictionary<BotType, int> {
        {BotType.MineBot, 1},
        {BotType.JumpBot, 0}
    };

    [SerializeField] private GolfBots.Level.LevelInventorySetup[] levelInventories; // All Level Inventory Setups

    /// <summary>
    /// The current Bot that's next to Aim / Send
    /// </summary>
    private BotType currentBot = BotType.MineBot;

    void Awake() {
        botPrefabs = new Dictionary<BotType, GameObject> {
            {BotType.MineBot, MineBotPrefab},
            {BotType.JumpBot, JumpBotPrefab}
        };

        SetBotUI();
    }

    private void SetBotUI() {
        GolfBots.State.EventManager.Instance.RaiseSetBotUI(new BotUIPacket(currentBot, currentBots[currentBot]));
    }

    private void SetInventory(int newInventoryID) {
        Debug.Log($"Setting inventory from button press {newInventoryID}");
        currentBots[BotType.MineBot] = levelInventories[newInventoryID].MineBotCount;
        currentBots[BotType.JumpBot] = levelInventories[newInventoryID].JumpBotCount;
        SetBotUI();
    }

    private void SetInventory(GolfBots.Level.LevelInventorySetup newInventory) {
        Debug.Log($"Setting inventory from Reset: {newInventory}");
        currentBots[BotType.MineBot] = newInventory.MineBotCount;
        currentBots[BotType.JumpBot] = newInventory.JumpBotCount;
        SetBotUI();
    }

    private void RefillInventory(int levelID) {
        SetInventory(levelID);
    }

    void OnEnable() {
        GolfBots.State.EventManager.Instance.OnSetupBot += SendCurrentBot;
        GolfBots.State.EventManager.Instance.OnNextBot += NextBot;
        GolfBots.State.EventManager.Instance.OnBotAimPointsSet += SpawnBot;
        GolfBots.State.EventManager.Instance.OnResetInventory += SetInventory;
        GolfBots.State.EventManager.Instance.OnButtonPress += SetInventory;
        GolfBots.State.EventManager.Instance.OnRefillInventory += RefillInventory;
    }

    void OnDisable() {
        GolfBots.State.EventManager.Instance.OnSetupBot -= SendCurrentBot;
        GolfBots.State.EventManager.Instance.OnNextBot -= NextBot;
        GolfBots.State.EventManager.Instance.OnBotAimPointsSet -= SpawnBot;
        GolfBots.State.EventManager.Instance.OnResetInventory -= SetInventory;
        GolfBots.State.EventManager.Instance.OnButtonPress -= SetInventory;
        GolfBots.State.EventManager.Instance.OnRefillInventory -= RefillInventory;
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

        SetBotUI();
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
            SetBotUI();
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
