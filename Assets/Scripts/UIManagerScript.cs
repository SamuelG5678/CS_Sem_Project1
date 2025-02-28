using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;

    public GameObject time_object;
    public GameObject levelname_object;
    private TextMeshProUGUI time_text;
    private TextMeshProUGUI levelname_text;

    public Slider redSlider;
    public Slider blueSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        time_text = time_object.GetComponent<TextMeshProUGUI>();
        levelname_text = levelname_object.GetComponent<TextMeshProUGUI>();
        if (time_text == null)
        {
            Debug.Log("time text in UI not found");
        }
        if (levelname_text == null)
        {
            Debug.Log("level name text in UI not found");
        }
    }

    public void SetTimerText(string current_time)
    {
        time_text.text = current_time;
    }

    public void SetLevelNameText(string current_level)
    {
        levelname_text.text = current_level;
    }

    public void UpdateSliderValue(string pickupColor, int newSliderValue)
    {
        if (pickupColor == "red")
        {
            redSlider.value = newSliderValue;
        } else if (pickupColor == "blue")
        {
            blueSlider.value = newSliderValue;
        }
    }
}
