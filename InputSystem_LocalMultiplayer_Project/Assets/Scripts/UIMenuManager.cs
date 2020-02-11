using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class UIMenuManager : Singleton<UIMenuManager>
{
    [Header("References")]
    public GameObject UIMenuCameraObject;

    [Header("In-Scene Player Panel")]
    public GameObject inScenePlayerRebindPanel;

    [Header("Player Panel Settings")]
    public GameObject playerRebindPanelPrefab;
    public Transform playerRebindListRoot;

    public void UpdateRebindPlayerPanelList()
    {

        Destroy(inScenePlayerRebindPanel);

        
        List<PlayerController> activePlayerControllers = GameManager.Instance.GetActivePlayerControllers();

        for(int i = 0; i < activePlayerControllers.Count; i++)
        {
            GameObject spawnedPlayerRebindPanel = Instantiate(playerRebindPanelPrefab, playerRebindListRoot.position, playerRebindListRoot.rotation) as GameObject;
            spawnedPlayerRebindPanel.transform.SetParent(playerRebindListRoot, false);

            PlayerInput spawnedPlayerInput = activePlayerControllers[i].GetPlayerInput();
            
            int spawnedPlayerIndex = spawnedPlayerInput.playerIndex;
            string spawnedPlayerDevicePath = spawnedPlayerInput.devices[0].ToString();

            spawnedPlayerRebindPanel.GetComponent<UIPlayerRebindDisplayBehaviour>().SetupPanelDisplays(spawnedPlayerIndex, spawnedPlayerDevicePath);

        }
        
        
    }

    public void ToggleMenu(bool newState)
    {
        UIMenuCameraObject.SetActive(newState);
    }
}
