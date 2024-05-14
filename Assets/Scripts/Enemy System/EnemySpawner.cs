using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnHolder spawnHolder;

    private Dictionary<GameObject, float> enemies = new Dictionary<GameObject, float>();

    // [SerializeField]
    public float minTime, maxTime, timer, gap, time;
    private float totalTime = 600f, elapsedTime = 0;
    public bool spawnEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        time = StageManager.instance.time;

        enemies.Add(spawnHolder.enemyPrefs[0], 1);
        enemies.Add(spawnHolder.enemyPrefs[1], 0);
        enemies.Add(spawnHolder.enemyPrefs[2], 0);
        enemies.Add(spawnHolder.enemyPrefs[3], 0);
        enemies.Add(spawnHolder.enemyPrefs[4], 0);

        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 60 && time <= 600)
        {
            UpdateProbablities();
            elapsedTime = 0;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }

    void SpawnEnemy()
    {
        // int index = Random.Range(0, spawnHolder.enemyPrefs.Count);
        // GameObject enemyPref = spawnHolder.enemyPrefs[index];
        GameObject enemyPref = GetEnemy();

        Instantiate(enemyPref, transform.position, Quaternion.identity);
    }

    GameObject GetEnemy()
    {
        GameObject enemySpawn = null;
        while (enemySpawn == null)
        {

            float totalProbability = 0f;

            // Hitung total probabilitas
            foreach (var enemy in enemies)
            {
                totalProbability += enemy.Value;
            }

            // Generate a random number between 0 and totalProbability
            float randomPoint = Random.value * totalProbability;

            // Pilih objek berdasarkan probabilitas
            foreach (var enemy in enemies)
            {
                if (randomPoint < enemy.Value)
                {
                    enemySpawn = enemy.Key;
                }
                else
                {
                    randomPoint -= enemy.Value;
                }
            }

            // float randomValue = Random.value; // Nilai acak antara 0 dan 1
            // float cumulativeProbability = 0f;

            // foreach (var enemy in enemies)
            // {
            //     cumulativeProbability += enemy.Value;
            //     if (randomValue <= cumulativeProbability)
            //     {
            //         enemySpawn = enemy.Key;
            //     }
            // }
        }
        return enemySpawn;


        // for (int i = 0; i < probabilities.Count; i++)
        // {
        //     cumulativeProbability += probabilities[i];
        //     if (randomValue <= cumulativeProbability)
        //     {
        //         Instantiate(enemies[i], transform.position, Quaternion.identity); // Instantiate musuh yang sesuai
        //         break;
        //     }
        // }
    }

    void UpdateProbablities()
    {

        int minNow = (int)time / 60;

        int j = 0;
        for (int i = 1; i < enemies.Count; i += 2)
        {
            if (minNow >= 10)
            {
                enemies[enemies.ElementAt(i).Key] = 0.2f;
                continue;
            }

            if (minNow == j)
            {
                enemies[enemies.ElementAt(i).Key] = 1f;
            }

            if (minNow - 1 == j)
            {
                enemies[enemies.ElementAt(i).Key] -= 0.5f;
            }
            else if (minNow + 1 == j)
            {
                enemies[enemies.ElementAt(i).Key] += 0.5f;
            }

            if (minNow == j + 2)
            {
                enemies[enemies.ElementAt(i).Key] -= 0.5f;
            }
            j += 2;
        }


        // foreach (var enemy in enemies)
        // {

        //     i += 2;
        // }
    }
}
