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
}
