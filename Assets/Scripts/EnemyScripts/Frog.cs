using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    //editor variables
    public float jumpHeight;
    public bool isGrounded;
    public float jumpDelay;
    public Animator animator = null;

    //private variables
    private Rigidbody2D myRigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        isGrounded = false;
        StartCoroutine(JumpLogic(jumpDelay));

        // Start enemy speed faster at higher scores
        speed = GameManager.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", !isGrounded);

        // If score is high enough increase enemy speed
        if (GameManager.dis > GameManager.enemyThresholdCount)
        {
            GameManager.enemyThresholdCount += GameManager.enemyThreshold;
            GameManager.enemySpeed += GameManager.enemyIncrease;
        }
    }

    //the jumping logic
    IEnumerator JumpLogic(float jumpDelay)
    {
        while (this != null)
        {
            yield return new WaitForSeconds(jumpDelay);
            myRigidbody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }
}
