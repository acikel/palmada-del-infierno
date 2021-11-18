using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : MonoBehaviour
{

    private DialogueManager diaMan;
    private bool blocked = false;
    private GameObject Player;
    private GameObject Exit;

    // Start is called before the first frame update
    void Start()
    {
        Exit = transform.parent.transform.GetChild(3).gameObject;
        Exit.SetActive(false);
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
                Debug.Log("freed");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Exit.SetActive(true);
            if (diaMan.dialogueStarted)
            {
                Player = other.gameObject;
                other.gameObject.GetComponent<PlayerController>().SetBlockActive();
                blocked = true;
                Debug.Log("blocked");
            }
        }
    }
}
