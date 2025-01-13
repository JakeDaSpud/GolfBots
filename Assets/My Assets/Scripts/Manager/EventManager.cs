using System;
using UnityEngine;

namespace GolfBots.State {

public class EventManager : GD.Singleton<EventManager> {
    
    /*void Awake() {
        Debug.Log("Awake() in EventManager called");
    }*/

    // Game / State Events
    public event Action OnGameStart; // Scene Start
    public event Action<int> OnButtonPress; // int is Door/Button ID; Level Completed, should change Player's Inventory and Restart / Respawn Point
    public event Action OnRespawn; // On R Press, should remake Player's Inventory and Respawn from last set Respawn Point
    public event Action OnGameFinish; // Final Level Completed, should show the Player's Final Stat Screen
    public event Action<GolfBots.Level.LevelInventorySetup> OnResetInventory; // On Button Press & Restart, should give Player the given level's Inventory
    public event Action<int> OnRefillInventory; // On Restart, should give Player the current level's Inventory

    // UI Events
    public event Action OnPause; // Set Pause bool to true or false
    
    // Player Events
    public event Action OnSetupBot; // Signals that a Bot should be set up, for Spawning in front of the Player
    public event Action<GolfBots.Bots.BotType> OnBotTypeSet; // Takes BotType; Left-click succeeds, should set BotType of Bot to Spawn
    public event Action<Vector3[]> OnBotAimPointsSet; // Takes an Array of Vector3s which will be where the sent Bot travels to
    public event Action OnNextBot; // Changes the Current Selected Bot

    // Raising Functions
    public void RaiseGameStart() { OnGameStart?.Invoke(); }
    public void RaiseButtonPress(int doorID) { OnButtonPress?.Invoke(doorID); }
    public void RaiseRespawn() { OnRespawn?.Invoke(); }
    public void RaiseGameFinish() { OnGameFinish?.Invoke(); }
    public void RaiseResetInventory(GolfBots.Level.LevelInventorySetup newInventory) { OnResetInventory?.Invoke(newInventory); }
    public void RaiseRefillInventory(int levelID) { OnRefillInventory?.Invoke(levelID); }

    public void RaisePause() { OnPause?.Invoke(); }

    public void RaiseSetupBot() { OnSetupBot?.Invoke(); }
    public void RaiseBotTypeSet(GolfBots.Bots.BotType type) { OnBotTypeSet?.Invoke(type); }
    public void RaiseBotAimPointsSet(Vector3[] aimPoints) { OnBotAimPointsSet?.Invoke(aimPoints); }
    public void RaiseNextBot() { OnNextBot?.Invoke(); }
}

}