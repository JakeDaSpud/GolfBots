using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GolfBots.Player {
    public class AimRaycastController : MonoBehaviour {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LineRenderer lr;

        [SerializeField] private Transform reflectionStart;

        [SerializeField] private float maxReflectionDistance = 100f;
        [SerializeField] private int maxAimReflections = 3;

        // Some Line Renderer Code was adapted from this video https://youtu.be/5ZBynjAsfwI?si=ToMuwr1NGvbByHtJ

        private void Awake() {
            if (playerController == null)
                Debug.Log("Player Controller not assigned.");

            if (lr == null)
                Debug.Log("Line Renderer not assigned.");
        }

        void Update() {
            CheckHoldingAim(playerController.isHoldingAim);
        }

        public void CheckHoldingAim(bool isHoldingAim) {
            if (isHoldingAim) {
                
                lr.enabled = true;

                Ray mouseRay = playerController.cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 mousePosition = Vector3.zero;

                if (Physics.Raycast(mouseRay, out hit)) {
                    mousePosition = hit.point;
                }

                mousePosition.y = reflectionStart.position.y;

                Vector3[] aimPoints = FindReflection(reflectionStart.position, (mousePosition - reflectionStart.position).normalized);
                lr.positionCount = aimPoints.Length;
                lr.SetPositions(aimPoints);
            }

            else {
                lr.enabled = false;
            }
        }

        // Physics Code adapted from https://stackoverflow.com/questions/51931455/unity3d-bouncing-reflecting-raycast
        /// <summary>
        /// Continually shoots rays that are reflected off surfaces, returning a list of points in that sequence.
        /// </summary>
        private Vector3[] FindReflection(Vector3 startPosition, Vector3 direction) {
            
            Vector3[] reflectionPoints = new Vector3[maxAimReflections + 1];
            reflectionPoints[0] = startPosition;

            int reflections = 0;
            Vector3 currentDirection = direction;
            Vector3 currentPosition = startPosition;

            // Calculate reflections
            while (reflections < maxAimReflections) {
                Ray ray = new Ray(currentPosition, currentDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxReflectionDistance)) {

                    // Add the hit point to the reflection points array
                    reflections++;
                    reflectionPoints[reflections] = hit.point;

                    // Update the direction for the next reflection
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point;
                }
                
                else {
                    reflections++;
                    reflectionPoints[reflections] = currentPosition + currentDirection * maxReflectionDistance;
                    break;
                }
            }

            // Trim the unused points in the array
            Array.Resize(ref reflectionPoints, reflections + 1);
            return reflectionPoints;
        }

        /*private Vector3[] FindReflection(Vector3 position, Vector3 direction) {
            
            Vector3[] aimPoints = new Vector3[maxAimReflections + 1];
            aimPoints[0] = position;

            int reflectionCount = 0;
            Vector3 currentDirection = direction;
            Vector3 currentPosition = position;

            if (currentAimReflections <= maxAimReflections) {

                currentAimReflections++;

                Vector3 startingPosition = position;

                Ray ray = new Ray(position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxReflectionDistance)) {
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                }

                else {
                    position += direction * maxReflectionDistance;
                }

                Debug.DrawLine(startingPosition, position, Color.magenta);

                FindReflection(position, direction);
            }
            
            return aimPoints;
        }*/
    }
}