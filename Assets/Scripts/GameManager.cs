using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundElement[] backgroundElements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.Move();
        }
    }
}
