using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Statistics : NetworkBehaviour
{
    public enum PlayerType
    {
        hider,
        seeker
    }

    private Room[,] rooms;
    private int mapSize;
    private Room playerRoom;

    public PlayerType playerType = PlayerType.seeker;

    public GameObject player;

    public GameObject[] tiles;

	public int visitedLimit = 5;
    public Room[] visitedRooms;

    public GameObject darkRoom;

	public int seed;

	public List<Transform> seekers = new List<Transform> ();
	
    void Start()
    {
        visitedRooms = new Room[visitedLimit];
        //Random.seed = seed;
    }

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

	public void AddToVisted(Room tile)
	{
        for (int i = visitedRooms.Length - 1; i > 0; i--)
        {
            if (visitedRooms[visitedRooms.Length - 1] != null)
                visitedRooms[visitedRooms.Length - 1].darkTile.SetActive(true);
            visitedRooms[i] = visitedRooms[i - 1];
        }
        visitedRooms[0] = tile;

        for (int i = 0; i < visitedRooms.Length; i++)
        {
            if (visitedRooms[i] == null)
                break;

            visitedRooms[i].darkTile.SetActive(false);
        }
	}
}