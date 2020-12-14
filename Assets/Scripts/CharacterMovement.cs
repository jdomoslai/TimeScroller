using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/*
 * @author: Nicolas Prezio
 * Sprint 1: Got the basic character movement and ground collision working
 * Most of the animation work is done
 * character can only jump once
 * SPRINT 2: Continued fleshing out character movement
 * removed crouching and replaced it with a "dodge" mechanic
 * added double jumping
 * all animation is working as intended
 */

public class CharacterMovement : MonoBehaviour
{
    //editor variables
    public float movementSpeed;
    public float jumpHeight;
    public int jumpCount;
    public Animator animator = null;
    public bool isGrounded = false;

    //private variables
    private bool isDodging;
    private Rigidbody2D characterRBody;
    private int jumps;
    //private float horizontalPos = 0.0f; 

    //private HighScores highScores = new HighScores { highScoreEntries = new List<HighScoreEntry>() };
    public static bool death;

    // Start is called before the first frame update
    private void Start()
    {
        //facingRight = true;
        characterRBody = GetComponent<Rigidbody2D>();
        jumps = 1;

        death = false;

        //highScores = highScores.Load();
    }

    // Update is called once per frame
    private void Update()
    {
        //basic character movements
        //horizontal
        //REVISION
        //I have removed horizontal movement because it does not fit into the game
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        //transform.position += (movement * movementSpeed) * Time.smoothDeltaTime;
        //Flip(horizontalPos);

        //jump
        animator.SetBool("isJumping", !isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && jumps != jumpCount && !isDodging)
        {
            jumps++;
            characterRBody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        if(isGrounded)
        {
            jumps = 1;
        }

        //dodge
        animator.SetBool("isDodging", isDodging);
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.C))
        {
            isDodging = true;
            StartCoroutine(waitForDodge());

            //Still to do once we figure out some other stuff first:
            //when the character is dodging they should be invincable for the duration
        }
    }

    /*
     * This method will wait for a set amount of time
     * Used for animating dodge
     */
    IEnumerator waitForDodge() { yield return new WaitForSeconds(1); isDodging = false; }

    /*
     * This method is used to detect when the player collides with something
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();

        //if there is an enemy that means we hit one
        if (enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    enemy.Die();
                    characterRBody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
                }
                else
                {
                    death = true;
                    
                    //player dies, restart the scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

    /*
    * This method will simply check to see which direction the
    * player is running and flip the sprite so the character
    * is facing the right way
    */
    //private void Flip(float horizontal)
    //{
    //    if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
    //    {
    //        facingRight = !facingRight;
    //        Vector3 characterScale = transform.localScale;
    //        characterScale.x *= -1;
    //        transform.localScale = characterScale;
    //    }
    //}

}
