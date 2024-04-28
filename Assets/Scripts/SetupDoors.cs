using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupDoors : MonoBehaviour
{
    [SerializeField] int minDoorDistance;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] AbilityProfile doorGeneration;
    GridScript grid;
    List<TileScript> doors = new List<TileScript>();

    private void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        PlaceDoors();
        RemoveDoors();
    }

    public void PlaceDoors()
    { 
        foreach(TileScript tile in grid.tileArray)
        {
            if(tile.generationID == 3)
            {
                bool hasAdjacentDoor = false;
                foreach (TileScript adjacentTile in tile.GetAdjacencyList())
                {
                    if (adjacentTile.generationID == 3) { hasAdjacentDoor = true; break; }
                }

                if (hasAdjacentDoor)
                {
                    tile.occupant = Occupant.POSSIBLEDOOR;
                    GameObject door = Instantiate(doorPrefab);
                    tile.occupantObject = door;
                    door.transform.position = tile.transform.position;
                    doors.Add(tile);
                }
                else
                {
                    tile.generationID = 2;
                    tile.TileGeneration();
                }
            }
        }
    }

    void RemoveDoors()
    {
        while( doors.Count > 0 )
        {
            int randInt = Random.Range(0, doors.Count);
            TileScript currentDoor = doors[randInt];
            TileScript otherDoor = null;
            foreach(TileScript tile in currentDoor.GetAdjacencyList())
            {
                if(tile.occupant == Occupant.POSSIBLEDOOR)
                {
                    otherDoor = tile;
                    break;
                }
            }
            Direction dirFromOtherDoor = TileMethods.GetDirectionBetweenAdjacentTiles(otherDoor, currentDoor);
            TileScript startingTile = currentDoor.adjacentTileDict[dirFromOtherDoor];
            Direction dirToOtherDoor = TileMethods.GetDirectionBetweenAdjacentTiles(currentDoor, otherDoor);
            TileScript targetTile = otherDoor.adjacentTileDict[dirToOtherDoor];

            int distance = TileMethods.GetTileDistance(startingTile, targetTile, grid, doorGeneration);

            if(distance < minDoorDistance && distance != -1)
            {
                Destroy(currentDoor.occupantObject);
                currentDoor.occupant = Occupant.WALL;
                Destroy(otherDoor.occupantObject);
                otherDoor.occupant = Occupant.WALL;
            }
            else
            {
                currentDoor.occupant = Occupant.DOOR;
                otherDoor.occupant = Occupant.DOOR;
                TurnDoors(currentDoor, otherDoor, dirToOtherDoor);
            }

            doors.Remove(currentDoor);
            doors.Remove(otherDoor);
        }
    }

    void TurnDoors(TileScript door1, TileScript door2, Direction direction = Direction.NONE)
    {
        if(direction == Direction.NONE)
        {
            direction = TileMethods.GetDirectionBetweenAdjacentTiles(door1, door2);
        }

        Door door1Object = door1.occupantObject.GetComponent<Door>();
        Door door2Object = door2.occupantObject.GetComponent<Door>();

        door1Object.partnerDoor = door2Object;
        door2Object.partnerDoor = door1Object;

        switch(direction)
        {
            case Direction.LEFT:
                door1Object.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                door2Object.transform.rotation = Quaternion.Euler(-Vector3.forward * 90);
                break;
            case Direction.RIGHT:
                door1Object.transform.rotation = Quaternion.Euler(-Vector3.forward * 90);
                door2Object.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                break;
            case Direction.UP:
                door2Object.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                break;
            case Direction.DOWN:
                door1Object.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                break;
        }
    }
}
