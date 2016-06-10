using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveConstant = 70f;

    void Start()
    {

    }

    void FixedUpdate()
    {
        float verticalMove = Input.GetAxis("Vertical") * moveConstant;
        float horizontalMove = Input.GetAxis("Horizontal") * moveConstant;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalMove, verticalMove));
	}
}
