using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{

    [Header("Spawn Settings")]
    public GameObject playerPrefab;
    public int numberOfPlayers;
    public Vector3 spawnArea;

    //Spawned
    private List<GameObject> spawnedPlayers;

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        spawnedPlayers = new List<GameObject>(numberOfPlayers);

        for(int i = 0; i < numberOfPlayers; i++)
        {

            GameObject spawnedPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
            
            spawnedPlayers.Insert(i, spawnedPlayer);

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            spawnedPlayer.transform.position = randomSpawnPosition;

            Quaternion randomSpawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            spawnedPlayer.transform.rotation = randomSpawnRotation;

        }
    }

}
