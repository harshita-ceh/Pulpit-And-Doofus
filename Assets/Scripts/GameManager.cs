using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : SingletonGeneric<GameManager>
{
    public GameObject platformPrefab;
    public PlayerMotor player;
    public ScoreController scoreController;
    public UIManager uiManager;

    private float minPlatformDestroyTime = 4f;
    private float maxPlatformDestroyTime = 5f;
    private float platformSpawn = 2f;
    private float spawnTimer = 0f;

    private Platform currentPlatform;

    private bool isGameOver;

    public static GameManager instance;

    private void Start() {
        StartCoroutine(FetchData());
    }

    private IEnumerator FetchData() {
        UnityWebRequest www = new UnityWebRequest("https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json", "GET");
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if(www.error != null) {
            Debug.LogError("Error with API");
        } else {
            Debug.Log("Data fetched : " + www.downloadHandler.text);
            DoofusDataModel data = DoofusDataModel.CreateFromJSON(www.downloadHandler.text);
            if(data.player_data != null) {
                player.playerSpeed = data.player_data.speed;
            }
            if(data.pulpit_data != null) {
                minPlatformDestroyTime = data.pulpit_data.min_pulpit_destroy_time;
                maxPlatformDestroyTime = data.pulpit_data.max_pulpit_destroy_time;
                platformSpawn = data.pulpit_data.pulpit_spawn_time;
            }
        }
    }

    private void Update() {
        if(isGameOver) {
            return;
        }

        if(spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;

            if(spawnTimer < 0) {
                AddNewPlatform(currentPlatform.transform.position, currentPlatform.transform.localScale);
            }
        }

        if(player.transform.position.y < -1) {
            OnGameOver();
        }
    }

    private void AddNewPlatform(Vector3 currentPos, Vector3 currentScale) {
        Vector3 nextPos = NextPlatformPosition();
        
        Vector3 newPos = new Vector3(currentPos.x + currentScale.x * nextPos.x, 0f, currentPos.z + currentScale.z * nextPos.z);

        currentPlatform = Instantiate(platformPrefab, newPos, Quaternion.identity, transform).GetComponent<Platform>();
        currentPlatform.PlacePlatform(Random.Range(minPlatformDestroyTime,maxPlatformDestroyTime)); ;

        spawnTimer = platformSpawn;
    }

    private Vector3 NextPlatformPosition() {
        int random = Random.Range(0, 4);
        switch(random) {
            case 0:
                return Vector3.left;
            case 1:
                return Vector3.right;
            case 2:
                return Vector3.forward;
            case 3:
                return Vector3.back;
            default:
                return Vector3.forward;
        }
    }

    public void UpdateScore(int amount) {
        scoreController.UpdateScore(amount);
    }

    public void StartGame() {
        isGameOver = false;

        if(currentPlatform != null) {
            Destroy(currentPlatform.gameObject);
            currentPlatform = null;
        }

        player.gameObject.SetActive(false);
        AddNewPlatform(Vector3.zero, Vector3.zero);
        player.gameObject.SetActive(true);
        scoreController.InitScore();
    }

    public void OnGameOver() {
        isGameOver = true;
        player.gameObject.SetActive(false);
        uiManager.ShowGameOverScreen(true);

        foreach(Transform platform in transform) {
            Destroy(platform.gameObject);
        }
    }
}
