using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileMethods
{
    static Direction[] directionArray = {Direction.LEFT, Direction.RIGHT, Direction.UP, Direction.DOWN };


    public static TileScript GetTileAtPosition(Vector2 position)
    {
        LayerMask tileMask = LayerMask.GetMask("Tiles");
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, 0.5f, tileMask);

        if (hit.collider != null)
        {
            TileScript tile = hit.transform.gameObject.GetComponent<TileScript>();
            return tile;
        }
        return null;
    }

    public static Direction GetDirectionBetweenAdjacentTiles(TileScript fromTile, TileScript toTile)
    {
        Direction nullDirection = Direction.NONE;
        for(int i =  0; i < directionArray.Length; i++)
        {
            if (fromTile.adjacentTileDict.ContainsKey(directionArray[i]))
            {
                if (fromTile.adjacentTileDict[directionArray[i]] == toTile)
                {
                    return directionArray[i];
                }
            }
        }
        return nullDirection;
    }

    public static List<TileScript> Get8AjacentTiles(TileScript centerTile)
    {
        List<TileScript> outputList = new List<TileScript>();
        TileScript rightTile = GetAdjacentTileFromDirection(centerTile, Direction.RIGHT);
        TileScript nextTile;
        if(rightTile != null)
        {
            outputList.Add(rightTile);

            nextTile = GetAdjacentTileFromDirection(rightTile, Direction.UP);
            if (nextTile != null) outputList.Add(nextTile);

            nextTile = GetAdjacentTileFromDirection(rightTile, Direction.DOWN);
            if (nextTile != null) outputList.Add(nextTile);
        }

        TileScript leftTile = GetAdjacentTileFromDirection(centerTile, Direction.LEFT);
        if (leftTile != null)
        {
            outputList.Add(leftTile);

            nextTile = GetAdjacentTileFromDirection(leftTile, Direction.UP);
            if (nextTile != null) outputList.Add(nextTile);

            nextTile = GetAdjacentTileFromDirection(leftTile, Direction.DOWN);
            if (nextTile != null) outputList.Add(nextTile);
        }

        nextTile = GetAdjacentTileFromDirection(centerTile, Direction.UP);
        if (nextTile != null) outputList.Add(nextTile);

        nextTile = GetAdjacentTileFromDirection(centerTile, Direction.DOWN);
        if (nextTile != null) outputList.Add(nextTile);

        return outputList;
    }

    public static TileScript GetAdjacentTileFromDirection(TileScript startTile, Direction direction)
    {
        if (startTile.adjacentTileDict.ContainsKey(direction))
        {
            return startTile.adjacentTileDict[direction];
        }
        else return null;
    }

    public static int GetTileDistance(TileScript startTile, TileScript endTile, GridScript grid, AbilityProfile abilityProfile)
    {
        grid.ResetTiles();

        startTile.tileDistance = 0;

        Queue<TileScript> searching = new Queue<TileScript>();

        searching.Enqueue(startTile);
        startTile.pathedThrough = true;
        startTile.previousTile = null;

        while (searching.Count > 0)
        {
            TileScript tile = searching.Dequeue();

            foreach (TileScript adjacentTile in tile.GetAdjacencyList())
            {
                if(adjacentTile == endTile)
                {
                    return tile.tileDistance + 1;
                }

                if (ShouldAddNextTile(tile, adjacentTile, abilityProfile))
                {
                    adjacentTile.previousTile = tile;
                    adjacentTile.pathedThrough = true;
                    adjacentTile.tileDistance = tile.tileDistance + 1;
                    searching.Enqueue(adjacentTile);
                }
            }
        }

        return -1;
    }

    static bool ShouldAddNextTile(TileScript tile, TileScript adjacentTile, AbilityProfile profile)
    {
        if (!profile.allTilesPathable && !profile.pathableTiles.Contains(adjacentTile.occupant)) return false;

        if (adjacentTile.pathedThrough) return false;

        int tileDistance = tile.tileDistance + 1;
        if (tileDistance > profile.range) return false;

        return true;
    }
}
