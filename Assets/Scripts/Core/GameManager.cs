using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] int enemiesAlive;
    [SerializeField] GameObject[] spawnPoints;
    GameObject enemyPrefab;
    public GameObject[] enemyPrefabs;
    [SerializeField] private Transform parent;
    private List<IEffect> effects;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //SpawnEnemies();
        }

        if (Keyboard.current.tKey.isPressed)
        {
            enemyPrefab = GetRandomEnemy();
            GameObject enemySpawned = Instantiate(enemyPrefab,
                new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)), Quaternion.identity, parent) as GameObject;
            enemySpawned.GetComponent<EnemyAIController>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }

    private GameObject GetRandomEnemy()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }

    private void SpawnEnemies()
    {
        for (var x = 0; x < spawnPoints.Length; x++)
        {
            enemyPrefab = GetRandomEnemy();
            GameObject spawnPoint = spawnPoints[x];
            GameObject enemySpawned =
                Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity, parent) as GameObject;
            enemySpawned.GetComponent<EnemyAIController>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }
}