using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject cloudEnemyPrefab;
    public GameObject starEnemyPrefab;
    public float cloudSpawnChance = 0.2f;
    public float starSpawnChance = 0.1f;  // �� �� ���� Ȯ�� (10%)

    [SerializeField] int MaxspawnMonsterCount;
    [SerializeField] int spawnMonsterCount;

    [SerializeField] GameObject ClearPanel;

    [SerializeField] float spawntime;

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawntime);

            if (spawnMonsterCount < MaxspawnMonsterCount)
            {
                SpawnEnemy();
            }
            else
            {
                isSpawning = false;
                StartCoroutine(ShowClearPanel());
            }
        }
    }

    IEnumerator ShowClearPanel()
    {
        yield return new WaitForSeconds(3f);
        ClearPanel.SetActive(true);

        StartCoroutine(ScoreManager.instance.ScoreAnimation());
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        float randomValue = Random.value;
        GameObject enemyToSpawn;

        if (randomValue < cloudSpawnChance)
        {
            enemyToSpawn = cloudEnemyPrefab;
        }
        else if (randomValue < cloudSpawnChance + starSpawnChance)
        {
            enemyToSpawn = starEnemyPrefab;
        }
        else
        {
            enemyToSpawn = enemyPrefab;
        }

        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        spawnMonsterCount++;

        if (enemyToSpawn == cloudEnemyPrefab)
        {
            // Ŭ���� �� �߰� ó��
        }
        else if (enemyToSpawn == starEnemyPrefab)
        {
            // �� �� �߰� ó��
        }
    }
}
