using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OldUIMenuBehaviour : MonoBehaviour
{

    /*
    [Header("References")]
    public Camera UICamera;
    private List<PlayerController> activePlayerControllers;
    private List<UIMenuPlayerDeviceDisplayBehaviour> spawnedUIPlayerDeviceDisplayBehaviours;

    [Header("Placeholder GameObjects")]
    public GameObject[] placeholderGameObjects;

    [Header("Player Panel Settings")]
    public GameObject UIPlayerDeviceDisplayPrefab;
    public Transform UIPlayerDeviceDisplayRoot;

    public void SetupUIMenuPlayerPanelList()
    {
        DestroyPlaceholderObjects();
        GetCurrentPlayerDatas();
        SetupCurrentPlayerUIMenuPanels();
    }

    void DestroyPlaceholderObjects()
    {
        for(int i = 0; i < placeholderGameObjects.Length; i++)
        {
            Destroy(placeholderGameObjects[i]);
        }
    }

    void GetCurrentPlayerDatas()
    {
        activePlayerControllers = GameManager.Instance.GetActivePlayerControllers();
    }

    void SetupCurrentPlayerUIMenuPanels()
    {

        spawnedUIPlayerDeviceDisplayBehaviours = new List<UIMenuPlayerDeviceDisplayBehaviour>();

        for(int i = 0; i < activePlayerControllers.Count; i++)
        {
            GameObject spawnedUIPlayerDeviceDisplayPanel = Instantiate(UIPlayerDeviceDisplayPrefab, UIPlayerDeviceDisplayRoot.position, UIPlayerDeviceDisplayRoot.rotation) as GameObject;
            spawnedUIPlayerDeviceDisplayPanel.transform.SetParent(UIPlayerDeviceDisplayRoot, false);

            PlayerInput spawnedPlayerInput = activePlayerControllers[i].GetPlayerInput();

            int spawnedPlayerIndex = spawnedPlayerInput.playerIndex;
            string spawnedPlayerDevicePath = spawnedPlayerInput.devices[0].ToString();



            spawnedUIPlayerDeviceDisplayPanel.GetComponent<PlayerDeviceRebindBehaviour>().playerIndex = spawnedPlayerIndex;
            spawnedUIPlayerDeviceDisplayBehaviours.Add(spawnedUIPlayerDeviceDisplayPanel.GetComponent<UIMenuPlayerDeviceDisplayBehaviour>());
            spawnedUIPlayerDeviceDisplayBehaviours[i].SetPlayerDeviceDisplay(spawnedPlayerIndex, spawnedPlayerDevicePath);
        }
    }

    void UpdateCurrentPlayerUIMenuPanels()
    {
        for(int i = 0; i < spawnedUIPlayerDeviceDisplayBehaviours.Count; i++)
        {
            PlayerInput spawnedPlayerInput = activePlayerControllers[i].GetPlayerInput();
            string spawnedPlayerDevicePath = spawnedPlayerInput.devices[0].ToString();

            spawnedUIPlayerDeviceDisplayBehaviours[i].UpdatePlayerDeviceDisplay(spawnedPlayerDevicePath);
        }
    }

    public void ToggleMenu(bool newState)
    {
        if(newState == true)
        {
            UpdateCurrentPlayerUIMenuPanels();
        }
        
        UICamera.enabled = newState;
    }
    */
}
