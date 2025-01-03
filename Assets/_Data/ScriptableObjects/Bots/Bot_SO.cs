using UnityEngine;

[CreateAssetMenu]
public class BotData : ScriptableObject {

    [SerializeField] private float moveSpeed = 100.0f;
    [SerializeField] private int maxReflections = 3;
    
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpInterval = 20.0f;
}
