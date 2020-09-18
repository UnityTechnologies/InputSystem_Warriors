using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

    private GameManager gameManager;
    
    //Game Mode
    private SerializedProperty currentGameModeProperty;

    //Single Player
    private SerializedProperty inScenePlayerProperty;

    //Local Multiplayer
    private SerializedProperty playerPrefabProperty;
    private SerializedProperty numberOfPlayersProperty;
    private SerializedProperty spawnRingCenterProperty;
    private SerializedProperty spawnRingRadiusProperty;

    void OnEnable()
    {

        //Game Mode
        currentGameModeProperty = serializedObject.FindProperty("currentGameMode");

        //Single Player
        inScenePlayerProperty = serializedObject.FindProperty("inScenePlayer");

        //Local Multiplayer
        playerPrefabProperty = serializedObject.FindProperty("playerPrefab");
        numberOfPlayersProperty = serializedObject.FindProperty("numberOfPlayers");
        spawnRingCenterProperty = serializedObject.FindProperty("spawnRingCenter");
        spawnRingRadiusProperty = serializedObject.FindProperty("spawnRingRadius");

    }


    public override void OnInspectorGUI()
    {

        gameManager = (GameManager)target;

        serializedObject.Update();

        DrawGameModeGUI();
        
        DrawSpaceGUI(3);

        EditorGUILayout.LabelField("Mode Settings", EditorStyles.boldLabel);

        if(gameManager.currentGameMode == GameMode.SinglePlayer)
        {
            DrawSinglePlayerGUI();
        }

        if(gameManager.currentGameMode == GameMode.LocalMultiplayer)
        {
            DrawLocalMultiplayerGUI();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawGameModeGUI()
    {
        EditorGUILayout.LabelField("Game Mode", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(currentGameModeProperty);
    }

    void DrawSinglePlayerGUI()
    {
         EditorGUILayout.PropertyField(inScenePlayerProperty);
    }

    void DrawLocalMultiplayerGUI()
    {

        EditorGUILayout.PropertyField(playerPrefabProperty);

        EditorGUILayout.PropertyField(numberOfPlayersProperty);

        EditorGUILayout.PropertyField(spawnRingCenterProperty);
        EditorGUILayout.PropertyField(spawnRingRadiusProperty);
    }

    void DrawSpaceGUI(int amountOfSpace)
    {
        for(int i = 0; i < amountOfSpace; i++)
        {
            EditorGUILayout.Space();
        }
    }
}
