using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float moveConstant;
	public Animator animator;

	public AudioClip walking, running;

	private AudioSource source;


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
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, verticalMove);

        if (verticalMove + horizontalMove > 0f || verticalMove + horizontalMove < 0f) {
			transform.rotation = Quaternion.FromToRotation (Vector2.right, new Vector2 (horizontalMove, verticalMove));

			if (!source.isPlaying) {
				source.PlayOneShot (walking, 1F);
			}

			moveConstant = 2f;

			if (Input.GetKeyDown (KeyCode.Space)) {
				source.Stop ();
			}
			if (Input.GetKey (KeyCode.Space)) {
				moveConstant = 6f;
				if (source.isPlaying)
					return;
				source.Play (running, 1F);
			}

			animator.SetBool ("isWalking", true);
		} 
		else 
		{
			source.Stop ();
			animator.SetBool("isWalking", false);
		}
    }
}
