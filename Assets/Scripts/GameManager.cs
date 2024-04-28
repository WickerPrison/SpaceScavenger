using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYERTURN, ENEMYTURN
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [System.NonSerialized] public GameState gameState;
    GridScript grid;

    EnemyController enemyController;
    PlayerScript playerScript;
    [System.NonSerialized] public List<PlayerUnit> playerUnitList = new List<PlayerUnit>();

    public event EventHandler OnStartPlayerTurn;
    public event EventHandler OnStartEnemyTurn;
    public event EventHandler OnCollectResources;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        enemyController = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
    }

    public void StartEnemyTurn()
    {
        grid.ResetTiles();
        gameState = GameState.ENEMYTURN;
        OnStartEnemyTurn?.Invoke(this, EventArgs.Empty);
    }

    public void StartPlayerTurn()
    {
        gameState = GameState.PLAYERTURN;
        OnStartPlayerTurn?.Invoke(this, EventArgs.Empty);
    }
}
