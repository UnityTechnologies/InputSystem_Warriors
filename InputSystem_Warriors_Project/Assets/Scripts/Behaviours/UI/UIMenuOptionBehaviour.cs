using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuOptionBehaviour : MonoBehaviour
{
    public int contextPanelID;

    public void ButtonOptionSelected()
    {
        UIManager.Instance.MenuButtonOptionSelected(contextPanelID);
    }
}
