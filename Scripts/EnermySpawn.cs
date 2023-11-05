using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnermySpawn : MonoBehaviour
{
    [Header("References")]
    public GameObject[] enermyPrefab;

    [Header("Attributes")]
    public int baseEnermyCount = 10;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public float scalingFactor = 0.75f;
    public float epsc = 15f;
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private int currentEnermyCount = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool spawnEnemies = false;
    private float eps;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroy);
    }

    private void Start()
    {
       StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!spawnEnemies) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            timeSinceLastSpawn = 0f;
            enemiesLeftToSpawn--;
            enemiesAlive++;
            spawningEnemies();
        }
        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0)
        {
            EndWave();
        }
    }
    private void EnemyDestroy()
    {
        enemiesAlive--;

    }
    private void spawningEnemies()
    {
        int index = Random.Range(0, enermyPrefab.Length);
        GameObject enemiesToSpawn = enermyPrefab[index];
        Instantiate(enemiesToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        enemiesLeftToSpawn = baseEnermyCount;
        spawnEnemies = true;
        eps = EnemiesPerSecond();
    }
    private void EndWave()
    {
        spawnEnemies = false;
        timeSinceLastSpawn = 0f; 
        currentEnermyCount++;
        StartCoroutine(StartWave());
    }
    private int enemiesPerSpawn()
    {
        //make game more harder per wave (increase number of enermy per wave)
        return Mathf.RoundToInt(baseEnermyCount * Mathf.Pow(currentEnermyCount, scalingFactor));
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentEnermyCount, scalingFactor), 0, epsc);
    }
}
