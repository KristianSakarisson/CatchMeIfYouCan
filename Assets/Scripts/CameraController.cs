using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    Vector3 differenceZaxis;

    void Start()
    {
        differenceZaxis = new Vector3(0, 0, Camera.main.transform.position.z - player.transform.position.z);
    }
	
	void Update ()
    {
        Camera.main.transform.position = player.transform.position + differenceZaxis;
	}

    public void SetPlayer(GameObject input)
    {
        player = input;
    }
}
