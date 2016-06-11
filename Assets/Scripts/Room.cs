using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    private GameObject door;
    private GameObject wall;

    private Vector3 realPosition;

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

            if (sides[i] == Side.door)
                newSide = Instantiate(door, spawnVector, Quaternion.identity) as GameObject;
            else
                newSide = Instantiate(wall, spawnVector, Quaternion.identity) as GameObject;

            sidePrefabs[i] = newSide;

            newSide.transform.Rotate(new Vector3(0, 0, spawnRotations[i % spawnRotations.Length]));
            newSide.transform.parent = transform;
        }
    }

    public void SetDoor(GameObject input)
    {
        door = input;
    }
    
    public void SetWall(GameObject input)
    {
        wall = input;
    }

    public Vector3 GetPosition()
    {
        return realPosition;
    }
}
