using System.Collections.Generic;
using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    public HighScoreDisplayScript[] highScoreDisplayArray;
    public static HighScoreScript instance;

    public List<HighScoreEntry> levelOneScores = new List<HighScoreEntry>();
    public List<HighScoreEntry> levelTwoScores = new List<HighScoreEntry>();
    public List<HighScoreEntry> levelThreeScores = new List<HighScoreEntry>();
    public List<HighScoreEntry> levelFourScores = new List<HighScoreEntry>();
    public List<HighScoreEntry> levelFiveScores = new List<HighScoreEntry>();

    void Start()
    {
        instance = this;
    }

    public void UpdateDisplay(List<HighScoreEntry> scores)
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.time.CompareTo(x.time));

        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].playerName, scores[i].time);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }

    public void AddNewScore(List<HighScoreEntry> scores, string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { playerName = entryName, time = entryScore });
    }
}