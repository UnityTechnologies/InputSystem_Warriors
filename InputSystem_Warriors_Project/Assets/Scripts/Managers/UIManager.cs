using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [Header("Sub-Behaviours")]
    public UIMenuBehaviour uiMenuBehaviour;

    public void SetupManager()
    {
        uiMenuBehaviour.SetupBehaviour();
    }

    public void UpdateUIMenuState(bool newState)
    {
        uiMenuBehaviour.UpdateUIMenuState(newState);
    }

    public void MenuButtonOptionSelected(int newPanelID)
    {
        uiMenuBehaviour.SwitchUIContextPanels(newPanelID);
    }

}
