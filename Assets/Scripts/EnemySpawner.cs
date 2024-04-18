using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField]
    public GameObject enemyPref;
    // [SerializeField]
    public float minTime, maxTime, timer, gap;
    public bool spawnEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(enemyPref, transform.position, Quaternion.identity);
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        float time = StageManager.Instance.time;
        // tiap 15 detik
        if (time < 60 * 10 && time % 15 == 0 && minTime < maxTime)
        {
            maxTime -= gap;
        }

        if (time < 60 * 10 && time % 60 == 0 && minTime < maxTime)
        {
            minTime += gap;
        }

        timer = Random.Range(minTime, maxTime);
    }
}
