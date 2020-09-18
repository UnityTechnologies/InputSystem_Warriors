using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContextPanelsBehaviour : MonoBehaviour
{
    public GameObject[] contextPanels;
    private int currentContextPanelID;

    public void SetupContextPanels()
    {
        currentContextPanelID = 0;

        for(int i = 0; i < contextPanels.Length; i++)
        {
            contextPanels[i].SetActive(false);
        }

        contextPanels[currentContextPanelID].SetActive(true);
    }

    public void UpdateContextPanels(int newContextPanelID)
    {
        contextPanels[currentContextPanelID].SetActive(false);
        currentContextPanelID = newContextPanelID;
        contextPanels[currentContextPanelID].SetActive(true);
    }
    
}
