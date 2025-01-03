using UnityEngine;

[CreateAssetMenu(fileName = "New Bot", menuName = "Bot Type")]
public class BotData : ScriptableObject {

    [SerializeField] public float moveSpeed = 100.0f;
    [SerializeField] public int maxReflections = 3;
    
    [SerializeField] public bool canJump = false;
    [SerializeField] public float jumpInterval = 20.0f;
}