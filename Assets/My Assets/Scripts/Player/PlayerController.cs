using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GolfBots.Player {
    
public class PlayerController : MonoBehaviour {
    // Adapted from Brackey's NavMesh Tutorial
    // https://www.youtube.com/watch?v=CHV1ymlw-P8

    [SerializeField] public Camera cam;
    [SerializeField] private Animator mesh;
    private PlayerInputs actions;
    // This didn't work out, I couldn't use the bool out of this for checking how I wanted in my AimRaycastController.cs class
    // public UnityEvent<bool> HoldingAim = new UnityEvent<bool>();
    public bool isHoldingAim = false;
    private Vector3 spawnPoint;

    public void SetSpawnPoint(Vector3 spawnPoint) {
        this.spawnPoint = spawnPoint;
    }

    void Awake() {
        actions = new PlayerInputs();
    }

    void FixedUpdate() {
        if (GetComponent<NavMeshAgent>().velocity == Vector3.zero) {
            mesh.SetBool("isWalking", false);
        }
    }

    void OnEnable() {
        actions.Player.Aim.Enable();
        actions.Player.Aim.performed += Aim;
        actions.Player.Aim.canceled += AimCanceled;
        actions.Player.Scroll.Enable();
        actions.Player.Scroll.performed += NextBot;
        actions.Player.Restart.Enable();
        actions.Player.Restart.performed += Restart;
        actions.Player.Pause.Enable();
        actions.Player.Pause.performed += Pause;
    }

    void OnDisable() {
        actions.Player.Aim.performed -= Aim;
        actions.Player.Aim.canceled -= AimCanceled;
        actions.Player.Aim.Disable();
        actions.Player.Scroll.performed -= NextBot;
        actions.Player.Scroll.Disable();
        actions.Player.Restart.performed -= Restart;
        actions.Player.Restart.Disable();
        actions.Player.Pause.performed -= Pause;
        actions.Player.Pause.Disable();
    }

    /// <summary>
    /// Move the Player as close as possible to the Mouse
    /// </summary>
    void OnMove() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            mesh.SetBool("isWalking", true);
            this.GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }
    }

    void Aim(InputAction.CallbackContext context) {
        isHoldingAim = true;
        //Debug.Log("Aiming Started");
    }

    void AimCanceled(InputAction.CallbackContext context) {
        isHoldingAim = false;

        // If is standing on correct ground
        if (IsOnAimableGround()) {
            GolfBots.State.EventManager.Instance.RaiseSetupBot();
        }

        //Debug.Log("Aiming Stopped");
    }

    bool IsOnAimableGround() {
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.layer == 9) { // Layer 9 is the Aimable Ground Layer
                //Debug.Log("On aimable ground");
                return true;
            }
        }
        //Debug.Log("NOT on aimable ground");
        return false;
    }

    private void Pause(InputAction.CallbackContext context) {
        GolfBots.State.EventManager.Instance.RaisePause();
    }

    void NextBot(InputAction.CallbackContext context) {
        GolfBots.State.EventManager.Instance.RaiseNextBot();
        Debug.Log("Switching to next Bot");
    }

    void Restart(InputAction.CallbackContext context) {
        GolfBots.State.EventManager.Instance.RaiseRespawn();
    }
}

}