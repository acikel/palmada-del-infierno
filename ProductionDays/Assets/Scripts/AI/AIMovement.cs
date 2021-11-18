using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private bool lookRight = true;

    [SerializeField]
    private float moveSpeed = 5f;
    
    
    private static readonly int velocityParameter = Animator.StringToHash("Velocity");

    private Animator animator;
    public Animator Animator
    {
        get
        {
            animator ??= GetComponent<Animator>();
            return animator;
        }
    }

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
                if (Animator != null)
                    Animator.SetFloat(velocityParameter, 0f);
                
                return true;     
            }
        }
        
        move = move * moveSpeed * Time.deltaTime;
        transform.position = position + move;
        
        if (Animator != null)
            Animator.SetFloat(velocityParameter, move.magnitude);

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
}
