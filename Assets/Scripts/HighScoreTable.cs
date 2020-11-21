using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntries;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        // Templates and containers
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        // Example entries
        highScoreEntries = new List<HighScoreEntry>()
        {
            new HighScoreEntry {score = 999 },
            new HighScoreEntry {score = 125 },
            new HighScoreEntry {score = 698 },
            new HighScoreEntry {score = 222 },
            new HighScoreEntry {score = 436 },
            new HighScoreEntry {score = 854 },
            new HighScoreEntry {score = 752 },
            new HighScoreEntry {score = 50 },
            new HighScoreEntry {score = 100 },
            new HighScoreEntry {score = 888 },
        };

        // Todo: Load JSON File
        //string jsonString = PlayerPrefs.GetString("HighScoreTable");
        //HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        // Sort entries
        for (int i = 0; i < highScoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highScoreEntries.Count; j++)
            {
                if (highScoreEntries[j].score > highScoreEntries[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScoreEntries[i];
                    highScoreEntries[i] = highScoreEntries[j];
                    highScoreEntries[j] = tmp;
                }
            }
        }

        // Output
        highscoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highScoreEntry in highScoreEntries)
        {
            CreateHighscoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    // Example Save and Add
    //private void AddHighScoreEntry(int score)
    //{
    //    HighScoreEntry highScoreEntry = new HighScoreEntry { score = score };

    //    string jsonString = PlayerPrefs.GetString("HighScoreTable");
    //    HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

    //    highScores.highScoreEntries.Add(highScoreEntry);

    //    string json = JsonUtility.ToJson(highScores);
    //    PlayerPrefs.SetString("HighScoreTable", json);
    //    PlayerPrefs.Save();
    //}

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
