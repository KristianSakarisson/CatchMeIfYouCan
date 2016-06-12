using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 differenceZaxis;

	public Camera cam = null;

	public float currentOrtho; // Value of most current Camera.main.orthographicSize

    private Statistics statistics;

	public float zoom	 	= 1; // Zoom speed
	public float smooth 	= 1.0f; // Smoothness of zooming
	public float minOrtho 	= 1.0f; // Minimum of zoom
	public float maxOrtho 	= 6.0f; // Maximum of zoom

    void Start()
    {
		currentOrtho = cam.orthographicSize;

        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

        DrawMinimap();

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

    public void DrawMinimap()
    {
        Vector3 offsetVector = new Vector3(.04f, .04f, 0f);
        for (int x = 0; x < statistics.GetSize(); x++)
        {
            for (int y = 0; y < statistics.GetSize(); y++)
            {
                Room UIRoom = statistics.GetRoom(x, y);
                Vector3 actualOffset = new Vector3(x * offsetVector.x, y * offsetVector.y, 0f);
                for (int i = 0; i < UIRoom.sides.Length; i++)
                {
                    if (UIRoom.sides[i] == Room.Side.wall)
                    {
                        GameObject newObject = Instantiate(statistics.UIWall, transform.position + actualOffset + Vector3.forward, Quaternion.identity) as GameObject;

                        newObject.transform.parent = transform;

                        newObject.transform.Rotate(new Vector3(0f, 0f, 90f * -i));
                    }
                }
            }
        }
    }	
}
