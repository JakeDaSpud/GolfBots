using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    // Adapted from Brackey's NavMesh Tutorial
    // https://www.youtube.com/watch?v=CHV1ymlw-P8

    [SerializeField] public Camera cam;
    private PlayerInputs actions;
    // This didn't work out, I couldn't use the bool out of this for checking how I wanted in my AimRaycastController.cs class
    // public UnityEvent<bool> HoldingAim = new UnityEvent<bool>();
    public bool isHoldingAim = false;

    void Awake() {
        actions = new PlayerInputs();
    }

    void OnEnable() {
        actions.Player.Aim.Enable();
        actions.Player.Aim.performed += Aim;
        actions.Player.Aim.canceled += AimCanceled;
    }

    void OnDisable() {
        actions.Player.Aim.performed -= Aim;
        actions.Player.Aim.canceled -= AimCanceled;
        actions.Player.Aim.Disable();
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
        //GetComponent<LineRenderer>().enabled = true;
    }

    void AimCanceled(InputAction.CallbackContext context) {
        isHoldingAim = false;
        Debug.Log("Aiming Stopped");
        //GetComponent<LineRenderer>().enabled = false;
    }

    void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
