using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statistics : MonoBehaviour
{
    private Room[,] rooms;
    private int mapSize;
    private Room playerRoom;

    public GameObject player;

    public GameObject[] tiles;

	public int visitedLimit = 5;
	public ArrayList visitedTiles = new ArrayList();

    public GameObject darkRoom;

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

		if (visitedTiles.Count >= visitedLimit) {

			Room what = (Room)visitedTiles [0];
			Debug.Log ("Removing index: " + visitedTiles.IndexOf (what));

			what.darkTile.SetActive(true);
			if(!GetPlayerRoom() == what)
				visitedTiles.RemoveAt (0);

		}

		visitedTiles.Add (tile);
		Debug.Log (visitedTiles.Count);
	}
}