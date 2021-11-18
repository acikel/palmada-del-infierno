using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public bool cameraFollowing = false;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject camPosX;
    [SerializeField] private float camSpeed = 15;

    // Start is called before the first frame update
    void Awake()
    {
        if (InstanceRepository.Instance.Get<CameraController>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = InstanceRepository.Instance.Get<PlayerController>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraFollowing)
        {
            camPosX.transform.position = new Vector3(Player.transform.position.x, camPosX.transform.position.y, camPosX.transform.position.z);
            Player.GetComponent<PlayerController>()._lvlCenterX = camPosX.transform.position.x;
            Player.GetComponent<PlayerController>()._lvlCenterZ = camPosX.transform.position.z;
        }
        else
        {
            if(LevelManager.Instance.currentRoom >= 0)
            {
                SetActiveRoom(LevelManager.Instance.currentRoom);
            }
        }
    }

    public void SetActiveRoom(float _activeRoomX)
    {
        StartCoroutine(MoveToRoomCenter(_activeRoomX));
    }

    IEnumerator MoveToRoomCenter(float _activeRoomX)
    {
        while (camPosX.transform.position.x < _activeRoomX)
        {
            camPosX.transform.position += new Vector3(camSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        Player.GetComponent<PlayerController>()._lvlCenterX = camPosX.transform.position.x;
        Player.GetComponent<PlayerController>()._lvlCenterZ = camPosX.transform.position.z;
    }

    public void SetIntermissionLvl()
    {
        StartCoroutine(MoveToPlayerPosition());
    }

    IEnumerator MoveToPlayerPosition()
    {
        while (camPosX.transform.position.x < Player.transform.position.x)
        {
            camPosX.transform.position += new Vector3(camSpeed * Time.deltaTime, 0, 0);
            Player.GetComponent<PlayerController>()._lvlCenterX = camPosX.transform.position.x;
            Player.GetComponent<PlayerController>()._lvlCenterZ = camPosX.transform.position.z;
            yield return null;
        }
        cameraFollowing = true;
    }

    public void YeetCamToPos(float _activeRoomX)
    {
        camPosX.transform.position = new Vector3(_activeRoomX,0f,0f);
    }

    void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }
}
