using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformTimerUI : MonoBehaviour {
    public TMP_Text text;

    private float timer = 0;

    private void Awake() {
        text.text = "";
    }

    private void Update() {
        if(timer > 0) {
            timer -= Time.deltaTime;

            text.text = (Mathf.Round(timer * 100f) / 100f).ToString();
        }
    }

    public void InitTimer(float timer) {
        this.timer = timer;
    }
}
