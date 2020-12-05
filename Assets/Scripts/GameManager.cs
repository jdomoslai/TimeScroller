using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundElement[] backgroundElements = null; // Ground

    [SerializeField]
    public static List<MovableElement> movableElements = null; // Background objects

    private HighScores highScores = new HighScores { highScoreEntries = new List<HighScoreEntry>() };

    //for the source
    private Text distanceText;
    private float f_dis = 0;
    private int dis = 0;

    private void Awake()
    {
        ScoreSystem.Init();
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        movableElements = new List<MovableElement>();
    }

    private void UpdateDistance()
    {
        f_dis += 1 * Time.deltaTime;
        dis = (int)f_dis;

        distanceText.text = "Score:" + dis.ToString();
    }

    //this is for upadte the times( i need it to put player controller)
    public void UpdateBonus(int count)
    {
        dis += count;
        distanceText.text = "Score:" + dis.ToString();

    }

    //this is for the algorithm for timed release of power ups
    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.gameObject.tag == "Bonus1")
    //    {
    //        GameManager.Instance.UpdateBonus(5);
    //        //gameManager.UpdateBonus(5);
    //        Destroy(coll.gameObject);
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
        foreach (BackgroundElement element in backgroundElements)
        {
            element.Move();
        }

        if (movableElements.Count > 0)
        {
            foreach (MovableElement element in movableElements)
            {
                element.Move();
            }
        }

        // Exit to main menu
        if (Input.GetKeyDown("escape"))
        {
            Save();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    // Saves scores to file
    private void Save()
    {
        // Add and sort entries
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = dis };

        highScores.highScoreEntries.Add(highScoreEntry);

        highScores.highScoreEntries = highScores.highScoreEntries.OrderByDescending(x => x.score).Take(10).ToList();

        string json = JsonUtility.ToJson(highScores);

        ScoreSystem.Save(json);
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
