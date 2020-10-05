using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuBehaviour : MonoBehaviour
{

    [Header("Sub-Behaviours")]
    public UIContextPanelsBehaviour uiContextPanelsBehaviour;
    public UIPanelRebindBehaviour uiPanelRebindBehaviour;

    [Header("Core Object References")]
    public GameObject uiMenuCameraObject;
    public GameObject uiMenuCanvasObject;

    [Header("Default Selected")]
    public GameObject defaultSelectedGameObject;

    
    [Header("Player Display")]
    public DeviceDisplayConfigurator deviceDisplayconfigurator;
    public Image deviceDisplayIcon;
    public TextMeshProUGUI IDDisplayText;
    

    public void SetupBehaviour()
    {   
        UpdateUIMenuState(false);
    }

    public void UpdateUIMenuState(bool newState)
    {
        switch (newState)
        {
            case true:
                ResetContextPanels();
                UpdateEventSystemDefaultSelected();
                UpdateEventSystemUIInputModule();
                UpdateUIPlayerDisplay();
                UpdateUIRebindActions();
                break;

            case false:
                
                break;
        }

        UpdateCoreUIObjectsState(newState);
    }

    void ResetContextPanels()
    {
        uiContextPanelsBehaviour.SetupContextPanels();
    }

    

    void UpdateCoreUIObjectsState(bool newState)
    {
        uiMenuCameraObject.SetActive(newState);
        uiMenuCanvasObject.SetActive(newState);
    }

    
    void UpdateUIPlayerDisplay()
    {
        PlayerController focusedPlayerController = GameManager.Instance.GetFocusedPlayerController();
        
        //ID
        int focusedPlayerID = focusedPlayerController.GetPlayerID();
        IDDisplayText.SetText((focusedPlayerID + 1).ToString());

        //Color
        Color focusedPlayerDeviceColor = deviceDisplayconfigurator.GetDeviceColor(focusedPlayerController.GetPlayerInput());
        deviceDisplayIcon.color = focusedPlayerDeviceColor;
    }

    void UpdateEventSystemDefaultSelected()
    {
        EventSystemManager.Instance.SetCurrentSelectedGameObject(defaultSelectedGameObject);
    }

    void UpdateEventSystemUIInputModule()
    {
        EventSystemManager.Instance.UpdateActionAssetToFocusedPlayer();
    }

    void UpdateUIRebindActions()
    {
        uiPanelRebindBehaviour.UpdateRebindActions();
    }

    public void SwitchUIContextPanels(int selectedPanelID)
    {
        uiContextPanelsBehaviour.UpdateContextPanels(selectedPanelID);
    }
    
}
