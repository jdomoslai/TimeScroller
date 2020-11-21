using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @author: Nicolas Prezio
 * This class checks to see if the character is on the ground
 * It is used for the jump mechanic
 */

public class Grounded : MonoBehaviour
{
    //private variables
    private GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        //this script is a child of the player game object
        //so this gets the player
        sprite = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //has the character landed?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (sprite.name == "Nameless")
                sprite.GetComponent<CharacterMovement>().isGrounded = true;
            if (sprite.name == "Frog")
                sprite.GetComponent<Frog>().isGrounded = true;
        }
    }

    //has the character left the ground?
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (sprite.name == "Nameless")
                sprite.GetComponent<CharacterMovement>().isGrounded = false;
            if (sprite.name == "Frog")
                sprite.GetComponent<Frog>().isGrounded = false;
        }
    }
}
