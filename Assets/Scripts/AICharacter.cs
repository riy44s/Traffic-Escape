using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameter")]
    [SerializeField] private float patrolSpeed = 3f;
    private bool movingLeft;

    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = enemy.rotation;
        enemy.position = rightEdge.position;
    }
    private void Update()
    {     
        Patrol();
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;

        float newYRotation = movingLeft ? -90 : 90f;
        enemy.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, newYRotation, initialRotation.eulerAngles.z);
    }

    private void MoveDirection(int direction)
    {
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * patrolSpeed,
        enemy.position.y, enemy.position.z);
    }

}
