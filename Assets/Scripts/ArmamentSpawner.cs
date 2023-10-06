using System.Collections;
using UnityEngine;

public class ArmamentSpawner : SpawnManager
{
    // adjustable variable
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        SpawnBullets();
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    #region Private Methods

    private void SpawnAmmo()
    {
        Bullet ammo = GetAmmo();
        if (ammo == null) return;

        // set position
        ammo.transform.position = spawnPoint.position;

        // set tra

        // shoot
        ammo.Spawn();
        ammo.Shoot(SetTrajectory());
    }

    private Vector3 SetTrajectory()
    {
        return spawnPoint.position - Player.position;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            SpawnAmmo();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    #endregion
}
