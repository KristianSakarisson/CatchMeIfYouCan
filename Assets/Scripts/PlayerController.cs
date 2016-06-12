using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerController : NetworkBehaviour
{
    public float moveConstant;
	public Animator animator;

	//Array of seekers, used by hider for checking distance.
	public bool isHider = false;
	public ArrayList seekers = new ArrayList ();
	public Statistics statistics;
	public AudioClip walking, running, doorOpen, doorClose;

	private AudioSource source;


	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

		Debug.Log ("seekers: " + seekers.Count);
		source = GetComponent<AudioSource> ();

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.305f);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("rooms").SetActive(false);
            GameObject.Find("Scripts").GetComponent<Initialize>().Init();
        }
    }

    void FixedUpdate() 
	{
		if (!isLocalPlayer) {
			return;
		}
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, verticalMove);

        if (verticalMove + horizontalMove > 0f || verticalMove + horizontalMove < 0f) {
			transform.rotation = Quaternion.FromToRotation (Vector2.right, new Vector2 (horizontalMove, verticalMove));

			if (!source.isPlaying) {
				moveConstant = 2f;
				walk();
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log("Source name: " + source.name);
				source.Stop ();
			}
			if (Input.GetKey (KeyCode.Space)) {
				moveConstant = 3f;
				if (source.isPlaying)
					return;
				run();
			}

			animator.SetBool ("isWalking", true);
		} 
		else 
		{
			source.Stop ();
			animator.SetBool("isWalking", false);
		}

		if (isHider) {
			if(statistics.seekers.Count > 1)
			{
				Debug.Log ("Closest dist is: " + GetClosestSeeker(statistics.seekers));
			}
		}
    }

	//Finds nearest seeker.
	float GetClosestSeeker(List<Transform> enemies)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Transform t in enemies) {
			float dist = Vector3.Distance (t.position, currentPos);
			if (dist < minDist && dist != 0) {
				tMin = t;
				minDist = dist;
			}
		}
		return minDist;
	}

	public void walk()
	{
		source.PlayOneShot (walking, 1F);
	}
	public void run()
	{
		source.PlayOneShot (running, 1F);
	}
	public void openDoor()
	{
		source.PlayOneShot (doorOpen, 0.7F);
	}
	public void closeDoor()
	{
		source.PlayOneShot (doorOpen, 0.7F);
	}
}
