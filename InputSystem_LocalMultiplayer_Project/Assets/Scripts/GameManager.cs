using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("Spawn Player Settings")]
    public bool spawnMultiplePlayers = false;
    public GameObject playerPrefab;
    public int numberOfPlayers;
    public Vector3 spawnArea;

    //Spawned Players
    private List<PlayerController> spawnedPlayerControllers;

    [Header("UI")]
    public GameObject pauseMenu;

    void Start()
    {
        
        TogglePauseMenu(false);

        if(spawnMultiplePlayers)
        {
            SpawnPlayers();
        }
    }

    void SpawnPlayers()
    {
        spawnedPlayerControllers = new List<PlayerController>(numberOfPlayers);

        for(int i = 0; i < numberOfPlayers; i++)
        {

            GameObject spawnedPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
            
            spawnedPlayerControllers.Insert(i, spawnedPlayer.GetComponent<PlayerController>());

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            spawnedPlayer.transform.position = randomSpawnPosition;

            Quaternion randomSpawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            spawnedPlayer.transform.rotation = randomSpawnRotation;

        }
    }

    public void TogglePauseMenu(bool newState)
    {
        pauseMenu.SetActive(newState);

        
    }

}
