using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float moveConstant;
	public Animator animator;

	public AudioClip walking, running, doorOpen, doorClose;
	private AudioSource source;

	private float stanimaRegenTime = 10;
	private float time = 0;

	void Start ()
	{
		animator = gameObject.GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

    void FixedUpdate() 
	{
		if (!isLocalPlayer) {
			return;
		}

		time += Time.deltaTime;

		Debug.Log (time);

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
			if (Input.GetKey (KeyCode.Space) && time >= stanimaRegenTime) {
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
