using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace GolfBots.UI {

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;

    public static bool isPaused = false;

    private void OnEnable() {
        GolfBots.State.EventManager.Instance.OnPause += TryPause;
    }

    private void OnDisable() {
        GolfBots.State.EventManager.Instance.OnPause -= TryPause;
    }

    private void TryPause() {
        
        Debug.Log("TryPause()");
        if (!isPaused)
            Pause();

        else
            Unpause();
    }

    private void SetLevelText() {
        float playtime = GD.State.StateManager.Playtime;
        int minutes = Mathf.FloorToInt(playtime / 60F);
        int seconds = Mathf.FloorToInt(playtime - minutes * 60);
        
        Transform temp = pauseMenu.transform.Find("Background/Menu/ProgrammablePlaytimeText");
        TMPro.TextMeshProUGUI playtimeText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        playtimeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    // Formatting taken from https://discussions.unity.com/t/making-a-timer-00-00-minutes-and-seconds/14318/4
    private void SetPlaytimeText() {
        Transform temp = pauseMenu.transform.Find("Background/Menu/ProgrammableLevelText");
        TMPro.TextMeshProUGUI levelText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        // Delegate to pad the leeel number if it's 1 digit
        Func<int, string> newLevelNumber = num =>
            num < 10 ? "0" + num.ToString() : num.ToString();

        levelText.SetText("Funghi Forest " + newLevelNumber(GD.State.StateManager.currentLevel));
    }

    public void Pause() {
        Debug.Log("Game paused");
        SetPlaytimeText();
        SetLevelText();
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Unpause() {
        Debug.Log("Game unpaused");
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

}

}