using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] obstacles;

    [SerializeField]
    private Obstacle[] platforms;

    private List<Obstacle> movingObstacles;

    private Obstacle currentObstacle = null;

    // Start is called before the first frame update
    void Start()
    {
        movingObstacles = new List<Obstacle>();
        currentObstacle = Instantiate(platforms[3], new Vector3(20, 0.4f, 0), Quaternion.identity);
        movingObstacles.Add(currentObstacle);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingObstacles.Count > 0)
        {
            foreach (Obstacle mo in movingObstacles)
            {
                mo.Move();
            }
        }
    }
}
