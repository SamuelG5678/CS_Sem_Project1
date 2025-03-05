using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    [Header("Canvases")]
    public GameObject menuCanvas;
    public GameObject deathscreenCanvas;
    public GameObject UICanvas;
    public GameObject winCanvas;

    [Header("Player Stuff")]
    public GameObject player;

    [Header("Levels")]
    public List<GameObject> levels;
    public TMP_Dropdown levelDropdown;
    public GameObject currentSpawnPoint;
    public int currentLevel;
    public string currentLevelName;

    [Header("Misc")]
    public bool timerIsRunning = false;
    public float runTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        currentLevel = 0;
        menuCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
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
        Instantiate(levels[currentLevel]);
        currentSpawnPoint = levels[currentLevel].GetComponent<LevelScript>().spawnPoint;
        currentLevelName = levels[currentLevel].name;
        player.transform.position = currentSpawnPoint.transform.position;

        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.spawn);
        runTime = 0f;
        timerIsRunning = true;
        PlayerScript.instance.UnfreezePlayer();
    }

    public void ClearLevel()
    {
        Debug.Log("ClearLevel triggered");
        LevelScript currentLevelInstance = Object.FindFirstObjectByType<LevelScript>();
        Destroy(currentLevelInstance.gameObject);
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
        winCanvas.SetActive(false);
        UICanvas.SetActive(true);
        ClearLevel();
        StartLevel();
    }

    public void ReturnToMenu()
    {
        ClearLevel();
        timerIsRunning = false;
        deathscreenCanvas.SetActive(false);
        winCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        PlayerScript.instance.FreezePlayer();
    }

    public void PlayerDeath()
    {
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.die);
        timerIsRunning = false;
        UICanvas.SetActive(false);
        deathscreenCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
    }

    public void WinLevel()
    {
        //set win time for level
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.winlevel);
        timerIsRunning = false;
        UICanvas.SetActive(false);
        winCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
    }

    public void SetLevel()
    {
        currentLevel = levelDropdown.value;
    }
}
