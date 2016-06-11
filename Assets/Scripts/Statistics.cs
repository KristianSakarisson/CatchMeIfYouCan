using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour
{
    private Room[,] rooms;

    public void SetSize(int size)
    {
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
}
