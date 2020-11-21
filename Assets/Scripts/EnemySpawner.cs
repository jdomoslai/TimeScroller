using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemyTypes;
    [SerializeField]
    private float spawnSeconds;
    [SerializeField]
    private float deleteSeconds;

    private List<Enemy> enemies;
    private float spawnTime;
    private float deleteTime;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        spawnTime = spawnSeconds + Time.time;
        deleteTime = deleteSeconds + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ConstantMovement(enemies);
        Spawn(enemies, enemyTypes, ref spawnTime, spawnSeconds);
        Delete(enemies, ref deleteTime, deleteSeconds);
    }

    //get the enemies moving to the left
    private void ConstantMovement(List<Enemy> enemies)
    {
        if(enemies.Count > 0)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Move();
            }
        }
    }

    //spawns the enemy sprites randomly
    private void Spawn(List<Enemy> enemyList, Enemy[] enemies, ref float spawnTime, float spawnSeconds)
    {
        if(Time.time > spawnTime)
        {
            int randIndex = Random.Range(0, enemies.Length);
            Enemy enemy = enemies[randIndex];

            enemy = Instantiate(enemy, new Vector3(20, RandomPosition(), 0), Quaternion.identity);
            enemyList.Add(enemy);

            spawnTime += spawnSeconds;
        }
    }

    //deletes the enemies after they leave the screen
    private void Delete(List<Enemy> enemyList, ref float deleteTime, float deleteSeconds)
    {
        if(Time.time > deleteTime)
        {
            Enemy enemyToRemove = enemyList.FirstOrDefault();

            if(enemyToRemove)
            {
                enemyList.Remove(enemyToRemove);
                Destroy(GameObject.Find(enemyToRemove.name));

                deleteTime += deleteSeconds;
            }
        }
    }

    //returns a random position for enemies to spawn
    //only effects flying enemies
    private float RandomPosition()
    {
        return Random.Range(-2.5f, 1.0f);
    }
}
