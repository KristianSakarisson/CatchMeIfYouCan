using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 differenceZaxis;

	public float zoomSizeMin = 1;
	public float zoomSizeMax = 4;

	public float duration = 2.0f;
	public float zoom = 0.8f;

    void Start()
    {
        player = GameObject.Find("player");
        differenceZaxis = new Vector3(0, 0, Camera.main.transform.position.z - player.transform.position.z);
    }
	
	void Update ()
    { 
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			zoomOut();
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoomIn();
		}

		Camera.main.transform.position = player.transform.position + differenceZaxis;
	}

    public void SetPlayer(GameObject input)
    {
        player = input;
    }

	public void zoomOut() 
	{
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(Camera.main.orthographicSize+zoom, zoomSizeMin, zoomSizeMax), Time.deltaTime / duration);
	}
	public void zoomIn() 
	{
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(Camera.main.orthographicSize-zoom, zoomSizeMin, zoomSizeMax), Time.deltaTime / duration);
	}
	public void canZoom() 
	{
		Debug.Log(Camera.main.orthographic);
	}
}
