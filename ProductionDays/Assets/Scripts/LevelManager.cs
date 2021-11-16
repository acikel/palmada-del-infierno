using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private CameraController camController;
    [SerializeField] private List<GameObject> Rooms;
    private int currentRoom = 0;

    // Start is called before the first frame update
    void Start()
    {
        InstanceRepository.Instance.AddOnce(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camController == null)
        {
            camController = InstanceRepository.Instance.Get<CameraController>();
            camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
        }
    }

    public GameObject GetCurrentRoom() { return Rooms[currentRoom]; }

    private void RoomCleared()
    {
        camController.cameraFollowing = true;
    }

    public void RoomReached()
    {
        camController.cameraFollowing = false;
        currentRoom++;
        camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
    }

    void OnConfirmButton()
    {
        RoomCleared();
    }
}
