using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateGrid : EditorWindow
{
    GameObject tilePrefab;
    GameObject grid;
    TextAsset levelCSV;
    int rowsNum;
    int columnsNum;
    int[,] gridArray;


    [MenuItem("Tools/Create Grid")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateGrid));
    }

    private void OnGUI()
    {
        tilePrefab = EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false) as GameObject;
        levelCSV = EditorGUILayout.ObjectField("Level CSV", levelCSV, typeof(TextAsset), false) as TextAsset;
        rowsNum = EditorGUILayout.IntField("Number of Rows", rowsNum);
        columnsNum = EditorGUILayout.IntField("Number of Columns", columnsNum);

        if(GUILayout.Button("Create Grid"))
        {
            GenerateGrid();
            SpawnTiles();
        }
    }

    void GenerateGrid()
    {
        gridArray = new int[columnsNum, rowsNum];
    }

    void SpawnTiles()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach(GameObject grid in grids)
        {
            DestroyImmediate(grid);
        }

        grid = new GameObject();
        grid.name = "Grid";
        grid.tag = "Grid";
        GridScript gridScript = grid.AddComponent<GridScript>();
        gridScript.rows = gridArray.GetLength(0);
        gridScript.columns = gridArray.GetLength(1);

        foreach (Transform child in grid.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for(int row = 0; row < gridArray.GetLength(0); row++)
        {
            for(int column = 0; column < gridArray.GetLength(1); column++)
            {
                TileScript tile = Instantiate(tilePrefab).GetComponent<TileScript>();
                tile.gridPosition = new Vector2Int(row, column);
                tile.transform.position = new Vector3(tile.gridPosition.x, tile.gridPosition.y, 0);
                tile.transform.parent = grid.transform;
            }
        }
    }
}
