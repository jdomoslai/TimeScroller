using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Moves GameObjects to the left
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
    }

    // Trigger for PowerUp bonus
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "BonusScore":
                    // Increase player score
                    GameManager.Instance.UpdateBonus(5);
                    break;
                case "BonusDistance":
                    // Pushes player's x position forward
                    if (GameManager.initPlayerPosition > GameObject.FindGameObjectWithTag("Player").transform.position.x)
                    {
                        float newPositionX = GameObject.FindGameObjectWithTag("Player").transform.position.x + 1.0f;
                        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(newPositionX, GameObject.FindGameObjectWithTag("Player").transform.position.y);
                    }
                    break;
                default:
                    Delete();
                    break;
            }

            Delete();
        }

        
        if (collision.tag == "PowerReset")
        {
            Delete();
        }
    }

    // Destroys and removes current PowerUp from play
    private void Delete()
    {
        MovablePowerUp.movingPowerUps.Remove(this);
        Destroy(this.gameObject);
    }
}
