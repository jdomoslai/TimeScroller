using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePowerUp : MonoBehaviour
{
    [SerializeField]
    private PowerUp[] powerUps;

    [SerializeField]
    private float spawnSecondsPowerUps;

    public static List<PowerUp> movingPowerUps;

    private float spawnTimePowerUps;

    // Start is called before the first frame update
    void Start()
    {
        movingPowerUps = new List<PowerUp>();
        spawnTimePowerUps = Time.time + spawnSecondsPowerUps;
    }

    // Update is called once per frame
    void Update()
    {
        ConstantMovement(movingPowerUps);
        Spawn(movingPowerUps, powerUps, ref spawnTimePowerUps, spawnSecondsPowerUps);
    }

    // Moves PowerUps if they exist
    private void ConstantMovement(List<PowerUp> powerUps)
    {
        if (powerUps.Count > 0)
        {
            foreach (PowerUp pu in powerUps)
            {
                pu.Move();
            }
        }
    }

    // Spawns random moving PowerUps after a certain time in seconds
    private void Spawn(List<PowerUp> powerUpList, PowerUp[] powerUps, ref float spawnTime, float spawnSeconds)
    {
        if (Time.time > spawnTime)
        {
            int rand = Random.Range(0, powerUps.Length);
            PowerUp po = powerUps[rand];

            po = Instantiate(po, new Vector3(20, RandomPosition(), 0), Quaternion.identity);
            powerUpList.Add(po);

            spawnTime += spawnSeconds;
        }
    }

    // Returns a random position within the camera's Y range
    private float RandomPosition()
    {
        return Random.Range(-3.0f, 3.0f);
    }
}
