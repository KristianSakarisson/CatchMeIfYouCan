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
                Vector3 roomVector = new Vector3(transform.position.x + i * 1.48f, transform.position.y + j * 1.48f, transform.position.z);

                Room newRoom = GenerateRoom();
                GameObject thisRoom = new GameObject("room " + i + ", " + j);
                thisRoom.transform.parent = parent.transform;

                for (int k = 0; k < newRoom.sides.Length; k++)
                {
                    GameObject newSide;
                    Vector3 spawnVector = roomVector;
                    Vector3[] spawnOffsets = new Vector3[4] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
                    float[] spawnRotations = new float[2] { 0f, 90f };

                    spawnVector += spawnOffsets[k] * 1.5f / 2f;
  
                    if (newRoom.sides[k] == Room.Side.door)
                        newSide = Instantiate(door, spawnVector, Quaternion.identity) as GameObject;
                    else
                        newSide = Instantiate(wall, spawnVector, Quaternion.identity) as GameObject;

                    newSide.transform.Rotate(new Vector3(0, 0, spawnRotations[k % spawnRotations.Length]));
                    newSide.transform.parent = thisRoom.transform;
                }

                newRoom.xPos = i;
                newRoom.yPos = j;

                statistics.AddRoom(newRoom, i, j);

                if (spawnPosition[0] == i && spawnPosition[1] == j)
                {
                    GameObject playerSpawned = Instantiate(player, roomVector, Quaternion.identity) as GameObject;

                    playerSpawned.name = "player";
                }
            }
        }
	}

    private Room GenerateRoom()
    {
        Room newRoom = new Room();

        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                newRoom.sides[i] = Room.Side.wall;
            }
            else
            {
                newRoom.sides[i] = Room.Side.door;
            }
        }

        return newRoom;
    }
}

public class Room
{
    public GameObject door;
    public GameObject wall;

    public int xPos;
    public int yPos;

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