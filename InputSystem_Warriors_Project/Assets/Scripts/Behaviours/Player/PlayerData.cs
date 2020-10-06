using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Objects/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement Settings")]
    public float movementSpeed = 3;

    //Movement Direction (From PlayerController)
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public bool movementInputHappening;

    public void UpdatePlayerMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }
}
