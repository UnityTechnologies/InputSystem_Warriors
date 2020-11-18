using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelQuitBehaviour : MonoBehaviour
{
    public void ButtonPressedQuitGame()
    {
        QuitGame();
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
