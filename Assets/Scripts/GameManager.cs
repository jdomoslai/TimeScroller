using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundElement[] backgroundElements; // Ground

    [SerializeField]
    public static List<MovableElement> movableElements; // Background objects

    //for the source
    private Text distanceText;
    private float f_dis = 0;
    private int dis = 0;

    // Start is called before the first frame update
    void Start()
    {
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        movableElements = new List<MovableElement>();
    }

    private void UpdateDistance()
    {
        f_dis += 1 * Time.deltaTime;
        dis = (int)f_dis;

        distanceText.text = "Score:" +  dis.ToString();
    }

    //this is for upadte the times( i need it to put player controller)
    public void UpdateBonus(int count)
    {
        dis += count;
        distanceText.text = "Score:" + dis.ToString();

    }

    //this is for the algorithm for timed release of power ups
    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.gameObject.tag == "Bonus1")
    //    {
    //        GameManager.Instance.UpdateBonus(5);
    //        //gameManager.UpdateBonus(5);
    //        Destroy(coll.gameObject);
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
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

        // Exit to main menu
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
