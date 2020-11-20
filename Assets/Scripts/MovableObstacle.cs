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
    private float deleteSecondsPlatforms;

    [SerializeField]
    private float spawnSecondsObstacles;

    [SerializeField]
    private float deleteSecondsObstacles;

    private List<Obstacle> movingObstacles;

    private List<Obstacle> movingPlatforms;

    private float spawnTimePlatforms;

    private float deleteTimePlatforms;

    private float spawnTimeObstacles;

    private float deleteTimeObstacles;

    // Start is called before the first frame update
    void Start()
    {
        movingObstacles = new List<Obstacle>();

        movingPlatforms = new List<Obstacle>();

        spawnTimePlatforms = Time.time + spawnSecondsPlatforms;

        deleteTimePlatforms = Time.time + deleteSecondsPlatforms;

        spawnTimeObstacles = Time.time + spawnSecondsObstacles;

        deleteTimeObstacles = Time.time + deleteSecondsObstacles;
    }

    // Update is called once per frame
    void Update()
    {
        ConstantMovement(movingObstacles);

        ConstantMovement(movingPlatforms);

        Spawn(movingPlatforms, platforms, ref spawnTimePlatforms, spawnSecondsPlatforms);

        Delete(movingPlatforms, ref deleteTimePlatforms, deleteSecondsPlatforms);

        Spawn(movingObstacles, obstacles, ref spawnTimeObstacles, spawnSecondsObstacles);

        Delete(movingObstacles, ref deleteTimeObstacles, deleteSecondsObstacles);
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

    // Deletes moving Obstacles after a certain time in seconds
    private void Delete(List<Obstacle> obstacleList, ref float deleteTime, float deleteSeconds)
    {
        if (Time.time > deleteTime)
        {
            Obstacle platformRemove = obstacleList.FirstOrDefault();

            if (platformRemove)
            {
                obstacleList.Remove(platformRemove);
                Destroy(GameObject.Find(platformRemove.name));

                deleteTime += deleteSeconds;
            }
        }
    }

    // Returns a random position within the camera's Y range
    private float RandomPosition()
    {
        return Random.Range(-3.0f, 3.0f);
    }
}
