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
    private BackgroundElement[] backgroundElements; // Ground

    [SerializeField]
    public static List<MovableElement> movableElements; // Background objects

    public static float initPlayerPosition; // Initial player position

    private HighScores highScores = new HighScores { highScoreEntries = new List<HighScoreEntry>() };

    //for the source
    private Text distanceText;
    private float f_dis = 0;
    public static int dis = 0;

    public static int enemyThresholdCount;      // Score to increase speeds
    public static float enemySpeed = 5.0f;      // Current enemy speed
    public static float enemyIncrease = 0.5f;   // Amount to increase enemy speed by
    public static int enemyThreshold = 75;      // Amount to add to enemy score threshold count

    public static int obstacleThresholdCount;      // Score to increase speeds
    public static float obstacleSpeed = 5.0f;      // Current enemy speed
    public static float obstacleIncrease = 0.5f;   // Amount to increase enemy speed by
    public static int obstacleThreshold = 100;      // Amount to add to enemy score threshold count

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        ScoreSystem.Init();
        Load();
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        movableElements = new List<MovableElement>();
        enemyThresholdCount = enemyThreshold;
        obstacleThresholdCount = obstacleThreshold;
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
        f_dis += (count * 2);
        dis = (int)f_dis;
        distanceText.text = "Score:" + dis.ToString();

    }

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
