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

        // Used by the conditions to get the current state of the game object
        private GameObject gameObject;

        public Player.PlayerController Player { get => player; set => player = value; }
        public Player.PlayerBotInventoryController Inventory { get => inventory; set => inventory = value; }
        public GameObject GameObject { get => gameObject; set => gameObject = value; }

        // Add other context dependencies here

        public ConditionContext(Player.PlayerController player, Player.PlayerBotInventoryController inventory, GameObject gameObject) {
            Player = player;
            Inventory = inventory;
            GameObject = gameObject;
        }

        public ConditionContext(Player.PlayerController player, Player.PlayerBotInventoryController inventory) : this(player, inventory, null) { }
        public ConditionContext(Player.PlayerController player) : this(player, null, null) { }
        public ConditionContext(Player.PlayerBotInventoryController inventory) : this(null, inventory, null) { }
        public ConditionContext(GameObject gameObject) : this(null, null, gameObject) { }
        public ConditionContext() : this(null, null, null) { }
    }
}