using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float speed = 1;

    // Moves Enemies to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }

    private void Start()
    {
        // Start enemy speed faster at higher scores
        speed = GameManager.enemySpeed;
    }

    private void Update()
    {
        // If score is high enough increase enemy speed
        if (GameManager.dis > GameManager.enemyThresholdCount)
        {
            GameManager.enemyThresholdCount += GameManager.enemyThreshold;
            GameManager.enemySpeed += GameManager.enemyIncrease;
        }
    }
}
