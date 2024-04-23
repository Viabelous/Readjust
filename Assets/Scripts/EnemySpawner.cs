using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField]
    private GameObject enemyPref;
    [SerializeField]
    private SpawnHolder spawnHolder;

    // [SerializeField]
    public float minTime, maxTime, timer, gap, time;
    public bool spawnEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        time = StageManager.instance.time;
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {

        // if (time < 10)
        // {
        //     enemyPref = spawnHolder.enemyPrefs.Find(
        //         prefab => prefab.GetComponent<MobController>().enemy.enemyName == EnemyName.pinkBoogie
        //     );
        // }


        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        // if (time < 60 * 10 && time % 15 == 0 && minTime < maxTime)
        // {
        //     maxTime -= gap;
        // }

        // if (time < 60 * 10 && time % 60 == 0 && minTime < maxTime)
        // {
        //     minTime += gap;
        // }

        timer = Random.Range(minTime, maxTime);
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnHolder.enemyPrefs.Count);
        enemyPref = spawnHolder.enemyPrefs[index];

        Instantiate(enemyPref, transform.position, Quaternion.identity);
    }
}
