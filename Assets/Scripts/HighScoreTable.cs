using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private HighScores highScores = new HighScores { highScoreEntries = new List<HighScoreEntry>() };
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        ScoreSystem.Init();

        // Templates and containers
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        Load();

        // Sort entries
        highScores.highScoreEntries = highScores.highScoreEntries.OrderByDescending(x => x.score).Take(10).ToList();

        // Output
        highscoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntries)
        {
            CreateHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    // Creates a highscore entry
    private void CreateHighscoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        // Position
        float templateHeight = 25f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // Entry data
        int rank = transformList.Count + 1;
        int score = highScoreEntry.score;

        entryTransform.Find("PosText").GetComponent<TMP_Text>().text = rank.ToString();
        entryTransform.Find("ScrText").GetComponent<TMP_Text>().text = score.ToString();
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 0);

        transformList.Add(entryTransform);
    }

    // Loads scores from file
    private void Load()
    {
        string scoreString = ScoreSystem.Load();

        if (scoreString != null)
        {
            highScores = JsonUtility.FromJson<HighScores>(scoreString);
        }
    }

    /*
     * Inner HighScores class
     * Houses List of HighScoreEntries
     */
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntries;
    }

    /*
     * Inner HighScoreEntryClass
     * A serializable highscore entry
     */
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
    }
}
