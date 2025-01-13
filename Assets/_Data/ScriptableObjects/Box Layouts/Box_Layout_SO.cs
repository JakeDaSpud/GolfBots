using UnityEngine;

namespace GolfBots.Level {
    [CreateAssetMenu(fileName = "New Level Box Locations", menuName = "Level Box Locations")]
    public class BoxLayout : ScriptableObject {

        [SerializeField] public int LevelID = 0;
        [SerializeField] public Transform[] BoxLocations;
    }
}
