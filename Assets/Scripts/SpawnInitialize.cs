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

	IEnumerator PlayerCheckInterval(float time)
	{
		yield return new WaitForSeconds (time);

		foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player")) 
		{
			if(!statistics.seekers.Contains(pl.transform))
				statistics.seekers.Add (pl.transform);
		}

		StartCoroutine (PlayerCheckInterval (time));
	}
	void Start () 
	{
        if (!isLocalPlayer)
        {
            return;
        }

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

		statistics.playerType = Statistics.PlayerType.seeker;

		Debug.LogError("Seeker");

		/*if (GameObject.FindWithTag ("Hider") != null) 
		{
			GameObject.FindWithTag("Hider").GetComponent<PlayerController>().seekers.Add (transform);
		}*/
		foreach(GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
			statistics.seekers.Add (pl.transform);
		//statistics.GetComponent<Statistics> ().seekers.Add (transform);

        if (isServer)
        {
            seed = 2;
            statistics.seed = seed;
			statistics.playerType = Statistics.PlayerType.hider;

			gameObject.GetComponent<PlayerController>().isHider = true;
			Debug.LogError("Hider");
        }

        StartCoroutine(WaitBeforeInitialize(.05f));
		StartCoroutine (PlayerCheckInterval (1f));

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
