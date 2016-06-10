using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    float moveConstant = .2f;

	void Start ()
    {
	
	}
	
	void FixedUpdate ()
    {
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        transform.Translate(horizontalMove, verticalMove, 0);
	}
}
