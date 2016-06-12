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
		Debug.Log ("started");
		if (!isLocalPlayer) {
			Debug.Log ("not local");
			return;
		}
		myCamera = (Transform)Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		myCamera.GetComponent<CameraController> ().player = gameObject;
		Debug.Log ("is local");
	}
	// Update is called once per frame
	void Update () {
		//if (!isLocalPlayer) 
		//{
		//Debug.Log("not local");
		//return;
		//}
		//myCamera = (Transform) Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		//myCamera.GetComponent<CameraController> ().player = gameObject;
		//Debug.Log ("is local");
	}

	//IEnumerator StartingThing (float time)
	//{
		//einki

		//yield return new WaitForSeconds(time);
		//Debug.Log ("started");
		//if (!isLocalPlayer) 
		//{
			//Debug.Log("not local");
			//return;
		//}
		//myCamera = (Transform) Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		//myCamera.GetComponent<CameraController> ().player = gameObject;
		//Debug.Log ("is local");
	//}
}
