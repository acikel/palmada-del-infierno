
using UnityEngine;

public class PatrolStationary : State
{
    private Vector3 position;
    private float sightDistance = 8f;

    private Transform playerTransform;

    public override void OnStart()
    {
        Debug.Log("Patrol");
        position = GameObject.transform.position;

        playerTransform = GameObject.FindWithTag("Player").transform;
        //playerTransform = InstanceRepository.Instance.Get<PlayerController>().transform;
    }

    public override void Update()
    {
        if (Vector3.Distance(position, playerTransform.position) < sightDistance) {
            Blackboard.Set(BlackboardConstants.VARIABLE_TARGET, playerTransform.gameObject);
            SetState(new EngagePlayer());
        }
    }
}
