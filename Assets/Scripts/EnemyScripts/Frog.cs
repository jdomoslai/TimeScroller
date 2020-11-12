using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    //editor variables
    public float jumpHeight;
    public bool isGrounded;
    public float jumpDelay;
    public Animator animator;

    //private variables
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        isGrounded = false;
        StartCoroutine(JumpLogic(jumpDelay));
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", !isGrounded);
    }

    //the jumping logic
    IEnumerator JumpLogic(float jumpDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpDelay);
            myRigidbody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }
}
