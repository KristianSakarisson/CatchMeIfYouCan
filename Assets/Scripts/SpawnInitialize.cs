using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnInitialize : NetworkBehaviour {

	public Transform cam;
	public Transform myCamera;

	// Use this for initialization
	void Start () 
	{
		GameObject.Find ("Scripts").GetComponent<Statistics> ().player = gameObject;
		if (!isLocalPlayer) {
			return;
		}
		myCamera = (Transform)Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		myCamera.GetComponent<CameraController> ().player = gameObject;
	}

	// Update is called once per frame
	void Update () {
	}
}
