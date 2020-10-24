using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * Sprint 1: Got the basic character movement and ground collision working
 * Most of the animation work is done
 * character can only jump once
 * SPRINT 2: ToDo: Continue fleshing out character movement
 * fix crouching
 * add double jumping and dodging
 */

public class CharacterMovement : MonoBehaviour
{
    //editor variables
    public float movementSpeed;
    public float jumpHeight;
    public Animator animator;
    public bool isGrounded = false;

    //private variables
    //private bool facingRight;
    private bool isCrouched;
    private Rigidbody2D characterRBody;
    //private float horizontalPos = 0.0f; 

    // Start is called before the first frame update
    private void Start()
    {
        //facingRight = true;
        //isCrouched = false;
        characterRBody = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at regular intervals
    private void FixedUpdate()
    {
        //horizontalPos = Input.GetAxis("Horizontal");
        //animator.SetFloat("Horizontal", horizontalPos);
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
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterRBody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

        //if they are not grounded we should be playing jump anim
        if (!isGrounded)
            animator.Play("Jump");
        else if (isGrounded && !isCrouched) // this probably can't stay because of other cases.
            animator.Play("Idle");

        //crouch
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.C))
        {
            if(isCrouched)
            {
                Debug.Log("no longer crouching");
                animator.Play("Idle");
                isCrouched = !isCrouched;
            }
            else
            {
                animator.Play("crouch");
                isCrouched = true;
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
