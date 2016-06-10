using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{
    public GameObject[] roomOutlines;
    private int[] rotations = new int[] { 0, 90, 180, 270 };

	void Start ()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 spawnVector = new Vector3(transform.position.x + i * 1.48f, transform.position.y + j * 1.48f, transform.position.z);
                GameObject newRoom = Instantiate(roomOutlines[Random.Range(0, roomOutlines.Length)], spawnVector, Quaternion.identity) as GameObject;

                newRoom.transform.Rotate(new Vector3(0, 0, rotations[Random.Range(0, rotations.Length)]));
            }
        }
	}
}
