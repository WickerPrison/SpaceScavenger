using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMap : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] int width;
    public List<GenSquareProfile> superpositions = new List<GenSquareProfile>();
    [SerializeField] float wallAlignmentBonus;
    [SerializeField] List<float> weights = new List<float>();
    public Dictionary<GenSquareProfile, float> weightDict = new Dictionary<GenSquareProfile, float>();
    List<GenSquare> squares = new List<GenSquare>();
    GenSquare[,] squareGrid;
    GameObject[] squareObjects;
    GenSquare nextSquare;

    [SerializeField] GameObject tilePrefab;
    GameObject grid;
    GridScript gridScript;
    int[,] gridArray;

    // Start is called before the first frame update
    void Awake()
    {
        squareGrid = new GenSquare[width, height];
        SetupDictionary();
        SetupSquares();
        SetupChosenProfiles();
        while(squares.Count > 0)
        {
            nextSquare = ChooseNextSquare();
            nextSquare.SelectProfile();
            if (nextSquare.selectedProfile == null) return;
            CollapseWavefunctions();
            squares.Remove(nextSquare);
        }

        gridArray = new int[width * 5, height * 5];
        BuildGridArray();
        SpawnTiles();
        foreach(TileScript tile in gridScript.tileArray)
        {
            tile.SetUpAdjacencyList();
        }
    }

    void SetupSquares()
    {
        squareObjects = GameObject.FindGameObjectsWithTag("Generation Square");
        foreach (GameObject square in squareObjects)
        {
            GenSquare genSquare = square.GetComponent<GenSquare>();
            genSquare.superpositions = new List<GenSquareProfile>(superpositions);
            genSquare.weightDict = new Dictionary<GenSquareProfile, float>(weightDict);
            squareGrid[genSquare.gridPosition[0], genSquare.gridPosition[1]] = genSquare;
            squares.Add(genSquare);
        }
    }

    void SetupChosenProfiles()
    {
        List<GenSquare> squaresToRemove = new List<GenSquare>();
        foreach(GenSquare square in squares)
        {
            if(square.chosenProfile != null)
            {
                nextSquare = square;
                nextSquare.SelectProfile();
                CollapseWavefunctions();
                squaresToRemove.Add(square);
            }
        }

        foreach(GenSquare square in squaresToRemove)
        {
            squares.Remove(square);
        }
    }

    GenSquare ChooseNextSquare()
    {
        List<GenSquare> squarePool = new List<GenSquare>();
        float entropy = 10000;
        foreach(GenSquare genSquare in squares)
        {
            float squareEntropy = genSquare.GetEntropy();
            if (squareEntropy < entropy)
            {
                entropy = squareEntropy;
            }
        }

        if (entropy == 10000) return null;

        foreach(GenSquare genSquare in squares)
        {
            float squareEntropy = genSquare.GetEntropy();
            if(squareEntropy == entropy)
            {
                squarePool.Add(genSquare);
            }
        }

        int randInt = Random.Range(0, squarePool.Count);
        return squarePool[randInt];
    }

    void CollapseWavefunctions()
    {
        GenSquare targetSquare;
        if (nextSquare.gridPosition[1] + 1 < squareGrid.GetLength(1))
        {
            targetSquare = squareGrid[nextSquare.gridPosition[0], nextSquare.gridPosition[1] + 1];
            targetSquare.CollapseWavefunction(nextSquare.selectedProfile, "Bottom", wallAlignmentBonus);
        }

        if(nextSquare.gridPosition[1] - 1 >= 0)
        {
            targetSquare = squareGrid[nextSquare.gridPosition[0], nextSquare.gridPosition[1] - 1];
            targetSquare.CollapseWavefunction(nextSquare.selectedProfile, "Top", wallAlignmentBonus);
        }

        if(nextSquare.gridPosition[0] + 1 < squareGrid.GetLength(0))
        {
            targetSquare = squareGrid[nextSquare.gridPosition[0] + 1, nextSquare.gridPosition[1]];
            targetSquare.CollapseWavefunction(nextSquare.selectedProfile, "Left", wallAlignmentBonus);
        }
        
        if(nextSquare.gridPosition[0] - 1 >= 0)
        {
            targetSquare = squareGrid[nextSquare.gridPosition[0] - 1, nextSquare.gridPosition[1]];
            targetSquare.CollapseWavefunction(nextSquare.selectedProfile, "Right", wallAlignmentBonus);
        }
    }

    void BuildGridArray()
    {
        foreach(GenSquare square in squareGrid)
        {
            for(int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 5;  y++)
                {
                    gridArray[x + 5 * square.gridPosition[0], y + 5 * square.gridPosition[1]] = square.gridArray[x, y];
                }
            }
        }
    }

    void SpawnTiles()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject grid in grids)
        {
            DestroyImmediate(grid);
        }

        grid = new GameObject();
        grid.name = "Grid";
        grid.tag = "Grid";
        gridScript = grid.AddComponent<GridScript>();
        gridScript.rows = gridArray.GetLength(0);
        gridScript.columns = gridArray.GetLength(1);
        gridScript.tileArray = new TileScript[gridScript.rows, gridScript.columns];

        foreach (Transform child in grid.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int row = 0; row < gridArray.GetLength(0); row++)
        {
            for (int column = 0; column < gridArray.GetLength(1); column++)
            {
                TileScript tile = Instantiate(tilePrefab).GetComponent<TileScript>();
                tile.gridPosition = new Vector2Int(row, column);
                tile.transform.position = new Vector3(tile.gridPosition.x, tile.gridPosition.y, 0);
                tile.transform.parent = grid.transform;
                tile.generationID = gridArray[row, column];
                tile.TileGeneration();
                tile.TileStart();
            }
        }
    }

    void SetupDictionary()
    {
        for(int i = 0; i < superpositions.Count; i++)
        {
            weightDict.Add(superpositions[i], weights[i]);
        }
    }
}
