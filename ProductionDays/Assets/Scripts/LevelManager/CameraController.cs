using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool cameraFollowing = false;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject camPosX;
    [SerializeField] private float camSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        if (InstanceRepository.Instance.Get<CameraController>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
            DontDestroyOnLoad(this.gameObject);
        }
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
        Debug.Log("after Call");
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
        Debug.Log("Cor");
        cameraFollowing = true;
    }
}
