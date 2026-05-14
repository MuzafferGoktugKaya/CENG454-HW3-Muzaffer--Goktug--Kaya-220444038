using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 0;
    
    public static event Action<int> OnWaveChanged;

    void Start()
    {
        spawnPoints = spawnPoints.Where(sp => sp != null).ToList();
        
        if (spawnPoints.Count > 0)
        {
            StartCoroutine(SpawnWaveRoutine());
        }
        else
        {
            Debug.LogError("WaveManager: Spawn noktanız yok!");
        }
    }

    IEnumerator SpawnWaveRoutine()
    {
        while (true)
        {
            currentWave++;
            // for UI update
            OnWaveChanged?.Invoke(currentWave);
            
            Debug.Log($"Wave {currentWave} Starting!");

            for (int i = 0; i < enemiesPerWave + currentWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
        BasicEnemy enemyScript = enemy.GetComponent<BasicEnemy>();
        
        CoreController core = UnityEngine.Object.FindFirstObjectByType<CoreController>();

        if (enemyScript != null && core != null)
        {
            if (UnityEngine.Random.value > 0.5f)
                enemyScript.SetMovementStrategy(new MoveToCoreStrategy());
            else
                enemyScript.SetMovementStrategy(new ZigZagStrategy());

            enemyScript.Initialize(core.transform);
        }
    }
}