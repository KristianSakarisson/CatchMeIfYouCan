using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 differenceZaxis;

	public float currentOrtho; // Value of most current Camera.main.orthographicSize

	public float zoom	 	= 1; // Zoom speed
	public float smooth 	= 1.0f; // Smoothness of zooming
	public float minOrtho 	= 1.0f; // Minimum of zoom
	public float maxOrtho 	= 6.0f; // Maximum of zoom

    void Start()
    {
		currentOrtho = Camera.main.orthographicSize;
        //differenceZaxis = new Vector3(0, 0, Camera.main.transform.position.z - player.transform.position.z);
    }
	
	void Update ()
    { 
		if (player)
		{
			float scroll = Input.GetAxis ("Mouse ScrollWheel");
			if (scroll != 0.0f) {
				currentOrtho -= scroll * zoom;
				currentOrtho = Mathf.Clamp (currentOrtho, minOrtho, maxOrtho);
			}

			ZoomView ();
			Camera.main.transform.position = player.transform.position + differenceZaxis;
		}
	}

    public void SetPlayer(GameObject input)
    {
        player = input;
    }

	public void ZoomView()
	{
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, currentOrtho, smooth * Time.deltaTime);
	}
		
}
