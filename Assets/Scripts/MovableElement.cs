using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableElement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;

    // Moves GameObjects to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }
}
