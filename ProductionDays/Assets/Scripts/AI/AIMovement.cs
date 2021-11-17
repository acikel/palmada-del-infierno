using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private bool lookRight = true;

    [SerializeField]
    private float moveSpeed = 5f;
    
    // [SerializeField]
    // private float movementAbortTime = 3f;
    
    // private bool movingTowardsDestination;
    // private Vector3 destination;

    // private Coroutine movementCoroutine;

    private void Start()
    {
        if (transform.rotation.y < 0)
            lookRight = false;
    }

    public bool WalkTowards(Vector3 destination, float reachDistance)
    {
        Vector3 position = transform.position;

        TurnTowardsDestination(destination, position);
        Vector3 difference = destination - position;
        difference.y = 0;
        Vector3 move = difference.normalized;

        if (Vector3.Distance(position, destination) < reachDistance)
        {
            if (Mathf.Abs(position.z - destination.z) > 0.1f)
            {
                move.x = 0;
            }
            else
            {
                return true;     
            }
        }
        
        move = move * moveSpeed * Time.deltaTime;
        transform.position = position + move;

        return false;
    }

    private void TurnTowardsDestination(Vector3 destination, Vector3 position)
    {
        Vector3 difference = destination - position;

        if (difference.x < 0 && lookRight)
        {
            lookRight = false;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        if (difference.x > 0 && !lookRight)
        {
            lookRight = true;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    // private IEnumerator WalkTowardsDestination()
    // {
    //     Vector3 lastPosition = transform.position;
    //     float lastPositionChangeTime = Time.time;
    //     
    //     while (movingTowardsDestination)
    //     {
    //         Vector3 position = transform.position;
    //
    //         Vector3 difference = destination - position;
    //         Vector3 move = Vector3.right;
    //         
    //         if (difference.x < 0)
    //             move = Vector3.left;
    //
    //         if (Vector3.Distance(position, destination) < 0.5f)
    //         {
    //             break;
    //         }
    //         
    //         move = move * moveSpeed * Time.deltaTime;
    //         transform.Translate(move);
    //         
    //         position += transform.position;
    //         if (Vector3.Distance(position, lastPosition) > 0.01f)
    //         {
    //             lastPosition = position;
    //             lastPositionChangeTime = Time.time;
    //         }
    //
    //         if (lastPositionChangeTime >= movementAbortTime)
    //         {
    //             // Abort movement after a given time trying
    //             break;
    //         }
    //
    //         yield return null;
    //     }
    // }
}
