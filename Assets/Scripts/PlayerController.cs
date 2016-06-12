using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerController : NetworkBehaviour
{
    public float moveConstant;
	public Animator animator;

	//The uhhh, the bit you know the bit
	public float health = 100;
	public int negativeStamina = 5;

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
				//moveConstant = 2f;
				walk();
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log("Source name: " + source.name);
				source.Stop ();
			}
			if (Input.GetKey (KeyCode.Space)) {
				//moveConstant = 3f;
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
				//Debug.Log ("Closest dist is: " + GetClosestSeeker(statistics.seekers));

				if(GetClosestSeeker(statistics.seekers) < 0.4)
				{
					health -= Time.deltaTime * negativeStamina;
					if(moveConstant >= 0)
					{
						moveConstant -= Time.deltaTime;
					}
				}
			}
		}
    }

	//Finds nearest seeker.
	float GetClosestSeeker(List<Transform> enemies)
	{
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Transform t in enemies) {
			float dist = Vector3.Distance (t.position, currentPos);
			if (dist < minDist && dist != 0) {
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
