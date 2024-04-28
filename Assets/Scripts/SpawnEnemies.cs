using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] int minEnemies;
    [SerializeField] int maxEnemies;
    [SerializeField] GameObject enemyPrefab;
    GridScript grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        GetLocations();
    }

    void GetLocations()
    {
        int enemyNum = Random.Range(minEnemies, maxEnemies + 1);
        for(int i = 0; i < enemyNum; i++)
        {
            bool validPosition = false;
            while (!validPosition)
            {
                int xpos = Random.Range(0, grid.tileArray.GetLength(0));
                int ypos = Random.Range(0, grid.tileArray.GetLength(1));
                if (grid.tileArray[xpos, ypos].occupant == Occupant.EMPTY)
                {
                    Spawn(grid.tileArray[xpos, ypos]);
                    validPosition = true;
                }
            }

        }
    }

    void Spawn(TileScript spawnTile)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnTile.transform.position;
        spawnTile.occupant = Occupant.ENEMY;
        spawnTile.occupantObject = enemy;
    }
}
