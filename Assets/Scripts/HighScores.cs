using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScores
{
    public List<HighScoreEntry> highScoreEntries;

    // Saves scores to file
    public void Save(int newScore)
    {
        // Add and sort entries
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = newScore };

        highScoreEntries.Add(highScoreEntry);

        highScoreEntries = highScoreEntries.OrderByDescending(x => x.score).Take(10).ToList();

        string json = JsonUtility.ToJson(this);

        ScoreSystem.Save(json);
    }

    // Loads scores from file
    public HighScores Load()
    {
        string scoreString = ScoreSystem.Load();

        if (scoreString != null)
        {
            return JsonUtility.FromJson<HighScores>(scoreString);
        }

        return new HighScores { highScoreEntries = new List<HighScoreEntry>() };
    }
}
