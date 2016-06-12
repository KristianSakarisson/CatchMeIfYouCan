using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerController : NetworkBehaviour
{
	public Transform winScreen;	

    public float moveConstant;
	public Animator animator;

	//The uhhh, the bit you know the bit
	public bool hasWon = false;
	public int negativeStamina = 5;

	//Array of seekers, used by hider for checking distance.
	public bool isHider = false;
	public ArrayList seekers = new ArrayList ();
	public Statistics statistics;
	public AudioClip walking, running;
	private AudioSource source;

	System.DateTime initGame;

	public int baseMovementSpeed 	= 2;
	public int sprintMovementSpeed 	= 3;

	public double stanimaRegenTime 	= 15;
	private bool time = false;

	void Start ()
	{
		initGame = new System.DateTime();
		animator = gameObject.GetComponent<Animator> ();
		statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

		Debug.Log ("seekers: " + seekers.Count);
		source = GetComponent<AudioSource> ();

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.305f);
	}

    float timeSinceDimensionJump = 10f;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        timeSinceDimensionJump += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && timeSinceDimensionJump > 10f)
        {
            GameObject.Find("rooms").SetActive(false);
            GameObject.Find("Scripts").GetComponent<Initialize>().Init();
            timeSinceDimensionJump = 0f;
        }
    }

    void FixedUpdate() 
	{
		if (!isLocalPlayer) {
			return;
		}

		if (moveConstant < 0) {
			moveConstant = 0;
		}

		if (moveConstant == 0 && hasWon == false) {
			Debug.Log("GameOver");
			Instantiate(winScreen, transform.position, winScreen.rotation);
			hasWon = true;
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
				moveConstant = baseMovementSpeed;
				walk();
			}


			if (Input.GetKey (KeyCode.Space) && time == true) {
				source.Stop ();
				time = false;
				moveConstant = sprintMovementSpeed;
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
			if(statistics.seekers.Count > 0)
			{
				//Debug.Log ("Closest dist is: " + GetClosestSeeker(statistics.seekers));

				if(GetClosestSeeker(statistics.seekers) < 0.4)
				{
					//health -= Time.deltaTime * negativeStamina;
					if(moveConstant >= 0)
					{
						moveConstant -= Time.deltaTime * negativeStamina;
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
}
