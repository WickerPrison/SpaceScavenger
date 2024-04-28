using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSalvage : MonoBehaviour
{
    [SerializeField] int minSalvage;
    [SerializeField] int maxSalvage;
    [SerializeField] GameObject salvagePrefab;
    GridScript grid;
    List<GameObject> salvageList;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        salvageList = new List<GameObject>();
        GetLocations();
    }

    void GetLocations()
    {
        int salvageNum = Random.Range(minSalvage, maxSalvage + 1);
        for (int i = 0; i < salvageNum; i++)
        {
            bool validPosition = false;
            while (!validPosition)
            {
                int xpos = Random.Range(0, grid.tileArray.GetLength(0));
                int ypos = Random.Range(0, grid.tileArray.GetLength(1));
                if (grid.tileArray[xpos, ypos].occupant == Occupant.EMPTY && NotNearOtherSalvage(xpos, ypos))
                {
                    Spawn(grid.tileArray[xpos, ypos]);
                    validPosition = true;
                }
            }

        }
    }

    bool NotNearOtherSalvage(int xpos, int ypos)
    {
        Transform tileTransform = grid.tileArray[xpos, ypos].gameObject.transform;
        foreach(GameObject obj in salvageList)
        {
            if(Vector2.Distance(tileTransform.position, obj.transform.position) < 3.5f)
            {
                return false;
            }
        }
        return true;
    }

    void Spawn(TileScript spawnTile)
    {
        GameObject salvage = Instantiate(salvagePrefab);
        salvageList.Add(salvage);
        salvage.transform.position = spawnTile.transform.position;
        spawnTile.occupant = Occupant.SALVAGE;
        spawnTile.occupantObject = salvage;
    }
}
