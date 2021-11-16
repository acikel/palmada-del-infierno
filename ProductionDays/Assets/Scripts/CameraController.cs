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
        InstanceRepository.Instance.AddOnce(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraFollowing)
        {
            camPosX.transform.position = new Vector3(Player.transform.position.x, camPosX.transform.position.y, camPosX.transform.position.z);
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
    }
}
