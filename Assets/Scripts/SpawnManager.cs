using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables

    // adjustable variables
    [Header("Prefab")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Bullet bullet;
    [Space]
    [Header("Spawn Settings")]
    [SerializeField] protected int spawnCount;
    [SerializeField] protected float spawnDelay;

    // private variables
    private Enemy[] enemiesSpawned;
    private Bullet[] bulletSpawned;

    #endregion

    #region Shared Methods

    protected void SpawnBullets()
    {
        bulletSpawned = new Bullet[spawnCount];
        for (int spawned = 0; spawned < spawnCount; spawned++)
        {
            bulletSpawned[spawned] = Instantiate(bullet, this.transform);
            bulletSpawned[spawned].Despawn();
        }
    }

    protected void SpawnEnemies()
    {
        enemiesSpawned = new Enemy[spawnCount];
        for (int spawned = 0; spawned < spawnCount; spawned++)
        {
            enemiesSpawned[spawned] = Instantiate(enemy, this.transform);
            enemiesSpawned[spawned].Despawn();
        }
    }

    protected Enemy GetClone()
    {
        for (int current = 0; current < enemiesSpawned.Length; current++)
        {
            if (!enemiesSpawned[current].isActive) return enemiesSpawned[current];
        }
        return null;
    }

    protected Bullet GetAmmo()
    {
        for (int current = 0; current < bulletSpawned.Length; current++)
        {
            if (!bulletSpawned[current].isActive) return bulletSpawned[current];
        }
        return null;
    }

    #endregion
}
