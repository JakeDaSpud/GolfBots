using UnityEngine;

namespace GolfBots.Level {
    [CreateAssetMenu(fileName = "New Level Box Locations", menuName = "Level Box Locations")]
    public class BoxLayoutSO : ScriptableObject {

        [SerializeField] public int LevelID = 0;
        [SerializeField] public Vector3[] BoxLocations;
    }
}
