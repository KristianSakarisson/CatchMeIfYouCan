using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour
{
    private Room[,] rooms;
    private int mapSize;
    private Room playerRoom;

    public GameObject player;

    public GameObject[] tiles;

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
