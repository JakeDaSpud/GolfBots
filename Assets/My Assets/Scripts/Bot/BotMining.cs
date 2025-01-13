using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GolfBots.Level;

namespace GolfBots.Bots {
    public class BotMining : MonoBehaviour {
        void OnTriggerEnter(Collider collisionObject) {
            if (collisionObject.gameObject.GetComponent<Mineable>()) {
                collisionObject.gameObject.GetComponent<Mineable>().Mine();
            }
        }
    }
}
