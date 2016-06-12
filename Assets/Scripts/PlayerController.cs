using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public float moveConstant;

	void FixedUpdate() 
	{
		if (transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer == false) {
			return;
		}
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, verticalMove);

        if(verticalMove + horizontalMove > 0f || verticalMove + horizontalMove < 0f)
            transform.rotation = Quaternion.FromToRotation(Vector2.right, new Vector2(horizontalMove, verticalMove));
    }
}
