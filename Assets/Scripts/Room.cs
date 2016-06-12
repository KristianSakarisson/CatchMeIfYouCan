using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    private GameObject side; // The "wall" object - contains both a door and a wall

    private Vector3 realPosition; // Actual transform.position of the object

    private Statistics statistics; // Reference to the statistics object

    private Vector3 zAdjust = new Vector3(0f, 0f, 10f); // Adjustment for tiles to make sure that they are behind the player

    public GameObject darkTile;

    // Room coordinates in relation to other rooms
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
    public GameObject[] sidePrefabs = new GameObject[4]; // Array of references to sides
    public Type type; // Type of rooms

    public Room[] GetNeighbors() // Return an array of all neighboring rooms. If a certain neighbor doesn't exist, that index will be null
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

    //Generate random doors and walls in a room, and find the actual in-game position
    public void GenerateRoom(int x, int y)
    {
        statistics = GameObject.Find("Scripts").GetComponent<Statistics>(); // create reference to statistics

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

    // Create the actual room in-game
    public void BuildRoom()
    {
        for (int i = 0; i < sides.Length; i++) // Loop through all room sides
        {
            GameObject newSide;
            Vector3 spawnVector = realPosition;
            Vector3[] spawnOffsets = new Vector3[4] { Vector3.up, Vector3.right, Vector3.down, Vector3.left }; 
            float[] spawnRotations = new float[2] { 0f, 90f };

            spawnVector += spawnOffsets[i] * 1.5f / 2f; // Offset spawn location of side

            newSide = Instantiate(side, spawnVector, Quaternion.identity) as GameObject; // Instantiate side at the spawnVector

            sidePrefabs[i] = newSide; // Store reference to instantiated side

            newSide.transform.Rotate(new Vector3(0, 0, spawnRotations[i % spawnRotations.Length])); // Rotate side if right or left wall
            newSide.transform.parent = transform; // Set actual object as side parent
        }

        BoxCollider2D coll = gameObject.AddComponent<BoxCollider2D>(); // Add box collider with trigger to room
        Vector2 colliderSize = new Vector2(1.4f, 1.4f);
        coll.size = colliderSize;
        coll.offset = realPosition;
        coll.isTrigger = true;

        DrawTile();
    }

    public void DrawSides() // Set wall or door to active and the other to inactive
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

    public void DrawTile() // Draw a random tile at room location
    {
        GameObject newTile = Instantiate(statistics.tiles[Random.Range(0, statistics.tiles.Length)], realPosition + zAdjust, Quaternion.identity) as GameObject;
        darkTile = Instantiate(statistics.darkRoom, realPosition + Vector3.back / 2, Quaternion.identity) as GameObject;

        darkTile.transform.parent = transform;// Set tile parents to room object
        newTile.transform.parent = transform;

        darkTile.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, .05f); // Set slight transparancy on dark tile
        //darkTile.SetActive(false);
    }

    public void SetSide(GameObject input)
    {
        side = input;
    }

    public Vector3 GetPosition()
    {
        return realPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == statistics.player && statistics.playerType == Statistics.PlayerType.seeker)
        {
            statistics.SetPlayerRoom(GetComponent<Room>());
            statistics.AddToVisted(GetComponent<Room>());
            darkTile.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == statistics.player)
        {
			//statistics.AddToVisted (GetComponent<Room>());
        }
    }
}
