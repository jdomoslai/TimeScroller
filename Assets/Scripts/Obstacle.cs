using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool stationary;

    [SerializeField]
    private bool platform;

    private void Start()
    {
        // Start obstacle speed faster at higher scores
        speed = GameManager.obstacleSpeed;
    }

    private void Update()
    {
        // If score is high enough increase obstacle speed
        if (GameManager.dis > GameManager.obstacleThresholdCount)
        {
            GameManager.obstacleThresholdCount += GameManager.obstacleThreshold;
            GameManager.obstacleSpeed += GameManager.obstacleIncrease;
        }
    }

    // Moves GameObjects to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }

    // Trigger for Obstacle penalty
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Delete();

            // Push player's x position back
            float newPositionX = GameObject.FindGameObjectWithTag("Player").transform.position.x - 1.0f;
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(newPositionX, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        }

        if (collision.tag == "PowerReset")
        {
            Delete();
        }
    }

    // Destroys and removes current Obstacle from play
    private void Delete()
    {
        if (MovableObstacle.movingPlatforms.Contains(this))
        {
            MovableObstacle.movingPlatforms.Remove(this);
        }
        else if (MovableObstacle.movingObstacles.Contains(this))
        {
            MovableObstacle.movingObstacles.Remove(this);
        }

        Destroy(this.gameObject);
    }
}
