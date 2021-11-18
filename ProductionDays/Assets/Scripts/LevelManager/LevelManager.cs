using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private CameraController camController;
    [SerializeField] public List<GameObject> Rooms;
    [SerializeField] private DialogueManager diaMan;
    private int currentRoom = -1;
    private bool activeRoomCleared = false;
    private int _roomAmount;

    private GameObject lvl;
    private PlayerController player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayGameMusic();
        if (InstanceRepository.Instance.Get<LevelManager>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
            DontDestroyOnLoad(this.gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        diaMan = InstanceRepository.Instance.Get<DialogueManager>();

        _roomAmount = Rooms.Count;
        lvl = GameObject.FindGameObjectWithTag("lvl");
        if (lvl != null)
        {
            int i = 0;
            foreach (Transform child in lvl.transform)
            {
                Rooms[i] = lvl.transform.GetChild(i).gameObject;
                i++;
            }
        }

        player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
        player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;

        Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
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
        AudioManager.Instance.ChangeGameMusic(GameMusic.Fight);
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
