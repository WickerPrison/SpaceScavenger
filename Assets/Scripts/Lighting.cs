using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lighting : MonoBehaviour
{
    PlayerUnit unit;
    Pathfinding pathfinding;
    LayerMask tileMask;
    Vector2 lastConeDirection = new Vector2(1,1);
    Occupant[] blockLightOccupants = { Occupant.WALL, Occupant.DOOR };

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<PlayerUnit>();
        pathfinding = GetComponent<Pathfinding>();
        tileMask = LayerMask.GetMask("Tiles");
    }

    // Update is called once per frame
    void Update()
    {
        unit.currentTile = pathfinding.GetTileAtPosition(transform.position);
        unit.currentTile.fowState = FOWstate.LIT;
        foreach (TileScript adjacentTile in unit.currentTile.GetAdjacencyList())
        {
            adjacentTile.fowState = FOWstate.LIT;
        }

        if (unit.currentTile.adjacentTileDict.ContainsKey(Direction.RIGHT))
        {
            LightTopAndBottom(unit.currentTile.adjacentTileDict[Direction.RIGHT]);
        }

        if (unit.currentTile.adjacentTileDict.ContainsKey(Direction.LEFT))
        {
            LightTopAndBottom(unit.currentTile.adjacentTileDict[Direction.LEFT]);
        }

        Light(unit.lightProfile);
    }

    void LightTopAndBottom(TileScript tile)
    {
        if(tile.adjacentTileDict.ContainsKey(Direction.DOWN))
        {
            tile.adjacentTileDict[Direction.DOWN].fowState = FOWstate.LIT;
        }

        if (tile.adjacentTileDict.ContainsKey(Direction.UP))
        {
            tile.adjacentTileDict[Direction.UP].fowState = FOWstate.LIT;
        }
    }

    void Light(LightProfile profile)
    {
        switch (profile.type)
        {
            case LightType.CONE:
                Flashlight(profile);
                break;
        }
    }

    void Flashlight(LightProfile profile)
    {
        if(unit.state != PlayerUnitState.IDLE)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            lastConeDirection = mousePos - (Vector2)transform.position;
            lastConeDirection = lastConeDirection.normalized;
        }

        for(int i = 0; i < profile.angle; i+= 5)
        {
            float angle = i - profile.angle / 2;
            Vector2 direction = RotateDirection(lastConeDirection, angle);
            LightRay(profile, direction);
        }
    }

    void LightRay(LightProfile profile, Vector2 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, profile.range, tileMask);

        foreach (RaycastHit2D hit in hits)
        {
            TileScript tile = hit.collider.gameObject.GetComponent<TileScript>();
            tile.fowState = FOWstate.LIT;
            if(blockLightOccupants.Contains(tile.occupant))
            {
                return;
            }
        }
    }

    Vector2 RotateDirection(Vector2 oldDirection, float degrees)
    {
        Vector2 newDirection;
        newDirection.x = Mathf.Cos(degrees * Mathf.Deg2Rad) * oldDirection.x - Mathf.Sin(degrees * Mathf.Deg2Rad) * oldDirection.y;
        newDirection.y = Mathf.Sin(degrees * Mathf.Deg2Rad) * oldDirection.x + Mathf.Cos(degrees * Mathf.Deg2Rad) * oldDirection.y;
        return newDirection;
    }
}
