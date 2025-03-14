using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.EditorUtilities;
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
    public GameObject highscoreCanvas;

    [Header("Player Stuff")]
    public GameObject player;
    public string playerName;

    [Header("Levels")]
    public List<GameObject> levels;
    public TextMeshProUGUI levelNameUIComp;
    public TMP_Dropdown levelDropdown;
    public GameObject currentSpawnPoint;
    public int currentLevel;
    public string currentLevelName;
    string runTimeText = "0";

    [Header("Misc")]
    public bool timerIsRunning = false;
    public float runTime = 0;
    public TMP_InputField playerNameInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        currentLevel = 0;
        menuCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
        AudioManagerScript.instance.PlayMusic(AudioManagerScript.instance.menuMusic);

        HighScoreScript.instance.AddNewScore(HighScoreScript.instance.levelOneScores, playerName, runTime);
        HighScoreScript.instance.AddNewScore(HighScoreScript.instance.levelTwoScores, playerName, runTime);
        HighScoreScript.instance.AddNewScore(HighScoreScript.instance.levelThreeScores, playerName, runTime);
    }

    private void FixedUpdate()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        if (timerIsRunning)
        {
            runTime += Time.deltaTime;
            int timerIntLength = Mathf.Round(runTime).ToString().Length;
            runTimeText = runTime.ToString().Substring(0, (3 + timerIntLength));
        }
        UIManagerScript.instance.SetTimerText(runTimeText);
    }

    public void StartLevel()
    {
        Instantiate(levels[currentLevel]);
        currentSpawnPoint = levels[currentLevel].GetComponent<LevelScript>().spawnPoint;
        currentLevelName = levels[currentLevel].GetComponent<LevelScript>().levelName;
        levelNameUIComp.text = currentLevelName;
        player.transform.position = currentSpawnPoint.transform.position;

        AudioManagerScript.instance.PlayMusic(AudioManagerScript.instance.levelMusic);
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
        highscoreCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        UICanvas.SetActive(true);
        StartLevel();
    }

    public void RestartLevel()
    {
        highscoreCanvas.SetActive(false);
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
        highscoreCanvas.SetActive(true);

        PlayerScript.instance.FreezePlayer();
        AudioManagerScript.instance.PlayMusic(AudioManagerScript.instance.menuMusic);
    }

    public void PlayerDeath()
    {
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.die);
        timerIsRunning = false;
        UICanvas.SetActive(false);
        deathscreenCanvas.SetActive(true);
        highscoreCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
    }

    public void WinLevel()
    {
        //set win time for level
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.winlevel);
        highscoreCanvas.SetActive(true);
        UpdateDisplayWithCurrentLevel();

            timerIsRunning = false;
        UICanvas.SetActive(false);
        winCanvas.SetActive(true);
        PlayerScript.instance.FreezePlayer();
    }

    public void SetLevel()
    {
        currentLevel = levelDropdown.value;
    }
    public void SetPlayerName()
    {
        playerName = playerNameInput.text;
    }

    public void UpdateDisplayWithCurrentLevel()
    {
        if (currentLevel == 0)
        {
            HighScoreScript.instance.UpdateDisplay(HighScoreScript.instance.levelOneScores);
        }
        else if (currentLevel == 1)
        {
            HighScoreScript.instance.UpdateDisplay(HighScoreScript.instance.levelTwoScores);
        }
        else if (currentLevel == 2)
        {
            HighScoreScript.instance.UpdateDisplay(HighScoreScript.instance.levelThreeScores);
        }
        else
        {
            Debug.Log("current level isn't 0-4 somehow");
        }
    }
}
