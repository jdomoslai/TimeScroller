using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    int score;
    int Time;
    int rewarded;
    public Transform player;
    public Text scoreText;
    public static string scoreString;
    public GameObject walls;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score:0";
    }

    // Update is called once per frame
    void Update()
    {
        Time += 1;
        rewarded += 1;
        if(Time == 10)
        {
            score += 1;
            scoreText.text = "Score:" + score.ToString();
            Time = 0;
        }
        if(rewarded == 1000)// when player got 100 score, their score will be double.
        {
            score *= 2;
            scoreText.text = "Score:" + score.ToString();
            rewarded = 0;
        }

    }

    //game over if the player hit the something
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Tree")
        {
            Start();
        }
    }

}
