using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnInitialize : NetworkBehaviour {

	public Transform cam;
	public Transform myCamera;

    public Statistics statistics;
    [SyncVar]
    public int seed;

	void Start () 
	{
        if (isServer)
            seed = Random.Range(0, 10);
        else if (isClient)
            Debug.Log("seed: " + seed + ", random.seed: " + Random.seed);

        if (!isLocalPlayer)
        {
            return;
        }

        Random.seed = seed;

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

        GameObject.Find("Scripts").GetComponent<Initialize>().Init();

        statistics.player = gameObject;

        myCamera = (Transform)Instantiate (cam, transform.position + Vector3.back, transform.rotation);
		myCamera.GetComponent<CameraController>().player = gameObject;
	}
}
