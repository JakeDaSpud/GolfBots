using UnityEngine;

namespace GolfBots.Level {
    public class Mineable : MonoBehaviour
    {
        public void Mine() {
            Debug.Log("Mining " + this.name);

            // Sound FX?
            // Particle FX?

            this.gameObject.GetComponent<Collider>().enabled = false;

            Destroy(this.gameObject);
        }
    }
}