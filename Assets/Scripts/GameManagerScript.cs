using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    [Header("Canvases")]
    public GameObject menuCanvas;
    public GameObject deathscreenCanvas;
    public GameObject UICanvas;
    public GameObject winCanvas;

    [Header("Player Stuff")]
    public GameObject spawnPoint;
    public GameObject player;

    [Header("Levels")]
    public List<GameObject> levels;
    public List<GameObject> spawnPoints;
    public int currentLevel;

    [Header("Misc")]
    public bool timerIsRunning = false;
    public float runTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        currentLevel = 0;
    }

    private void FixedUpdate()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        string runTimeText = "0";
        if (timerIsRunning)
        {
            runTime += Time.deltaTime;
            int timerIntLength = Mathf.Round(runTime).ToString().Length;
            runTimeText = runTime.ToString().Substring(0, (3 + timerIntLength));
        }
        //Debug.Log(runTimeText);
        UIManagerScript.instance.SetTimerText(runTimeText);
    }

    public void StartLevel()
    {
        runTime = 0f;
        timerIsRunning = true;

        spawnPoint = spawnPoints[currentLevel];
        player.transform.position = spawnPoint.transform.position;
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);
        UICanvas.SetActive(true);
        StartLevel();
    }

    public void RestartLevel()
    {
        deathscreenCanvas.SetActive(false);
        UICanvas.SetActive(true);
        StartLevel();
    }

    public void ReturnToMenu()
    {
        timerIsRunning = false;
        deathscreenCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void PlayerDeath()
    {
        timerIsRunning = false;
        UICanvas.SetActive(false);
        deathscreenCanvas.SetActive(true);
    }

    public void WinLevel()
    {
        //set win time for level
        timerIsRunning = false;
        UICanvas.SetActive(false);
        winCanvas.SetActive(true);
    }
}
