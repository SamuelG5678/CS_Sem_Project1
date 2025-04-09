using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplayScript : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public void DisplayHighScore(string name, float score)
    {
        nameText.text = name;
        //int timerIntLength = Mathf.Round(score).ToString().Length;
        //scoreText.text = score.ToString().Substring(0, (3 + timerIntLength));
        scoreText.text = string.Format("{0:000.000}", score);
    }

    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}