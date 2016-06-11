using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveConstant;

	void FixedUpdate() 
	{
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, verticalMove);
    }
}
