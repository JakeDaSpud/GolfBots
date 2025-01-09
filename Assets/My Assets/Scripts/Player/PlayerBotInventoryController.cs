using System.Collections.Generic;
using UnityEngine;
using GolfBots.Bots;

namespace GolfBots.Player {

public class PlayerBotInventoryController : MonoBehaviour {
    [SerializeField] private PlayerController playerController;

    private Dictionary<BotType, int> currentBots = new Dictionary<BotType, int> {
        {BotType.MineBot, 0},
        {BotType.JumpBot, 0}
    };

    private BotType currentBot = BotType.MineBot;

    void OnEnable() {
        // GameEvent listener, setBotInventory which will take in an array like {"Mine", "Mine", "Jump"} and set the inventory to be that
    }

    void OnDisable() {

    }

    public void NextBot() {
        if (currentBot == BotType.MineBot) {
            currentBot = BotType.JumpBot;
        }

        else {
            currentBot = BotType.MineBot;
        }
    }

    public void SendCurrentBot() {
        // Check if enough of current selected bot
        if (currentBots[currentBot] > 0) {
            Debug.Log("Sending " + currentBot + ".");
        
            // Raise GameEvent with bot type
            //event
            
            // Remove currentBot
            currentBots[currentBot]--;
        }

        else {
            Debug.Log("There are no " + currentBot + "s to send.");
            // Play deny sfx?
        }
    }
}

}
