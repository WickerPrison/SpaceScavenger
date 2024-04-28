using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum Occupant
{
    EMPTY, PLAYER, ENEMY, OBJECT, COVER, WALL, SPACE, DOOR, POSSIBLEDOOR, SALVAGE
}

public enum FOWstate
{
    UNSEEN, DARK, LIT
}

public enum Direction
{
    LEFT, RIGHT, UP, DOWN, NONE
}

public class TileScript : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] SpriteRenderer rightOutline;
    [SerializeField] SpriteRenderer leftOutline;
    [SerializeField] SpriteRenderer topOutline;
    [SerializeField] SpriteRenderer bottomOutline;

    public Vector2Int gridPosition;

    [System.NonSerialized] public FOWstate fowState = FOWstate.UNSEEN;
    public Occupant occupant = Occupant.EMPTY;
    [System.NonSerialized] public GameObject occupantObject;
    [System.NonSerialized] public TileScript previousTile;
    [System.NonSerialized] public int tileDistance;
    [System.NonSerialized] public bool pathedThrough;
    [System.NonSerialized] public bool selectable;
    [System.NonSerialized] public bool highlighted;
    [System.NonSerialized] public IInteractable interactable = null;

    GridScript grid;
    List<TileScript> adjacencyList = new List<TileScript>();
    SpriteRenderer spriteRenderer;
    SpriteMask spriteMask;
    [System.NonSerialized] public Dictionary<Direction, TileScript> adjacentTileDict = new Dictionary<Direction, TileScript>();
    [System.NonSerialized] public int generationID;

    public void TileStart()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteMask = GetComponentInChildren<SpriteMask>();
        grid = GetComponentInParent<GridScript>();
        grid.tileArray[(int)gridPosition.x, (int)gridPosition.y] = this;
    }

    public void SetUpAdjacencyList()
    {
        if (gridPosition.x > 0)
        {
            adjacencyList.Add(grid.tileArray[gridPosition.x - 1, gridPosition.y]);
            adjacentTileDict.Add(Direction.LEFT, grid.tileArray[gridPosition.x - 1, gridPosition.y]);
        }

        if (gridPosition.x + 1 < grid.tileArray.GetLength(0))
        {
            adjacencyList.Add(grid.tileArray[gridPosition.x + 1, gridPosition.y]);
            adjacentTileDict.Add(Direction.RIGHT, grid.tileArray[gridPosition.x + 1, gridPosition.y]);
        }

        if (gridPosition.y > 0)
        {
            adjacencyList.Add(grid.tileArray[gridPosition.x, gridPosition.y - 1]);
            adjacentTileDict.Add(Direction.DOWN, grid.tileArray[gridPosition.x, gridPosition.y - 1]);
        }

        if (gridPosition.y + 1 < grid.tileArray.GetLength(1))
        {
            adjacencyList.Add(grid.tileArray[gridPosition.x, gridPosition.y + 1]);
            adjacentTileDict.Add(Direction.UP, grid.tileArray[gridPosition.x, gridPosition.y + 1]);
        }
    }

    private void LateUpdate()
    {
        if (highlighted)
        {

        }

        switch (fowState)
        {
            case FOWstate.UNSEEN:
                spriteMask.enabled = true;
                if (!highlighted)
                {
                    SetTileColor(Color.black);
                }
                SetTileAlpha(gameSettings.unseenAlpha);
                break;
            case FOWstate.DARK:
                spriteMask.enabled = true;
                SetTileAlpha(0.5f);
                break;
            case FOWstate.LIT:
                spriteMask.enabled = false;
                if(highlighted)
                {
                    SetTileAlpha(0.25f);
                }
                else
                {
                    SetTileAlpha(0);
                }
                break;
        }
    }

    public void ResetTile()
    {
        SetTileColor(new Color(0,0,0,0));
        SetBorderColor(Color.black);
        pathedThrough = false;
        selectable = false;
        highlighted = false;
        tileDistance = 0;
        previousTile = null;
        ClearOutlines();
    }

    public void SetTileColor(Color color)
    {
        spriteRenderer.material.SetColor("_TileColor", color);
        spriteRenderer.material.SetFloat("_Alpha", color.a);
    }

    void SetTileAlpha(float alpha)
    {
        spriteRenderer.material.SetFloat("_Alpha", alpha);
    }

    public void SetBorderColor(Color color)
    {
        spriteRenderer.material.SetColor("_BorderColor", color);
    }

    public List<TileScript> GetAdjacencyList()
    {
        return adjacencyList;
    }

    public void SetupOutline(AbilityProfile abilityProfile) 
    {
        ClearOutlines();

        if (!adjacentTileDict.ContainsKey(Direction.RIGHT) || !adjacentTileDict[Direction.RIGHT].pathedThrough)
        {
            rightOutline.enabled = true;
            rightOutline.color = abilityProfile.outlineColor;
        }

        if (!adjacentTileDict.ContainsKey(Direction.DOWN) || !adjacentTileDict[Direction.DOWN].pathedThrough)
        {
            bottomOutline.enabled = true;
            bottomOutline.color = abilityProfile.outlineColor;
        }

        if (!adjacentTileDict.ContainsKey(Direction.LEFT) || !adjacentTileDict[Direction.LEFT].pathedThrough)
        {
            leftOutline.enabled = true;
            leftOutline.color = abilityProfile.outlineColor;
        }

        if (!adjacentTileDict.ContainsKey(Direction.UP) || !adjacentTileDict[Direction.UP].pathedThrough)
        {
            topOutline.enabled = true;
            topOutline.color = abilityProfile.outlineColor;
        }
    }

    void ClearOutlines()
    {
        rightOutline.enabled = false;
        leftOutline.enabled = false;
        bottomOutline.enabled = false;
        topOutline.enabled = false;
    }

    public void TileGeneration()
    {
        switch (generationID)
        {
            case 0:
                occupant = Occupant.SPACE; break;
            case 1:
                occupant = Occupant.EMPTY;
                occupantObject = null;
                break;
            case 2:
                occupant = Occupant.WALL; break;
        }
    }
}
