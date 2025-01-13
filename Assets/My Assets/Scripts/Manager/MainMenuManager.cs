using UnityEngine;
using UnityEngine.SceneManagement;

// This video was used to make this Menu: https://youtu.be/DX7HyN7oJjE?si=a4BMsRd-Ofy-0Ooe

namespace GolfBots.UI {
public class MainMenuManager : MonoBehaviour {

    /// <summary>
    /// Starts the First Level Scene (Funghi Forest).
    /// </summary>
    public void Play() {
        SceneManager.LoadSceneAsync(1);
    }

    /// <summary>
    /// Starts the Main Menu Scene.
    /// </summary>
    public void MainMenu() {
        GD.State.StateManager.Reset();
        SceneManager.LoadSceneAsync(0);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void Quit() {
        Application.Quit();
    }

}

}