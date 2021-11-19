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

    public bool reloading = false;
    public int _EndSceneGood;
    public int _EndSceneBad;

    private GameObject lvl;
    private PlayerController player;
    private StoryManager _storymanager;
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
        SceneManager.sceneUnloaded += OnSceneUnloaded;

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
        
        if(_storymanager.storyComplete)
        {
            if (_storymanager.loveScore >= 0) SceneManager.LoadScene(_EndSceneGood);
            else SceneManager.LoadScene(_EndSceneBad);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        diaMan = InstanceRepository.Instance.Get<DialogueManager>();
        _storymanager = diaMan.GetComponent<StoryManager>();
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

        if (reloading)
        {
            RoomReload();
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        reloading = true;
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
        if (currentRoom == -1) currentRoom = 0;
        camController.SetActiveRoom(Rooms[currentRoom].transform.position.x);
        player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
        player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
        Rooms[currentRoom].GetComponent<Room>().SpawnEnemies();
    }

    public void RoomReload()
    {
        AudioManager.Instance.ChangeGameMusic(GameMusic.Fight);
        Debug.Log("test");
        camController.cameraFollowing = false;
        activeRoomCleared = false;
        if(currentRoom >= 0)
        {
            camController.YeetCamToPos(Rooms[currentRoom].transform.position.x, Rooms[currentRoom]);
            player._lvlWidth = Rooms[currentRoom].GetComponent<Room>().xScale;
            player._lvlDeapth = Rooms[currentRoom].GetComponent<Room>().zScale;
            diaMan.SetCheckpointStory(currentRoom);
        }
        
        
    }

    void OnConfirmButton()
    {
        //RoomCleared();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
