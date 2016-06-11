using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{
    public int mapSize = 8;
    public GameObject player;
    private Statistics statistics;

    public GameObject door;
    public GameObject wall;

    void Awake()
    {
        statistics = GetComponent<Statistics>();
        statistics.SetSize(mapSize);
        int[] playerSpawnPosition = new int[] { Random.Range(0, mapSize), Random.Range(0, mapSize) };
        GameObject parent = new GameObject("rooms");

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                GameObject newRoom = new GameObject("room");
                newRoom.AddComponent<Room>();
                Room room = newRoom.GetComponent<Room>();
                room.GenerateRoom(i, j);
                room.SetDoor(door);
                room.SetWall(wall);
                room.BuildRoom();

                newRoom.transform.parent = parent.transform;

                if (playerSpawnPosition[0] == i && playerSpawnPosition[1] == j)
                {
                    GameObject playerSpawned = Instantiate(player, room.GetPosition(), Quaternion.identity) as GameObject;

                    playerSpawned.name = "player";
                }
            }
        }
    }
}