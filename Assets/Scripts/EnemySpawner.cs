using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField]
    public GameObject enemyPref;
    // [SerializeField]
    public float _minTime, _maxTime, _timer;


    // Start is called before the first frame update
    void Start()
    {
        _minTime = 2;
        _maxTime = 5;
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Instantiate(enemyPref, transform.position, Quaternion.identity);
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        _timer = Random.Range(_minTime, _maxTime);
    }
}
