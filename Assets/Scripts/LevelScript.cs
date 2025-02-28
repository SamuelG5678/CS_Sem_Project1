using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public GameObject spawnPoint;
    public static LevelScript instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
}
