using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GD.State {
    /// <summary>
    /// Manages the game state by evaluating win and loss conditions.
    /// </summary>
    public class StateManager : MonoBehaviour {

        [FoldoutGroup("Timing & Reset")]
        [SerializeField]
        [Tooltip("Reset all conditions on start")]
        private bool resetAllConditionsOnStart = true;

        [FoldoutGroup("Context", expanded: true)]
        [SerializeField]
        [Tooltip("Player reference to evaluate conditions required by the context")]
        private GolfBots.Player.PlayerController player;

        [FoldoutGroup("Context")]
        [SerializeField]
        [Tooltip("Player inventory collection to evaluate conditions required by the context")]
        private GolfBots.Player.PlayerBotInventoryController inventory;

        [FoldoutGroup("Context")]
        [SerializeField]
        [Tooltip("Player Spawn Points to teleport the Player to")]
        private GolfBots.Level.SpawnPoint[] spawnPoints;

        private int currentLevel = 1;

        /// <summary>
        /// The condition that determines if the player wins.
        /// </summary>
        [FoldoutGroup("Conditions")]
        [SerializeField]
        [Tooltip("The condition that determines if the player wins")]
        private ConditionBase winCondition;

        /// <summary>
        /// The condition that determines if the player loses.
        /// </summary>
        [FoldoutGroup("Conditions")]
        [SerializeField]
        [Tooltip("The condition that determines if the player loses")]
        private ConditionBase loseCondition;

        [FoldoutGroup("Achievements [optional]")]
        [SerializeField]
        [Tooltip("Set of optional conditions related to achievements")]
        private List<ConditionBase> achievementConditions;

        void OnEnable() {
            GolfBots.State.EventManager.Instance.OnButtonPress += SetPlayerSpawnPoint;
            GolfBots.State.EventManager.Instance.OnRespawn += RespawnPlayer;
        }

        void OnDisable() {
            GolfBots.State.EventManager.Instance.OnButtonPress -= SetPlayerSpawnPoint;
            GolfBots.State.EventManager.Instance.OnRespawn -= RespawnPlayer;
        }

        void SetPlayerSpawnPoint(int doorID) {
            player.SetSpawnPoint(spawnPoints[doorID-1].transform.position);
        }

        void RespawnPlayer() {
            // .Warp()
            player.GetComponent<NavMeshAgent>().Warp(spawnPoints[currentLevel-1].transform.position);

            // .SetInventory()
        }

        /// <summary>
        /// Indicates whether the game has ended.
        /// </summary>
        private bool gameEnded = false;

        private GolfBots.State.ConditionContext conditionContext;

        private void Awake()
        {
            if (player == null)
                throw new System.Exception("Player reference is required!");

            SetPlayerSpawnPoint(1);

            if (inventory == null)
                throw new System.Exception("Player Bot Inventory reference is required!");

            if (spawnPoints.Length == 0)
                throw new System.Exception("Player Spawn Points required!");

            // Wrap the two objects inside the context envelope
            conditionContext = new GolfBots.State.ConditionContext(player, inventory, spawnPoints);
        }

        private void OnDestroy() { }

        private void Start() {
            if (resetAllConditionsOnStart)
                ResetConditions();
        }

        /// <summary>
        /// Evaluates conditions each frame and handles game state transitions.
        /// </summary>
        //private void Update()  //TODO - NMCG : Slow down the update rate to once every 0.1 seconds
        //{
        //    //// If the game has already ended, no need to evaluate further
        //    //if (gameEnded)
        //    //    return;

        //    //// Evaluate the win condition
        //    //if (winCondition != null && winCondition.Evaluate(conditionContext))
        //    //{
        //    //    HandleWin();
        //    //    // Set gameEnded to true to prevent further updates
        //    //    gameEnded = true;
        //    //    // Optionally, disable this component
        //    //    // enabled = false;
        //    //}
        //    //// Evaluate the lose condition only if the win condition is not met
        //    //else if (loseCondition != null && loseCondition.Evaluate(conditionContext))
        //    //{
        //    //    HandleLoss();
        //    //    // Set gameEnded to true to prevent further updates
        //    //    gameEnded = true;
        //    //    // Optionally, disable this component
        //    //    // enabled = false;
        //    //}

        //    //foreach (var achievmentCondition in achievementConditions)
        //    //{
        //    //    if (achievmentCondition != null && achievmentCondition.Evaluate(conditionContext))
        //    //    {
        //    //        //do something
        //    //    }
        //    //}
        //}

        /// <summary>
        /// Handles the logic when the player wins.
        /// </summary>
        protected virtual void HandleWin()
        {
            Debug.Log($"Player Wins! Win condition met at {winCondition.TimeMet} seconds.");

            // Implement win logic here, such as:
            // - Displaying a victory screen
            // - Transitioning to the next level
            // - Awarding points or achievements
            // - Playing a victory sound or animation

            // Example:
            // UIManager.Instance.ShowVictoryScreen();
            // SceneManager.LoadScene("NextLevel");
        }

        /// <summary>
        /// Handles the logic when the player loses.
        /// </summary>
        protected virtual void HandleLoss()
        {
            Debug.Log($"Player Loses! Lose condition met at {loseCondition.TimeMet} seconds.");

            // Implement loss logic here, such as:
            // - Displaying a game over screen
            // - Offering a restart option
            // - Reducing player lives
            // - Playing a defeat sound or animation

            // Example:
            // UIManager.Instance.ShowGameOverScreen();
            // GameManager.Instance.RestartLevel();
        }

        /// <summary>
        /// Resets the win and loss conditions.
        /// Call this method when restarting the game or level.
        /// </summary>
        public void ResetConditions()
        {
            // Reset the gameEnded flag
            gameEnded = false;

            // Reset the win condition
            if (winCondition != null)
                winCondition.ResetCondition();

            // Reset the lose condition
            if (loseCondition != null)
                loseCondition.ResetCondition();

            // Reset the achievement conditions
            if (achievementConditions != null)
            {
                foreach (var achievmentCondition in achievementConditions)
                {
                    if (achievmentCondition != null)
                        achievmentCondition.ResetCondition();
                }
            }
        }

        /// <summary>
        /// Move code from Update to HandleTick to perform the tasks at a slower rate
        /// </summary>
        /// <see cref="TimeTickSystem"/>
        public void HandleTick()
        {
            // If the game has already ended, no need to evaluate further
            if (gameEnded)
                return;

            // Evaluate the win condition
            if (winCondition != null && winCondition.Evaluate(conditionContext))
            {
                HandleWin();
                // Set gameEnded to true to prevent further updates
                gameEnded = true;
                // Optionally, disable this component
                // enabled = false;
            }
            // Evaluate the lose condition only if the win condition is not met
            else if (loseCondition != null && loseCondition.Evaluate(conditionContext))
            {
                HandleLoss();
                // Set gameEnded to true to prevent further updates
                gameEnded = true;
                // Optionally, disable this component
                // enabled = false;
            }

            // Evaluate the achievement conditions
            foreach (var achievmentCondition in achievementConditions)
            {
                if (achievmentCondition != null && achievmentCondition.Evaluate(conditionContext))
                {
                    //do something here
                }
            }
        }
    }
}