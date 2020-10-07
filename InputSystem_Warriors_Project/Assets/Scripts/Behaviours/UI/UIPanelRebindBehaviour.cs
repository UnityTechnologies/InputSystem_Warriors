using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelRebindBehaviour : MonoBehaviour
{
    public UIRebindActionBehaviour[] uiRebindActionBehaviours;

    public void UpdateRebindActions()
    {
        for(int i = 0; i < uiRebindActionBehaviours.Length; i++)
        {
            uiRebindActionBehaviours[i].UpdateBehaviour();
        }
    }
}
