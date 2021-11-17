using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CameraController camController;
    [SerializeField] public List<GameObject> Rooms;
    [SerializeField] private DialogueManager diaMan;
    public int currentRoom = 0;
    private bool activeRoomCleared = false;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        if (InstanceRepository.Instance.Get<LevelManager>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
            DontDestroyOnLoad(this.gameObject);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
        player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;

        Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(camController == null)
        {
            camController = InstanceRepository.Instance.Get<CameraController>();
            if (camController == null)
            {
                Debug.Log("null");
            }else camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
        }

        if (Rooms[currentRoom].GetComponent<Room>().currentEnemyCount <= 0 && !activeRoomCleared)
        {
            RoomCleared();
        }
    }

    public GameObject GetCurrentRoom() { return Rooms[currentRoom]; }

    public void RoomCleared()
    {
        activeRoomCleared = true;
        camController.SetIntermissionLvl();
        //DialogeManager Call Function
        diaMan.StartDialogue();

    }

    public void RoomReached()
    {
        Debug.Log("test");
        camController.cameraFollowing = false;
        activeRoomCleared = false;
        currentRoom++;
        camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
        player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
        player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
        Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
    }

    void OnConfirmButton()
    {
        //RoomCleared();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
