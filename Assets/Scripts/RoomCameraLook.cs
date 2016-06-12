using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RoomCameraLook : NetworkBehaviour
{
    Camera cam;
    Statistics statistics;

	void Start ()
    {
        if (!isLocalPlayer)
            return;

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();
	}
	
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        statistics.player = gameObject;

        if (statistics.GetPlayerRoom() != null)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            GetComponentInChildren<PlayerController>().enabled = true;
            SetCamera(Camera.main);
            cam.transform.position = statistics.GetPlayerRoom().GetPosition() + Vector3.back;
        }
	}

    void SetCamera(Camera input)
    {
        cam = input;
    }
}
