using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnInitialize : NetworkBehaviour {

	public Transform cam;
	public Transform myCamera;

    public Statistics statistics;
    [SyncVar]
    public int seed;

    IEnumerator WaitBeforeInitialize(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Hello!");

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

        GameObject.Find("Scripts").GetComponent<Initialize>().Init();

        statistics.player = gameObject;

        myCamera = (Transform)Instantiate(cam, transform.position + Vector3.back, transform.rotation);
        myCamera.GetComponent<CameraController>().player = gameObject;
    }

	void Start () 
	{
        if (!isLocalPlayer)
        {
            return;
        }

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

        statistics.playerType = Statistics.PlayerType.seeker;

        if (isServer)
        {
            seed = 2;
            statistics.seed = seed;
            statistics.playerType = Statistics.PlayerType.hider;
        }

        StartCoroutine(WaitBeforeInitialize(.05f));
	}

    float time = 0f;

    void Update()
    {
        time += Time.deltaTime;

        if (isLocalPlayer)
        {
            //Debug.Log(statistics.playerType);
            //Debug.Log(Random.seed);
        }

        if(isServer)
        {
            //seed++;
            //Debug.Log("Server");
            //time = 0f;
        }
    }
}
