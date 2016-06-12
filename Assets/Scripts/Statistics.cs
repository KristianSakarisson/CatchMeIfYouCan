using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Statistics : NetworkBehaviour
{
    private Room[,] rooms;
    private int mapSize;
    private Room playerRoom;

    public GameObject player;

    public GameObject[] tiles;
    public GameObject darkRoom;

    [SyncVar]
    public int seed;

    public void SetSize(int size)
    {
        mapSize = size;
        rooms = new Room[size, size];
    }

    public void AddRoom(Room input, int x, int y)
    {
        rooms[x, y] = input;
    }

    public Room GetRoom(int x, int y)
    {
        return rooms[x, y];
    }

    public int GetSize()
    {
        return mapSize;
    }

    public void SetPlayerRoom(Room input)
    {
        playerRoom = input;
    }

    public Room GetPlayerRoom()
    {
        return playerRoom;
    }
}
