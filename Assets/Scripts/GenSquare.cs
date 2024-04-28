using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenSquare : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    public GenSquareProfile chosenProfile;
    [System.NonSerialized] public int[] gridPosition = new int[2];
    [System.NonSerialized] public List<GenSquareProfile> superpositions = new List<GenSquareProfile>();
    public Dictionary<GenSquareProfile, float> weightDict;
    List<float> percentChances = new List<float>();
    [System.NonSerialized] public GenSquareProfile selectedProfile;
    [System.NonSerialized] public int[,] gridArray = new int[5, 5];

    private void Awake()
    {
        gridPosition[0] = Mathf.RoundToInt((transform.localPosition.x - 2f) / 5f);
        gridPosition[1] = Mathf.RoundToInt((transform.localPosition.y - 2f) / 5f);
    }

    public float GetEntropy()
    {
        float entropy = 0;
        foreach (GenSquareProfile superposition in superpositions)
        {
            entropy += weightDict[superposition];
        }
        return entropy;
    }

    public void SelectProfile()
    {
        if(superpositions.Count <= 0)
        {
            Debug.Log("Failed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        float entropy = GetEntropy();

        if(chosenProfile == null)
        {

            for(int i = 0; i < superpositions.Count; i++)
            {
                float percentChance = weightDict[superpositions[i]] / entropy;
                percentChances.Add(percentChance);
            }

            float value = Random.value;
            for(int i = 0; i < superpositions.Count; i++)
            {
                if (percentChances[i] > value)
                {
                    selectedProfile = superpositions[i];
                    break;
                }
                else
                {
                    value -= percentChances[i];
                }
            }
        }
        else
        {
            selectedProfile = chosenProfile;
        }
        spriteRenderer.sprite = selectedProfile.sprite;
        gridArray = ReadCSV(selectedProfile);
    }

    // direction is the direction towards the adjacent tile
    public void CollapseWavefunction(GenSquareProfile adjacentProfile, string direction, float wallAlignmentBonus)
    {
        EdgeType adjacentEdgeType = GetAdjacentEdgeType(adjacentProfile, direction);
        List<GenSquareProfile> profilesToRemove = new List<GenSquareProfile>();
        foreach(GenSquareProfile profile in superpositions)
        {
            List<EdgeType> disallowedEdgeTypes = GetDisallowedEdgeTypes(adjacentEdgeType);
            if(disallowedEdgeTypes.Contains(GetEdgeType(profile, direction)))
            {
                profilesToRemove.Add(profile);
            }
            else if(adjacentEdgeType == EdgeType.INTERIOR && GetEdgeType(profile, direction) == EdgeType.INTERIOR)
            {
                if(WallsLineUp(adjacentProfile, profile, direction))
                {
                    weightDict[profile] += wallAlignmentBonus;
                }
            }
        }

        foreach(GenSquareProfile profile in profilesToRemove)
        {
            superpositions.Remove(profile);
        }
    }

    EdgeType GetAdjacentEdgeType(GenSquareProfile profile, string direction)
    {
        switch (direction)
        {
            case "Top":
                return profile.bottom;
            case "Bottom":
                return profile.top;
            case "Left":
                return profile.right;
            case "Right":
                return profile.left;
        }
        return EdgeType.SPACE;
    }

    EdgeType GetEdgeType(GenSquareProfile profile, string direction)
    {
        switch (direction)
        {
            case "Top":
                return profile.top;
            case "Bottom":
                return profile.bottom;
            case "Left":
                return profile.left;
            case "Right":
                return profile.right;
        }
        return EdgeType.SPACE;
    }

    List<EdgeType> GetDisallowedEdgeTypes(EdgeType adjacentEdgeType)
    {
        List<EdgeType> disallowedEdgeTypes = new List<EdgeType>();
        switch (adjacentEdgeType)
        {
            case EdgeType.INTERIOR:
                disallowedEdgeTypes.Add(EdgeType.SPACE);
                disallowedEdgeTypes.Add(EdgeType.WALL);
                break;
            case EdgeType.WALL:
                disallowedEdgeTypes.Add(EdgeType.INTERIOR);
                break;
            case EdgeType.SPACE:
                disallowedEdgeTypes.Add(EdgeType.INTERIOR);
                break;
        }
        return disallowedEdgeTypes;
    }

    bool WallsLineUp(GenSquareProfile adjacentProfile, GenSquareProfile profile, string direction)
    {
        bool firstWallLinesUp = true;
        bool secondWallLinesUp = true;
        if(direction == "Top" || direction == "Bottom")
        {
            if (adjacentProfile.left == EdgeType.WALL)
            {
                firstWallLinesUp = profile.left == EdgeType.WALL;
            }
            if(adjacentProfile.right == EdgeType.WALL)
            {
                secondWallLinesUp = profile.right == EdgeType.WALL;
            }
        }
        else
        {
            if (adjacentProfile.top == EdgeType.WALL)
            {
                firstWallLinesUp = profile.top == EdgeType.WALL;
            }
            if (adjacentProfile.bottom == EdgeType.WALL)
            {
                secondWallLinesUp = profile.bottom == EdgeType.WALL;
            }
        }

        if (firstWallLinesUp && secondWallLinesUp) return true;
        else return false;
    }

    int[,] ReadCSV(GenSquareProfile profile)
    {
        string[] levelLayout = profile.csv.text.Split('\n');
        int height = levelLayout.Length - 1;
        int width = levelLayout[0].Split('|').Length;
        int[,] gridArray = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            string[] line = levelLayout[y].Split('|');
            for (int x = 0; x < line.Length; x++)
            {
                gridArray[x, height - 1 - y] = int.Parse(line[x]);
            }
        }
        return gridArray;
    }

    private void OnDrawGizmosSelected()
    {
        if(chosenProfile != null)
        {
            spriteRenderer.sprite = chosenProfile.sprite;
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }
}
