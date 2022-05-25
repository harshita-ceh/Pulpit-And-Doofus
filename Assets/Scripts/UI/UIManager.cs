using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public Button startGameBtn;

    public GameObject hudScreen;

    public GameObject gameOverScreen;
    public Button retryBtn;

    private void Start() {
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener(delegate() {
            ShowStartScreen(false);
            ShowGameOverScreen(false);
            ShowHUDScreen(true);
            GameManager.instance.StartGame();
        });

        retryBtn.onClick.RemoveAllListeners();
        retryBtn.onClick.AddListener(delegate() {
            ShowStartScreen(false);
            ShowGameOverScreen(false);
            ShowHUDScreen(true);
            GameManager.instance.StartGame();
        });

        ShowStartScreen(true);
    }

    public void ShowStartScreen(bool state) {
        startScreen.gameObject.SetActive(state);
    }

    public void ShowHUDScreen(bool state) {
        hudScreen.gameObject.SetActive(state);
    }

    public void ShowGameOverScreen(bool state) {
        gameOverScreen.gameObject.SetActive(state);
    }
}
