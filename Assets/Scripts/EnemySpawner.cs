using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemyTypes = null;
    [SerializeField]
    private float spawnSeconds = 0;
    //[SerializeField]
    //private float deleteSeconds;

    private List<Enemy> enemies;
    private float spawnTime;
    //private float deleteTime;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        spawnTime = spawnSeconds + Time.time;
        //deleteTime = deleteSeconds + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ConstantMovement(enemies);
        Spawn(enemies, enemyTypes, ref spawnTime, spawnSeconds);
        //Delete(enemies, ref deleteTime, deleteSeconds);
    }

    //get the enemies moving to the left
    private void ConstantMovement(List<Enemy> enemies)
    {
        if(enemies.Count > 0)
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy)
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

    //returns a random position for enemies to spawn
    //only effects flying enemies
    private float RandomPosition()
    {
        return Random.Range(-2.5f, 1.0f);
    }

    //REVISION: NICOLAS PREZIO
    //I have removed this function as a result of a bug in the timers
    //Instead I opt to use OnBecameInvisible() in the enemy script
    //I feel that it is much better in performance and much less likely to bug out
    //OnBecameInvisible() also works much better with Enemies Die() function
    //deletes the enemies after they leave the screen
    //private void Delete(List<Enemy> enemyList, ref float deleteTime, float deleteSeconds)
    //{
    //    //Debug.Log("Time: " + Time.time);
    //    //Debug.Log("Delete Time: " + Time.time);

    //    if (Time.time > deleteTime)
    //    {
    //        Enemy enemyToRemove = enemyList.FirstOrDefault();
    //        Debug.Log("Enemy time out: " + enemyToRemove);

    //        if(enemyToRemove)
    //        {
    //            Debug.Log("Removing: " + enemyToRemove);
    //            enemyList.Remove(enemyToRemove);
    //            Destroy(GameObject.Find(enemyToRemove.name));

    //            deleteTime += deleteSeconds;
    //        }
    //        else
    //        {
    //            Debug.Log("Something went wrong");
    //        }
    //    }
    //}
}
