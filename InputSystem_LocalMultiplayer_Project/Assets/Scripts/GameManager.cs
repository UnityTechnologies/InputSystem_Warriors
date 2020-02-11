using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{

    [Header("Camera")]
    public Camera mainCamera;

    [Header("In-Scene Player Settings")]
    public GameObject inScenePlayer;

    [Header("Spawn Player Settings")]
    public bool spawnMultiplePlayers = false;
    public GameObject playerPrefab;
    public int numberOfPlayers;
    public Vector3 spawnArea;

    //Spawned Players
    private List<PlayerController> activePlayerControllers;

    [Header("UI")]
    public GameObject pauseMenu;

    void Start()
    {
        SetupUIMenu();
        SetupActivePlayers();
    }

    void SetupUIMenu()
    {
        UIMenuManager.Instance.ToggleMenu(false);
    }

    void SetupActivePlayers()
    {

        activePlayerControllers = new List<PlayerController>(numberOfPlayers);

        if(spawnMultiplePlayers)
        {

            Destroy(inScenePlayer);
            SpawnPlayers();

        }
        else if(!spawnMultiplePlayers)
        {

            PlayerController inScenePlayerController = inScenePlayer.GetComponent<PlayerController>();
            activePlayerControllers.Add(inScenePlayerController);

            UpdateUIMenuPlayerList();

        }
    }

    void SpawnPlayers()
    {

        for(int i = 0; i < numberOfPlayers; i++)
        {

            GameObject spawnedPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
            
            activePlayerControllers.Insert(i, spawnedPlayer.GetComponent<PlayerController>());

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
            spawnedPlayer.transform.position = randomSpawnPosition;

            Quaternion randomSpawnRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            spawnedPlayer.transform.rotation = randomSpawnRotation;

        }

        UpdateUIMenuPlayerList();
    }

    void UpdateUIMenuPlayerList()
    {
        UIMenuManager.Instance.UpdateRebindPlayerPanelList();
    }


    public void TogglePauseMenu(bool newState)
    {

        
        UIMenuManager.Instance.ToggleMenu(newState);

        
        for(int i = 0; i < activePlayerControllers.Count; i++)
        {
            //Pause Menu Is On -> Switch from Player Controls to Menu Controls
            if(newState == true)
            {
                activePlayerControllers[i].EnablePauseMenuControls();
            }
            //Pause Menu Is Off -> Switch from Menu Controls to Player Controls
            else if(newState == false)
            {
                activePlayerControllers[i].EnableGameplayControls();
            }

        }
        
    }

    public List<PlayerController> GetActivePlayerControllers()
    {
        return activePlayerControllers;
    }

}
