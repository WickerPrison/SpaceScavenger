using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour
{
    [System.NonSerialized] public List<TileScript> selectableTiles = new List<TileScript>();
    List<TileScript> pathedTiles = new List<TileScript>();
    [System.NonSerialized] public List<Vector3> outlinePoints = new List<Vector3>();
    LayerMask tileMask;
    GridScript grid;

    public void Start()
    {
        tileMask = LayerMask.GetMask("Tiles");
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
    }

    public TileScript GetTileAtPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, 0.5f, tileMask);

        if (hit.collider != null)
        {
            TileScript tile = hit.transform.gameObject.GetComponent<TileScript>();
            return tile;
        }
        return null;
    }

    public void Pathfinder(TileScript startingTile, AbilityProfile abilityProfile)
    {
        grid.ResetTiles();

        startingTile.tileDistance = 0;

        Queue<TileScript> searching = new Queue<TileScript>();

        searching.Enqueue(startingTile);
        startingTile.pathedThrough = true;
        startingTile.previousTile = null;

        selectableTiles.Clear();
        pathedTiles.Clear();
        pathedTiles.Add(startingTile);

        while(searching.Count > 0)
        {
            TileScript tile = searching.Dequeue();

            foreach(TileScript adjacentTile in tile.GetAdjacencyList())
            {
                if(ShouldAddNextTile(tile, adjacentTile, abilityProfile))
                {
                    adjacentTile.previousTile = tile;
                    adjacentTile.pathedThrough = true;
                    pathedTiles.Add(adjacentTile);
                    adjacentTile.tileDistance = tile.tileDistance + 1;
                    searching.Enqueue(adjacentTile);
                    if (CanSelectTile(adjacentTile, abilityProfile))
                    {
                        adjacentTile.selectable = true;
                        selectableTiles.Add(adjacentTile);
                    }
                }
            }
        }
        SetupOutline(abilityProfile);
        SelectionColor(abilityProfile);
     }

    bool ShouldAddNextTile(TileScript tile, TileScript adjacentTile, AbilityProfile profile)
    {
        if (!profile.allTilesPathable && !profile.pathableTiles.Contains(adjacentTile.occupant)) return false;

        if (adjacentTile.pathedThrough) return false;

        int tileDistance = tile.tileDistance + 1;
        if(tileDistance > profile.range) return false;

        return true;
    }

    bool CanSelectTile(TileScript adjacentTile, AbilityProfile profile)
    {
        if (!profile.selectableTiles.Contains(adjacentTile.occupant)) return false;

        if (!profile.allFowStatesSelectable && !profile.selectableFowStates.Contains(adjacentTile.fowState)) return false;

        return true;
    }

    public int GetDistance(TileScript tile1, TileScript tile2)
    {
        return Mathf.Abs(tile1.gridPosition.x - tile2.gridPosition.x) + Mathf.Abs(tile1.gridPosition.y - tile2.gridPosition.y);
    }

    void SetupOutline(AbilityProfile abilityProfile)
    {
        foreach(TileScript tile in pathedTiles)
        {
            tile.SetupOutline(abilityProfile);
        }
    }

    void SelectionColor(AbilityProfile abilityProfile)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10, tileMask);
        if (hit.collider != null)
        {
            TileScript tileScript = hit.transform.gameObject.GetComponent<TileScript>();
            if (tileScript.selectable)
            {
                tileScript.highlighted = true;
                tileScript.SetTileColor(abilityProfile.selectableColor);
            }
        }
    }

    public void ResetTiles()
    {
        foreach(TileScript tile in grid.tileArray)
        {
            tile.ResetTile();
        }
    }
}
