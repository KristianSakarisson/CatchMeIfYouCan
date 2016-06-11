using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 differenceZaxis;

	public float zoomSizeMin = 1;
	public float zoomSizeMax = 4;

	public float duration = 3.0f;
	private float elapsed = 0.0f;

	protected float zoom = 0.1f;

    void Start()
    {
        player = GameObject.Find("player");
        differenceZaxis = new Vector3(0, 0, Camera.main.transform.position.z - player.transform.position.z);
    }
	
	void Update ()
    { 
		if (Input.GetAxis("Mouse ScrollWheel") > 0) { //Zooma út
			zoomOut();
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) { //Zooma inn
			zoomIn();
		}
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoomSizeMin, zoomSizeMax );
		Camera.main.transform.position = player.transform.position + differenceZaxis;
	}

    public void SetPlayer(GameObject input)
    {
        player = input;
    }

	public void zoomOut() 
	{
		Debug.Log("Zooming out");
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(Camera.main.orthographicSize+zoom, zoomSizeMin, zoomSizeMax), duration);
	}
	public void zoomIn() 
	{
		Debug.Log("Zooming in");
		Camera.main.orthographicSize--;
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(Camera.main.orthographicSize-zoom, zoomSizeMin, zoomSizeMax), duration);
	}
	public void canZoom() 
	{
		Debug.Log(Camera.main.orthographic);
	}
}
