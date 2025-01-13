using UnityEngine;

namespace GolfBots.State {

/// <summary>
/// A condition that is met when Level 7's Button Press Event is raised.
/// </summary>
[CreateAssetMenu(fileName = "ButtonPressCondition", menuName = "GD/Conditions/Single/ButtonPress", order = 6)]
public class ButtonPressCondition : GD.State.ConditionBase {

    [Tooltip("The Button's Level ID that triggers this condition.")]
    [SerializeField]
    private int requiredLevelID = 7;

    /// <summary>
    /// Subscribe to he Button Event.
    /// Sets the priority level to highest by default.
    /// </summary>
    private void OnEnable() {
        PriorityLevel = GD.Types.PriorityLevel.Highest;
        GolfBots.State.EventManager.Instance.OnButtonPress += OnButtonPress;
    }

    /// <summary>
    /// Unsubscribe from event when disabled.
    /// </summary>
    private void OnDisable() {
        GolfBots.State.EventManager.Instance.OnButtonPress -= OnButtonPress;
    }

    /// <summary>
    /// Handles the button press event to check if the condition is met.
    /// </summary>
    private void OnButtonPress(int levelID) {
        if (levelID == requiredLevelID && !IsMet) {
            IsMet = true;
        }
    }

    /// <summary>
    /// Evaluates the condition logic.
    /// </summary>
    protected override bool EvaluateCondition(GolfBots.State.ConditionContext conditionContext) {
        return IsMet;
    }
}

}
