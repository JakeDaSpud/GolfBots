using UnityEngine;
using UnityEngine.Events;

namespace GolfBots.Level {
public class Button : MonoBehaviour {
    
    [SerializeField] private int id;

    void OnTriggerEnter(Collider collisionObject) {
        if (collisionObject.gameObject.tag == "Bot") {
            Debug.Log($"Pressed button {id}");

            if (id == 7) {
                GolfBots.State.EventManager.Instance.RaiseWin();
            }
            else {
                GolfBots.State.EventManager.Instance.RaiseButtonPress(id);
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}

}