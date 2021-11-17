using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionExit : MonoBehaviour
{
    private LevelManager lvlManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lvlManager = InstanceRepository.Instance.Get<LevelManager>();
            lvlManager.RoomReached();
            gameObject.SetActive(false);
        }    
    }
}
