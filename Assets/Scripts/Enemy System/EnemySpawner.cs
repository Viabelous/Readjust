using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnHolder spawnHolder;

    private GameObject enemyPref;

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
        int index = Random.Range(0, spawnHolder.enemyPrefs.Count);
        enemyPref = spawnHolder.enemyPrefs[index];

        Instantiate(enemyPref, transform.position, Quaternion.identity);
    }
}
