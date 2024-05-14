using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies; // Daftar musuh-musuh yang akan muncul
    public float initialProbability = 1f; // Probabilitas awal musuh
    public float probabilityDecayRate = 0.5f; // Tingkat penurunan probabilitas per menit
    public float spawnInterval = 10; // Interval waktu antara kemunculan musuh (dalam detik)
    public float totalTime = 600f; // Total waktu dalam detik (10 menit)

    private float elapsedTime = 0f; // Waktu yang telah berlalu
    private float nextSpawnTime = 0f; // Waktu kemunculan musuh selanjutnya

    void Start()
    {
        CalculateNextSpawnTime();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Hitung probabilitas baru setiap menit
        if (elapsedTime >= 60f)
        {
            initialProbability *= probabilityDecayRate;
            CalculateNextSpawnTime();
            elapsedTime = 0f;
        }

        // Spawn musuh jika sudah waktunya
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            CalculateNextSpawnTime();
        }
    }

    void CalculateNextSpawnTime()
    {
        nextSpawnTime = Time.time + spawnInterval; // Tentukan waktu kemunculan musuh selanjutnya
    }

    void SpawnEnemy()
    {
        // Spawn musuh sesuai dengan probabilitas awal yang telah dihitung
        if (initialProbability > 0f)
        {
            int randomIndex = Random.Range(0, enemies.Count);
            Instantiate(enemies[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
