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

    // Moves GameObjects to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }

    // Trigger to hide GameObjects and push back player on collision
    // Does not destroy GameObject as MovableObstacles script handles create/destroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!platform && collision.tag == "Player")
        {
            // Remove colliders
            Collider2D[] colliderArray = GetComponents<Collider2D>();

            foreach (var collider in colliderArray)
            {
                collider.enabled = !collider.enabled;
            }

            // Set GameObject visibility
            GetComponent<Renderer>().enabled = false;

            // Push player's x position back
            float newPositionX = GameObject.FindGameObjectWithTag("Player").transform.position.x - 1.0f;
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(newPositionX, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        }
    }
}
