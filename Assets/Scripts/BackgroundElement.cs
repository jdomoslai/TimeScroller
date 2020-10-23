using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BackgroundElement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform neighbour;

    [SerializeField]
    private MovableElement[] movableElements;

    private MovableElement currentObject = null;

    // Moves GameObjects to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }

    // Snaps ground to neighbours position to simulate endless ground
    public void SnapToNeighbour()
    {
        transform.position = new Vector2(neighbour.position.x + 20, transform.position.y);
    }

    // onTrigger collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reset ground and background spawn positions
        if (collision.tag == "Reset")
        {
            SnapToNeighbour();

            // Destroy unused GameObjects
            if (currentObject)
            {
                string name = currentObject.name;
                GameManager.movableElements.Remove(currentObject);
                Destroy(GameObject.Find(name));
            }

            // Randomly spawn background objects
            int rand = UnityEngine.Random.Range(0, movableElements.Length);
            currentObject = movableElements[rand];

            currentObject = Instantiate(currentObject, new Vector3(20, 0.4f, 0), Quaternion.identity);
            GameManager.movableElements.Add(currentObject);
        }
    }
}
