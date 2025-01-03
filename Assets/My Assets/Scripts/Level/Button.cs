using UnityEngine;
using UnityEngine.Events;

namespace GolfBots.Level {
    public class Button : MonoBehaviour {
        
        [SerializeField] private int id;
        public static UnityEvent<int> onButtonPressed = new UnityEvent<int>();

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "Bot") {
                Debug.Log("Raising Event: openDoor(" + id + ")");
                onButtonPressed?.Invoke(id);
            }
        }
    }
}