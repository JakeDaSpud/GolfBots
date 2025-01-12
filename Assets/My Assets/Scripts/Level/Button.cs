using UnityEngine;
using UnityEngine.Events;

namespace GolfBots.Level {
    public class Button : MonoBehaviour {
        
        [SerializeField] private int id;

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "Bot") {
                GolfBots.State.EventManager.Instance.RaiseButtonPress(id);
            }
        }
    }
}