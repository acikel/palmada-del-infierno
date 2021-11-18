using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : MonoBehaviour
{

    private DialogueManager diaMan;
    private bool blocked = false;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(diaMan == null)
        {
            diaMan = InstanceRepository.Instance.Get<DialogueManager>();
        }
        else
        {
            if(blocked && !diaMan.dialogueStarted)
            {
                Player.GetComponent<PlayerController>().SetBlockDisable();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (diaMan.dialogueStarted)
            {
                Player = other.gameObject;
                other.gameObject.GetComponent<PlayerController>().SetBlockActive();
                blocked = true;
            }
        }
    }
}
