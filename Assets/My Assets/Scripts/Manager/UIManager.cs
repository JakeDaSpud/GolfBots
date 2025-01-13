using System;
using GolfBots.Bots;
using UnityEngine;

namespace GolfBots.UI {

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject botUI;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Sprite[] botIcons;

    public static bool isPaused = false;

    private void OnEnable() {
        GolfBots.State.EventManager.Instance.OnPause += TryPause;
        GolfBots.State.EventManager.Instance.OnSetBotUI += SetBotUI;
        GolfBots.State.EventManager.Instance.OnWin += WinScreen;
    }

    private void OnDisable() {
        GolfBots.State.EventManager.Instance.OnPause -= TryPause;
        GolfBots.State.EventManager.Instance.OnSetBotUI -= SetBotUI;
        GolfBots.State.EventManager.Instance.OnWin -= WinScreen;
    }

    private void SetBotUI(BotUIPacket botUIPacket) {
        // Set icon
        Transform temp = botUI.transform.Find("BotIcon");
        UnityEngine.UI.Image botIcon = temp.gameObject.GetComponent<UnityEngine.UI.Image>();

        int newIconId = botUIPacket.type == BotType.MineBot ? 0 : 1;
        botIcon.sprite = botIcons[newIconId];

        // Set amount
        temp = botUI.transform.Find("BotAmount");
        TMPro.TextMeshProUGUI botAmount = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        botAmount.SetText(botUIPacket.amount.ToString());
    }

    private void WinScreen() {
        Time.timeScale = 0;
        isPaused = true;

        // Set Playtime
        float playtime = GD.State.StateManager.Playtime;
        int minutes = Mathf.FloorToInt(playtime / 60F);
        int seconds = Mathf.FloorToInt(playtime - minutes * 60);
        
        Transform temp = winScreen.transform.Find("Background/Menu/ProgrammablePlaytimeText");
        TMPro.TextMeshProUGUI currentText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        currentText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));

        // Set Restarts
        temp = winScreen.transform.Find("Background/Menu/ProgrammableRestartsText");
        currentText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        currentText.SetText(GD.State.StateManager.Restarts.ToString());

        // Set Bots Used
        temp = winScreen.transform.Find("Background/Menu/ProgrammableBotsUsedText");
        currentText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        currentText.SetText(GD.State.StateManager.BotsUsed.ToString());

        winScreen.SetActive(true);
    }

    private void TryPause() {
        
        Debug.Log("TryPause()");
        if (!isPaused)
            Pause();

        else
            Unpause();
    }

    // Formatting taken from https://discussions.unity.com/t/making-a-timer-00-00-minutes-and-seconds/14318/4
    private void SetLevelText() {
        float playtime = GD.State.StateManager.Playtime;
        int minutes = Mathf.FloorToInt(playtime / 60F);
        int seconds = Mathf.FloorToInt(playtime - minutes * 60);
        
        Transform temp = pauseMenu.transform.Find("Background/Menu/ProgrammablePlaytimeText");
        TMPro.TextMeshProUGUI playtimeText = temp.gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        playtimeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

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