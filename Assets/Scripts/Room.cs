using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
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
    public GameObject[] sidePrefabs = new GameObject[4];
    public Type type;

    public void GenerateRoom(int x, int y)
    {
        xPos = x;
        yPos = y;
        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                sides[i] = Room.Side.wall;
            }
            else
            {
                sides[i] = Room.Side.door;
            }
        }
    }

    public void BuildRoom()
    {
        for (int i = 0; i < sides.Length; i++)
        {
            Instantiate(door, transform.position, Quaternion.identity);
        }
    }
}
