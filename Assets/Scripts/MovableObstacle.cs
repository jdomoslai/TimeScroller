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

    [SerializeField]
    private float spawnSecondsPlatforms;

    [SerializeField]
    private float spawnSecondsObstacles;

    public static List<Obstacle> movingObstacles;

    public static List<Obstacle> movingPlatforms;

    private float spawnTimePlatforms;

    private float spawnTimeObstacles;

    // Start is called before the first frame update
    void Start()
    {
        movingObstacles = new List<Obstacle>();

        movingPlatforms = new List<Obstacle>();

        spawnTimePlatforms = Time.time + spawnSecondsPlatforms;

        spawnTimeObstacles = Time.time + spawnSecondsObstacles;
    }

    // Update is called once per frame
    void Update()
    {
        ConstantMovement(movingObstacles);

        ConstantMovement(movingPlatforms);

        Spawn(movingPlatforms, platforms, ref spawnTimePlatforms, spawnSecondsPlatforms);

        Spawn(movingObstacles, obstacles, ref spawnTimeObstacles, spawnSecondsObstacles);
    }

    // Moves Obstacles if they exist
    private void ConstantMovement(List<Obstacle> obstacles)
    {
        if (obstacles.Count > 0)
        {
            foreach (Obstacle o in obstacles)
            {
                o.Move();
            }
        }
    }

    // Spawns random moving Obstacles after a certain time in seconds
    private void Spawn(List<Obstacle> obstacleList, Obstacle[] obstacles, ref float spawnTime, float spawnSeconds)
    {
        if (Time.time > spawnTime)
        {
            int rand = Random.Range(0, obstacles.Length);
            Obstacle obstacle = obstacles[rand];

            obstacle = Instantiate(obstacle, new Vector3(20, RandomPosition(), 0), Quaternion.identity);
            obstacleList.Add(obstacle);

            spawnTime += spawnSeconds;
        }
    }

    // Returns a random position within the camera's Y range
    private float RandomPosition()
    {
        return Random.Range(-3.0f, 3.0f);
    }
}
