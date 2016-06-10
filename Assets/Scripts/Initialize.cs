using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{
    public int mapSize = 8;
    public GameObject[] roomOutlines;
    public GameObject player;
    private int[] rotations = new int[] { 0, 90, 180, 270 };

	void Awake ()
    {
        int[] spawnPosition = new int[] {Random.Range(0, mapSize), Random.Range(0, mapSize) };
        GameObject parent = new GameObject("rooms");
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Vector3 spawnVector = new Vector3(transform.position.x + i * 1.48f, transform.position.y + j * 1.48f, transform.position.z);
                GameObject newRoom = Instantiate(roomOutlines[Random.Range(0, roomOutlines.Length)], spawnVector, Quaternion.identity) as GameObject;

                newRoom.transform.Rotate(new Vector3(0, 0, rotations[Random.Range(0, rotations.Length)]));

                newRoom.transform.parent = parent.transform;

                if(spawnPosition[0] == i && spawnPosition[1] == j)
                {
                    GameObject playerSpawned = Instantiate(player, spawnVector, Quaternion.identity) as GameObject;

                    playerSpawned.name = "player";
                }
            }
        }
	}
}
