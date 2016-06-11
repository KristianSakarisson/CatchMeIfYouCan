using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour
{
    private GameObject[,] rooms;

    public void SetSize(int size)
    {
        rooms = new GameObject[size, size];
    }

    public void AddRoom(GameObject input, int x, int y)
    {
        rooms[x, y] = input;
    }

    public GameObject GetRoom(int x, int y)
    {
        return rooms[x, y];
    }
}
