using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnInitialize : NetworkBehaviour {

	public Transform cam;
	public Transform myCamera;

	void Start () 
	{
		if (!isLocalPlayer)
        {
			return;
		}

        GameObject.Find("Scripts").GetComponent<Statistics>().player = gameObject;

        myCamera = (Transform)Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		myCamera.GetComponent<CameraController> ().player = gameObject;
	}

	void Update () {
	}
}
