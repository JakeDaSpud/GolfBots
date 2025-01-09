using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace GolfBots.Level {
    public class Door : MonoBehaviour {
        
        [SerializeField] private int id;
        private bool shouldOpen = false;

        void Start() {
            Button.onButtonPressed.AddListener(OpenIfIdMatches);
        }

        void Update() {
            if (shouldOpen) {
                transform.DOMoveY(0.0f, 2.5f).OnComplete(DoneOpening); // Move to 0 on Y axis in 2.5 seconds
            }
        }

        void OpenIfIdMatches(int buttonId) {
            if (buttonId == id) {
                shouldOpen = true;
                // Play SFX
                Debug.Log("Opening Door " + id);
            }
        }

        void DoneOpening() {
            Debug.Log("Done Opening Door " + id);
            transform.DOKill();
            Destroy(this.gameObject);
        }
    }
}