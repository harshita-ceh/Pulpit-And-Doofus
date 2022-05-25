using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour {
    private bool scoreUpdated = false;
    private float animationTimer = 0.15f;
    public PlatformTimerUI timerUI;

    public void PlacePlatform(float destroyTimer) {
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, animationTimer).OnComplete(delegate () {
            timerUI.InitTimer(destroyTimer);
            Invoke(nameof(DoDestroy), destroyTimer - animationTimer);
            //Destroy(gameObject, destroyTimer);
        });
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Player") && !scoreUpdated) {
            GameManager.instance.UpdateScore(1);
            scoreUpdated = true;
        }
    }

    private void DoDestroy() {
        transform.DOScale(Vector3.zero, 0.15f).OnComplete(delegate () {
            Destroy(gameObject);
        });
    }
}
