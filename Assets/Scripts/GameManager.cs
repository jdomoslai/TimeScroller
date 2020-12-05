using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundElement[] backgroundElements = null; // Ground

    [SerializeField]
    public static List<MovableElement> movableElements = null; // Background objects

    // Start is called before the first frame update
    void Start()
    {
        movableElements = new List<MovableElement>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            element.Move();
        }

        if (movableElements.Count > 0)
        {
            foreach (MovableElement element in movableElements)
            {
                element.Move();
            }
        }
    }
}
