using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Adapted from Brackey's NavMesh Tutorial
    // https://www.youtube.com/watch?v=CHV1ymlw-P8

    [SerializeField]
    private Camera cam;

    void OnMove() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            this.GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }
    }
}
