using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 differenceZaxis;

	public Camera cam = null;

	public float currentOrtho; // Value of most current Camera.main.orthographicSize

	public float zoom	 	= 1; // Zoom speed
	public float smooth 	= 1.0f; // Smoothness of zooming
	public float minOrtho 	= 1.0f; // Minimum of zoom
	public float maxOrtho 	= 6.0f; // Maximum of zoom

    void Start()
    {
		currentOrtho = cam.orthographicSize;

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
			cam.transform.position = player.transform.position + Vector3.back;
		}
	}

    /*public void SetPlayer(GameObject input)
    {
        player = input;
    }*/

	public void ZoomView()
	{
		cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, currentOrtho, smooth * Time.deltaTime);
	}
		
}
