using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{
    public int mapSize = 8;
    public GameObject[] roomOutlines;
    public GameObject player;
    private int[] rotations = new int[] { 0, 90, 180, 270 };
    private Statistics statistics;

    public GameObject door;
    public GameObject wall;

    void Awake ()
    {
        statistics = GetComponent<Statistics>();
        statistics.SetSize(mapSize);
        int[] spawnPosition = new int[] {Random.Range(0, mapSize), Random.Range(0, mapSize) };
        GameObject parent = new GameObject("rooms");

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Vector3 spawnVector = new Vector3(transform.position.x + i * 1.48f, transform.position.y + j * 1.48f, transform.position.z);
                /*GameObject newRoom = Instantiate(roomOutlines[Random.Range(0, roomOutlines.Length)], spawnVector, Quaternion.identity) as GameObject;

                newRoom.transform.Rotate(new Vector3(0, 0, rotations[Random.Range(0, rotations.Length)]));

                newRoom.transform.parent = parent.transform;

                statistics.AddRoom(newRoom, i, j);

                if(spawnPosition[0] == i && spawnPosition[1] == j)
                {
                    GameObject playerSpawned = Instantiate(player, spawnVector, Quaternion.identity) as GameObject;

                    playerSpawned.name = "player";
                }*/
                Room newRoom = GenerateRoom();

                for (int k = 0; k < newRoom.sides.Length; k++)
                {
                    if (newRoom.sides[i] == Room.Side.door)
                        Instantiate(door, spawnVector, Quaternion.identity);
                    else
                        Instantiate(wall, spawnVector, Quaternion.identity);
                }
            }
        }
	}

    private Room GenerateRoom()
    {
        Room newRoom = new Room();

        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 1) == 0)
                newRoom.sides[i] = Room.Side.wall;
            else
                newRoom.sides[i] = Room.Side.door;
        }

        return newRoom;
    }
}

public class Room
{
    public GameObject door;
    public GameObject wall;

    public enum Side
    {
        door,
        wall
    };

    public enum Type
    {
        concrete,
        wood,
        tiles
    };

    public Side[] sides = new Side[4];
    public Type type;
}