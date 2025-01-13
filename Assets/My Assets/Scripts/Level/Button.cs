using UnityEngine;
using UnityEngine.Events;

namespace GolfBots.Level {
public class Button : MonoBehaviour {
    
    [SerializeField] private int id;

    void OnTriggerEnter(Collider collisionObject) {
        if (collisionObject.gameObject.tag == "Bot") {
            Debug.Log($"Pressed button {id}");
            GolfBots.State.EventManager.Instance.RaiseButtonPress(id);
            GetComponent<Collider>().enabled = false;
        }
    }
}

}