using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GolfBots.Player {
    
public class PlayerController : MonoBehaviour {
    // Adapted from Brackey's NavMesh Tutorial
    // https://www.youtube.com/watch?v=CHV1ymlw-P8

    [SerializeField] private PlayerBotInventoryController inventory;
    [SerializeField] public Camera cam;
    private PlayerInputs actions;
    // This didn't work out, I couldn't use the bool out of this for checking how I wanted in my AimRaycastController.cs class
    // public UnityEvent<bool> HoldingAim = new UnityEvent<bool>();
    public bool isHoldingAim = false;

    void Awake() {
        actions = new PlayerInputs();

        if (inventory == null)
            Debug.Log("Inventory is not assigned.");
    }

    void OnEnable() {
        actions.Player.Aim.Enable();
        actions.Player.Aim.performed += Aim;
        actions.Player.Aim.canceled += AimCanceled;
        actions.Player.Scroll.Enable();
        actions.Player.Scroll.performed += NextBot;
    }

    void OnDisable() {
        actions.Player.Aim.performed -= Aim;
        actions.Player.Aim.canceled -= AimCanceled;
        actions.Player.Aim.Disable();
        actions.Player.Scroll.Disable();
    }

    /// <summary>
    /// Move the Player as close as possible to the Mouse
    /// </summary>
    void OnMove() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            this.GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }
    }

    void Aim(InputAction.CallbackContext context) {
        isHoldingAim = true;
        Debug.Log("Aiming Started");
    }

    void AimCanceled(InputAction.CallbackContext context) {
        isHoldingAim = false;
        inventory.SendCurrentBot();
        Debug.Log("Aiming Stopped");
    }

    void NextBot(InputAction.CallbackContext context) {
        inventory.NextBot();
        Debug.Log("Switching to next Bot");
    }

    void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

}