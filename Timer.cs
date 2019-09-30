using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text gameTimerText;
    float gameTimer = 0f;
    public GameObject manager;
    private GameController controller;
    public GameObject enemy;
    float spawnRate = 10f;
    public GameObject[] spawnPoints = new GameObject[4];
    int difficulty = -1;

    void Start() {
        controller = manager.GetComponent<GameController>();
        InvokeRepeating("SpawnMimic", 5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        // game timer
        if (!controller.gameover) {
          gameTimer += Time.deltaTime;
          int seconds = (int)(gameTimer % 60);
          int minutes = (int)(gameTimer / 60) % 60;
          int hours = (int)(gameTimer/3600) % 24;
          string TimerString = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
          gameTimerText.text = TimerString;
        }

        // game countdown and spawnRate
        float timeSoFar = gameTimer;
        if (timeSoFar > 60 && difficulty < 4) {
            controller.countdown = 10f;
            spawnRate = 7f;
            difficulty = 4;
            changeSpawnRate();
            Debug.Log(difficulty);
        } else if (timeSoFar > 50 && difficulty < 3) {
            controller.countdown = 7f;
            spawnRate = 8f;
            difficulty = 3;
            changeSpawnRate();
            Debug.Log(difficulty);
        } else if (timeSoFar > 40 && difficulty < 2) {
            controller.countdown = 5f;
            spawnRate = 10f;
            difficulty = 2;
            changeSpawnRate();
            Debug.Log(difficulty);
        } else if (timeSoFar > 20 && difficulty < 1) {
            spawnRate = 12f;
            controller.countdown = 3f;
            difficulty = 1;
            changeSpawnRate();
            Debug.Log(difficulty);
        } else if (difficulty <= 0) {
            controller.countdown = 2f;
            difficulty = 0;
            Debug.Log(difficulty);
        }

        if (controller.gameover) {
            CancelInvoke();
        }

    }

    void SpawnMimic() {
        for (int i = 0; i < 4; i++) {
          Instantiate(enemy, spawnPoints[i].transform);
        }
    }

    void changeSpawnRate() {
        CancelInvoke("SpawnMimic");
        InvokeRepeating("SpawnMimic", 1f, spawnRate);
    }

}
