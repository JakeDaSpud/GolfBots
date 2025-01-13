using System.Collections.Generic;
using UnityEngine;

namespace GolfBots.Bots {
    public class BotMovement : MonoBehaviour {

        // Local Variables
        [SerializeField] private BotData botData; // Data this Bot will use
        private float moveSpeed;
        private int maxReflections;
        private int currentReflections;
        public Queue<Vector3> aimPoints = new Queue<Vector3>(); // Where this Bot will travel to
        private bool hasAimPointsSet = false;
        private float closePointApproximationValue = 0.5f; // How close you have to be in all 3 Axes of a Vector3 to be considered "close"
        private Vector3 currentDestination;

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

            //this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        private void OnEnable() {
            GolfBots.State.EventManager.Instance.OnRespawn += Die;
        }

        private void OnDisable() {
            GolfBots.State.EventManager.Instance.OnRespawn -= Die;
        }

        // Update is called once per frame
        void Update() {
            if (canJump)
                UpdateJumpTimer();
        }

        void FixedUpdate() {
            if (aimPoints.Count > 0) {
                TravelToPoint();
            }
            
            else {
                //Debug.Log($"Bot destroyed: AimPoints.Count is {aimPoints.Count}");
                Die();
            }

            if (this.transform.position.y < 0) {
                Debug.Log("Bot too far down");
                Die();
            }
        }

        public void SetAimPoints(Vector3[] aimPoints) {
            if (hasAimPointsSet)
                return;

            if (this.aimPoints == null)
                this.aimPoints = new Queue<Vector3>();
            
            for (int i = 0; i < aimPoints.Length; i++) {
                //Debug.Log($"Point {i} : {aimPoints[i]}");
                this.aimPoints.Enqueue(aimPoints[i]);
                //Debug.Log("Added Point " + i);
            }

            // Set the first point
            if (this.aimPoints.Count > 0) {
                currentDestination = this.aimPoints.Peek();
                //this.transform.LookAt(currentDestination);
                LookAtDestination();
            }

            //Debug.Log($"SetAimPoints()'s final this.aimPoints.Count = {this.aimPoints.Count}");
            hasAimPointsSet = true;
        }

        private bool isClose(Vector3 point) {
            return
                Mathf.Abs(point.x - this.transform.position.x) <= closePointApproximationValue
                &&
                Mathf.Abs(point.z - this.transform.position.z) <= closePointApproximationValue
            ;
        }

        private void TravelToPoint() {
            // If arrived at current point
            if (isClose(currentDestination)) {
                //Debug.Log("Reached point: " + currentDestination);

                if (aimPoints.Count > 1) {
                    // Set next in queue
                    aimPoints.Dequeue();
                    currentDestination = aimPoints.Peek();
                    //this.transform.LookAt(currentDestination);
                    LookAtDestination();
                }

                else {
                    Debug.Log("No more Points to travel to, destroying this Bot");
                    Die();
                    return;
                }
            }

            // Moving forward
            //transform.position += transform.forward * Time.fixedDeltaTime * moveSpeed;
            currentDestination.y = this.transform.position.y;
            Vector3 direction = (currentDestination - transform.position).normalized;
            //Debug.Log($"Direction to move: {direction}");

            this.GetComponent<Rigidbody>().MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }

        // GPT's LookAt Replacement
        private void LookAtDestination() {
            Vector3 direction = (currentDestination - transform.position).normalized;
            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.GetComponent<Rigidbody>().MoveRotation(targetRotation);
            }
        }

        void UpdateJumpTimer() {
            this.jumpTimer -= Time.deltaTime;

            if (this.jumpTimer <= 0.0f) {
                Jump();
                this.jumpTimer = this.jumpInterval; // Reset timer
            }
        }

        /// <summary>
        /// This Bot's "Exit" Sequence
        /// </summary>
        private void Die() {
            Destroy(gameObject);
        }

        void Jump() {
            Debug.Log(this.name + " is jumping");
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower + this.transform.forward * (jumpPower/2), ForceMode.Impulse);
        }
    }
}