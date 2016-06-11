using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Initialize : NetworkBehaviour
{
    public int mapSize = 8;
    public GameObject[] roomOutlines;
    public GameObject player;
    private int[] rotations = new int[] { 0, 90, 180, 270 };
    private Statistics statistics;

	[SyncVar]
	private int seed;

    public GameObject door;
    public GameObject wall;

    void Awake()
    {
		if (isServer) {
			seed = Random.Range (0, 1000000);
			Debug.Log ("Server seed, " + seed);
		} else {
			Debug.Log (seed);
		}
		Random.seed = seed;
		statistics = GetComponent<Statistics>();
        statistics.SetSize(mapSize);
        int[] spawnPosition = new int[] { Random.Range(0, mapSize), Random.Range(0, mapSize) };
        GameObject parent = new GameObject("rooms");

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                //Room newRoom = new Room();
                //newRoom.GenerateRoom(i, j);
                //newRoom.door = door;
                //newRoom.wall = wall;
                //newRoom.BuildRoom();

                Vector3 roomVector = new Vector3(transform.position.x + i * 1.48f, transform.position.y + j * 1.48f, transform.position.z);

                Room newRoom = new Room();
                newRoom.GenerateRoom(i, j);
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

                    newRoom.sidePrefabs[k] = newSide;

                    newSide.transform.Rotate(new Vector3(0, 0, spawnRotations[k % spawnRotations.Length]));
                    newSide.transform.parent = thisRoom.transform;
                }

                newRoom.xPos = i;
                newRoom.yPos = j;

                statistics.AddRoom(newRoom, i, j);
            }
        }
    }

	void Update() {

	}

	public override void OnStartServer() {
		Debug.LogError ("Server start!");
	}
	public override void OnStartClient() {
		Debug.LogError ("Client start!");
	}
	public override void OnStartLocalPlayer() {
		Debug.LogError ("LocalPlayer start!");
	}
}