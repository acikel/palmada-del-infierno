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
    public int currentRoom = 0;
    private bool activeRoomCleared = false;
    private int _roomAmount;

    private bool reloading = false;

    private GameObject lvl;
    private PlayerController player;
    
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (InstanceRepository.Instance.Get<LevelManager>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayGameMusic();
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

        if(currentRoom >= 0)
        {
            if (Rooms[currentRoom].GetComponent<Room>().currentEnemyCount <= 0 && !activeRoomCleared)
            {
                Debug.Log(Rooms[currentRoom]);
                Debug.Log(Rooms[currentRoom].GetComponent<Room>().currentEnemyCount);
                Debug.Log("Cleared");
                RoomCleared();
            }
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

        activeRoomCleared = false;
        camController = InstanceRepository.Instance.Get<CameraController>();
        Debug.Log(currentRoom);
        if(currentRoom >= 0)
        {
            player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
            player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
        }
        
        RoomReload();
    }
    public GameObject GetCurrentRoom() { return Rooms[currentRoom]; }

    public void RoomCleared()
    {
        Debug.Log("ImCleared");
        activeRoomCleared = true;
        currentRoom++;
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
        camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
        player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
        player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
        Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
    }

    public void RoomReload()
    {
        AudioManager.Instance.ChangeGameMusic(GameMusic.Fight);
        Debug.Log("test");
        if (reloading)
        {
            camController.cameraFollowing = false;
        }
        activeRoomCleared = false;
        if(currentRoom >= 0)
        {
            camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
            player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
            player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
            Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
        }
        
        
    }

    void OnConfirmButton()
    {
        //RoomCleared();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
