using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    private GameObject side;

    private Vector3 realPosition;

    private Statistics statistics;

    private Vector3 zAdjust = new Vector3(0f, 0f, 10f);

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

    public Room[] GetNeighbors()
    {
        Room[] neighbors = new Room[4];

        if (xPos != statistics.GetSize() - 1)
            neighbors[0] = statistics.GetRoom(xPos + 1, yPos);
        if (yPos != statistics.GetSize() - 1)
            neighbors[1] = statistics.GetRoom(xPos, yPos + 1);
        if (xPos != 0)
            neighbors[2] = statistics.GetRoom(xPos - 1, yPos);
        if (yPos != 0)
            neighbors[3] = statistics.GetRoom(xPos, yPos - 1);

        return neighbors;
    }

    public void GenerateRoom(int x, int y)
    {
        statistics = GameObject.Find("Scripts").GetComponent<Statistics>();

        xPos = x;
        yPos = y;

        realPosition = new Vector3(xPos, yPos, 0f) * 1.48f;

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
            GameObject newSide;
            Vector3 spawnVector = realPosition;
            Vector3[] spawnOffsets = new Vector3[4] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
            float[] spawnRotations = new float[2] { 0f, 90f };

            spawnVector += spawnOffsets[i] * 1.5f / 2f;

            newSide = Instantiate(side, spawnVector, Quaternion.identity) as GameObject;

            sidePrefabs[i] = newSide;

            newSide.transform.Rotate(new Vector3(0, 0, spawnRotations[i % spawnRotations.Length]));
            newSide.transform.parent = transform;
        }

        DrawSides();
        DrawTile();
    }

    public void DrawSides()
    {
        for (int i = 0; i < sidePrefabs.Length; i++)
        {
            if (sides[i] == Side.door)
            {
                sidePrefabs[i].transform.GetChild(0).gameObject.SetActive(true);
                sidePrefabs[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                sidePrefabs[i].transform.GetChild(0).gameObject.SetActive(false);
                sidePrefabs[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void DrawTile()
    {
        GameObject newTile = Instantiate(statistics.tiles[Random.Range(0, statistics.tiles.Length)], realPosition + zAdjust, Quaternion.identity) as GameObject;

        newTile.transform.parent = transform;
    }

    public void SetSide(GameObject input)
    {
        side = input;
    }

    public Vector3 GetPosition()
    {
        return realPosition;
    }
}
