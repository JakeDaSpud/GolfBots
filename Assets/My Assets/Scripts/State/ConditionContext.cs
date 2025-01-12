using UnityEngine;

// Adapted from Niall's ConditionContext

namespace GolfBots.State {

    /// <summary>
    /// Store reference to entities/objects that the conditions need to check against.
    /// </summary>
    public class ConditionContext {
        // Used by the conditions to get the current state of the player
        private Player.PlayerController player;

        // Used by the conditions to get the current state of the inventory
        private Player.PlayerBotInventoryController inventory;

        // Used by the conditions to get the current state of the spawn points
        private GolfBots.Level.SpawnPoint[] spawnPoints;

        // Used by the conditions to get the current state of the game object
        private GameObject gameObject;

        public Player.PlayerController Player { get => player; set => player = value; }
        public Player.PlayerBotInventoryController Inventory { get => inventory; set => inventory = value; }
        public GolfBots.Level.SpawnPoint[] SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
        public GameObject GameObject { get => gameObject; set => gameObject = value; }

        // Add other context dependencies here

        public ConditionContext(Player.PlayerController player, Player.PlayerBotInventoryController inventory, GolfBots.Level.SpawnPoint[] spawnPoints, GameObject gameObject) {
            Player = player;
            Inventory = inventory;
            SpawnPoints = spawnPoints;
            GameObject = gameObject;
        }

        public ConditionContext(Player.PlayerController player, Player.PlayerBotInventoryController inventory) : this(player, inventory, null, null) { }
        public ConditionContext(Player.PlayerController player, Player.PlayerBotInventoryController inventory, GolfBots.Level.SpawnPoint[] spawnPoints) : this(player, inventory, spawnPoints, null) { }
        public ConditionContext(Player.PlayerController player) : this(player, null, null, null) { }
        public ConditionContext(Player.PlayerBotInventoryController inventory) : this(null, inventory, null, null) { }
        public ConditionContext(GameObject gameObject) : this(null, null, null, gameObject) { }
        public ConditionContext() : this(null, null, null, null) { }
    }
}