using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField] GameObject _laserPrefab;

    public Transform[] laserSpawnPosition;

    public void SpawnLaser(float speed)
    {
        float laserPos = Random.Range(0f, laserSpawnPosition.Length);
        Transform laserPosition = laserSpawnPosition[(int)laserPos];

        Vector3 rotation = laserPosition.rotation.eulerAngles;
        Instantiate(_laserPrefab, laserPosition.position, Quaternion.Euler(rotation));

        LaserBehaviour laserBehaviour = _laserPrefab.GetComponent<LaserBehaviour>();
        laserBehaviour.SetLaserSpeed(speed);

    }



}