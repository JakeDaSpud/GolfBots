using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GolfBots.Bots {
    public class BotMovement : MonoBehaviour {

        // Local Variables
        [SerializeField] private BotData botData; // Data this Bot will use
        private float moveSpeed;
        private int maxReflections;
        private int currentReflections;

        private bool canJump;
        private float jumpInterval;
        private float jumpPower;
        private float jumpTimer;

        // Start is called before the first frame update
        void Start() {
            this.moveSpeed = botData.moveSpeed;
            this.maxReflections = botData.maxReflections;
            this.currentReflections = this.maxReflections;

            this.canJump = botData.canJump;
            this.jumpInterval = botData.jumpInterval;
            this.jumpPower = botData.jumpPower;
            this.jumpTimer = this.jumpInterval;
        }

        // Update is called once per frame
        void Update() {
            if (canJump)
                UpdateJumpTimer();
        }

        void FixedUpdate() {
            if (currentReflections > maxReflections) {
                Destroy(this);
            }

            // Moving forward
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }

        void UpdateJumpTimer() {
            this.jumpTimer -= Time.deltaTime;

            if (this.jumpTimer <= 0.0f) {
                Jump();
                this.jumpTimer = this.jumpInterval; // Reset timer
            }
        }

        void Jump() {
            Debug.Log(this.name + " is jumping");
        }
    }
}