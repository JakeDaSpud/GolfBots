using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBots.Level {
    public class Mineable : MonoBehaviour
    {
        public void Mine() {
            Debug.Log("Mining " + this.name);

            Destroy(this.gameObject);
        }
    }
}