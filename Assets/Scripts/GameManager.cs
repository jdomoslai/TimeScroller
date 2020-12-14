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

    public static float initPlayerPosition; // Initial player position

    public static HighScores highScores = new HighScores { highScoreEntries = new List<HighScoreEntry>() };

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
        highScores = highScores.Load();
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
            highScores.Save(dis);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (CharacterMovement.death)
        {
            highScores.Save(dis);
        }
    }
}
