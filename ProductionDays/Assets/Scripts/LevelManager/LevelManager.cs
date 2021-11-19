using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;


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
    public float loveScore;
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

        GameObject temp;
        temp = Instantiate(PlayerPrefab, new Vector3(-26.71f, 0.214f, 0), Quaternion.Euler(0, 90, 0));
        player = temp.GetComponent<PlayerController>();

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
        /*if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }*/

        
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

        InstanceRepository.Instance.Get<DialogueManager>().gameObject.GetComponent<StoryManager>()
            .loveScore = (int)loveScore;
        if (reloading)
        {
            Time.timeScale = 1.0f;
            RoomReload();
        }

        if (currentRoom >= 0 && reloading)
        {
            Debug.Log(currentRoom);
            Debug.Log(lvl.transform.GetChild(currentRoom).childCount);
            Vector3 spawn = lvl.transform.GetChild(currentRoom).gameObject.transform.GetChild(5).gameObject.transform.position;
            Debug.Log(spawn);
            if (spawn != null)
            {
                //transform.position = new Vector3(spawn.x, 0.214f, spawn.z);
                GameObject temp;
                temp = Instantiate(PlayerPrefab, new Vector3(spawn.x, 0.214f, spawn.z), Quaternion.Euler(0, 90, 0));
                player = temp.GetComponent<PlayerController>();
            }
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
