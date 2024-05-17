using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnHolder spawnHolder;
    [SerializeField] private float minTime, maxTime;
    [SerializeField] private int amount;
    [SerializeField] private bool spawnY = true;

    private Dictionary<GameObject, float> enemies = new Dictionary<GameObject, float>();
    private float timer, time;

    private float totalTime = 600f, elapsedTime = 0;

    private ItemSystem itemSystem;

    // Start is called before the first frame update
    void Start()
    {
        itemSystem = GameObject.Find("Player").GetComponent<ItemSystem>();
        if (itemSystem.CheckItem("Void Embodiment"))
        {
            amount *= 2;
        }

        time = StageManager.instance.time;

        enemies.Add(spawnHolder.enemyPrefs[0], 1);
        enemies.Add(spawnHolder.enemyPrefs[1], 0);
        enemies.Add(spawnHolder.enemyPrefs[2], 0);
        enemies.Add(spawnHolder.enemyPrefs[3], 0);
        enemies.Add(spawnHolder.enemyPrefs[4], 0);

        ResetTimer();
    }

    void Update()
    {
        time = StageManager.instance.time;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 60 && time <= totalTime)
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
        timer = Random.Range(time < totalTime ? minTime : minTime + 5, time < totalTime ? maxTime : maxTime + 10);
    }

    void SpawnEnemy()
    {
        // int index = Random.Range(0, spawnHolder.enemyPrefs.Count);
        // GameObject enemyPref = spawnHolder.enemyPrefs[index];

        for (int i = 0; i < amount; i++)
        {
            float x = spawnY ? Random.Range(StageManager.instance.minMap.x, StageManager.instance.maxMap.x) : transform.position.x;
            float y = spawnY ? transform.position.y : Random.Range(StageManager.instance.minMap.y, StageManager.instance.maxMap.y);
            Vector3 randomPos = new Vector3(x, y, 0);

            GameObject enemyPref = GetEnemy();
            Instantiate(enemyPref, randomPos, Quaternion.identity);
        }

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
        }
        return enemySpawn;
    }

    void UpdateProbablities()
    {

        int minNow = (int)time / 60;

        switch (minNow)
        {
            case 0:
                enemies[spawnHolder.enemyPrefs[0]] = 1f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 1:
                enemies[spawnHolder.enemyPrefs[0]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[1]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 2:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 1f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 3:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[2]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 4:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 1f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 5:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[3]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 6:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 1f;
                enemies[spawnHolder.enemyPrefs[4]] = 0f;
                break;
            case 7:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 0.5f;
                enemies[spawnHolder.enemyPrefs[4]] = 0.5f;
                break;
            case 8:
                enemies[spawnHolder.enemyPrefs[0]] = 0f;
                enemies[spawnHolder.enemyPrefs[1]] = 0f;
                enemies[spawnHolder.enemyPrefs[2]] = 0f;
                enemies[spawnHolder.enemyPrefs[3]] = 0f;
                enemies[spawnHolder.enemyPrefs[4]] = 1f;
                break;
            case 9:
                enemies[spawnHolder.enemyPrefs[0]] = 0.1f;
                enemies[spawnHolder.enemyPrefs[1]] = 0.1f;
                enemies[spawnHolder.enemyPrefs[2]] = 0.1f;
                enemies[spawnHolder.enemyPrefs[3]] = 0.2f;
                enemies[spawnHolder.enemyPrefs[4]] = 0.5f;
                break;
            case 10:
                enemies[spawnHolder.enemyPrefs[0]] = 0.1f;
                enemies[spawnHolder.enemyPrefs[1]] = 0.1f;
                enemies[spawnHolder.enemyPrefs[2]] = 0.2f;
                enemies[spawnHolder.enemyPrefs[3]] = 0.2f;
                enemies[spawnHolder.enemyPrefs[4]] = 0.4f;
                break;
        }

        // int j = 1;
        // for (int i = 0; i < enemies.Count; i++)
        // {
        //     if (minNow >= 10)
        //     {
        //         enemies[enemies.ElementAt(i).Key] = 0.2f;
        //         continue;
        //     }

        //     if (minNow == j)
        //     {
        //         enemies[enemies.ElementAt(i).Key] = 1f;
        //     }

        //     if (minNow - 1 == j)
        //     {
        //         enemies[enemies.ElementAt(i).Key] -= 0.5f;
        //     }
        //     else if (minNow + 1 == j)
        //     {
        //         enemies[enemies.ElementAt(i).Key] += 0.5f;
        //     }

        //     if (minNow == j + 2)
        //     {
        //         enemies[enemies.ElementAt(i).Key] -= 0.5f;
        //     }
        //     j += 2;
        // }

    }
}
