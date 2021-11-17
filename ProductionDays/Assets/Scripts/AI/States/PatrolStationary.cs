
using UnityEngine;

public class PatrolStationary : State
{
    private Vector3 position;
    private float sightDistance = 3f;

    public override void OnStart()
    {
        Debug.Log("Patrol");
        position = GameObject.transform.position;
    }

    public override void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(position, GameObject.transform.forward, sightDistance);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Blackboard.Set(BlackboardConstants.VARIABLE_TARGET, hit.collider.gameObject);
                SetState(new EngagePlayer());
                break;
            }
        }
    }
}
