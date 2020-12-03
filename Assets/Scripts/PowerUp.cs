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
            GameManager.Instance.UpdateBonus(5);
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
