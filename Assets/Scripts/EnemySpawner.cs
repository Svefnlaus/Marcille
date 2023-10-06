using System.Collections;
using UnityEngine;

public class EnemySpawner : SpawnManager
{
    #region Variables

    // adjustable variable
    [SerializeField] protected Transform[] spawnPoint;

    // shared variables
    protected int spot;
    protected int previousSpot;

    #endregion

    private void Awake()
    {
        SpawnEnemies();
    }

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private void SpawnEnemy()
    {
        Enemy clone = GetClone();
        if (clone == null) return;

        do { spot = Random.Range(0, spawnPoint.Length); }
        while (spot == previousSpot);
        clone.transform.position = spawnPoint[spot].position;
        previousSpot = spot;

        clone.Spawn();
        clone.SetColor();

        if (ScoreManager.score % 10 != 0 || ScoreManager.score == 0) return;
        clone.SpecialOne();
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnDelay);
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
