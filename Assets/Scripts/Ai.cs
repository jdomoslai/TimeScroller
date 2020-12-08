using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D characterRBody;
    // Start is called before the first frame update
    void Start()
    {
        //facingRight = true;
        characterRBody = GetComponent<Rigidbody2D>();
    }


}
