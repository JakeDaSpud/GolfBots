using System.Collections.Generic;
using GolfBots.Player;
using UnityEngine;

namespace GolfBots.Bots {

// Spawn and despawn bots at certain positions,
// Events to spawn bots

public class BotManager : MonoBehaviour {

    [SerializeField] private PlayerController playerController;

    private void SpawnBot(BotType type) {
        Debug.Log("Spawning " + type.ToString());

        // Check what type to spawn

        // Set the move-to points of the bot

        // Instantiate in front of the player
        
    }
}

}
