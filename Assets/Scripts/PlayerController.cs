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

	System.DateTime initGame;

	private double stanimaRegenTime = 15;
	private bool time = false;

	void Start ()
	{
		initGame = new System.DateTime();
		animator = gameObject.GetComponent<Animator> ();
		statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

		Debug.Log ("seekers: " + seekers.Count);
		source = GetComponent<AudioSource> ();
	}

    void FixedUpdate() 
	{
		if (!isLocalPlayer) {
			return;
		}

		System.DateTime timeNow = System.DateTime.Now;
		double second = (timeNow - initGame).TotalSeconds;
		if(second > stanimaRegenTime) {
			time = true;
			initGame = System.DateTime.Now;
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


			if (Input.GetKey (KeyCode.Space) && time == true) {
				source.Stop ();
				time = false;
				moveConstant = 3f;
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
