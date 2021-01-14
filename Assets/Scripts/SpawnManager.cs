using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos;
    private float startDelay = 2f, repeatRate = 2f;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.position;
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
    }


    void spawnObstacle()
    {
        if (!_playerController.GameOver)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
