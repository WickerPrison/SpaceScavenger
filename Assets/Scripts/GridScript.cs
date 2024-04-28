using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public TileScript[,] tileArray;
    public int rows;
    public int columns;

    private void Awake()
    {
        //tileArray = new TileScript[rows, columns];
    }

    public void ResetTiles()
    {
        foreach(TileScript tile in tileArray)
        {
            tile.ResetTile();
        }
    }

    // This update is set to always run before all other updates
    private void Update()
    {
        foreach(TileScript tile in tileArray)
        {
            if(tile.fowState != FOWstate.UNSEEN)
            {
                tile.fowState = FOWstate.DARK;
            }
        }
    }
}
