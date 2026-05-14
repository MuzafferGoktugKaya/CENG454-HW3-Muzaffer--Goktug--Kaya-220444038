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
    
    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 3; 
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float spawnDelay = 0.5f;

    private int currentWave = 0;
    public static event Action<int> OnWaveChanged;

    void Start()
    {
        spawnPoints = spawnPoints.Where(sp => sp != null).ToList();
        if (spawnPoints.Count > 0) StartCoroutine(SpawnWaveRoutine());
    }

    IEnumerator SpawnWaveRoutine()
    {
        while (currentWave < totalWaves) 
        {
            currentWave++;
            OnWaveChanged?.Invoke(currentWave);
            
            Debug.Log($"Wave {currentWave} started!");

            for (int i = 0; i < enemiesPerWave + (currentWave * 2); i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }

            
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        GameManager.Instance.LevelComplete(); 
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        BasicEnemy enemyScript = enemyObj.GetComponent<BasicEnemy>();
        CoreController core = UnityEngine.Object.FindFirstObjectByType<CoreController>();

        if (enemyScript != null && core != null)
        {
            IEnemy armoredEnemy = new ArmoredEnemyDecorator(enemyScript);
            
            if (Random.value > 0.5f) 
                enemyScript.SetMovementStrategy(new MoveToCoreStrategy());
            else 
                enemyScript.SetMovementStrategy(new ZigZagStrategy());
            
            armoredEnemy.Initialize(core.transform);
        }
    }
}