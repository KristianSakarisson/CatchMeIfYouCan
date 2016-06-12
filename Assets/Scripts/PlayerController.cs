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

	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

		Debug.Log ("seekers: " + seekers.Count);
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
			animator.SetBool ("isWalking", true);
		} 
		else 
		{
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
		foreach (Transform t in enemies)
		{
			float dist = Vector3.Distance(t.position, currentPos);
			if (dist < minDist && dist != 0)
			{
				tMin = t;
				minDist = dist;
			}
		}
		return minDist;
	}
}
