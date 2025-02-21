using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject menuCanvas;
    public GameObject deathscreenCanvas;
    public GameObject UICanvas;

    public GameObject spawnPoint;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
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
        deathscreenCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void PlayerDeath()
    {
        UICanvas.SetActive(false);
        deathscreenCanvas.SetActive(true);
    }
}
