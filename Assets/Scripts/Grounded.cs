using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    //editor variables
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //this script is a child of the player game object
        //so this gets the player
        player = gameObject.transform.parent.gameObject;
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
            player.GetComponent<CharacterMovement>().isGrounded = true;
        }
    }

    //has the character left the ground?
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            player.GetComponent<CharacterMovement>().isGrounded = false;
        }
    }
}
