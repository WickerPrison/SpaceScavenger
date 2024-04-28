using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

public enum EnemyUnitState
{
    MYTURN, MOVING, IDLE
}

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] AbilityProfile movement;
    [SerializeField] AbilityProfile attack;
    UnitStats stats;
    EnemyController controller;
    Pathfinding pathfinding;
    EnemyUnitState unitState;
    Stack<TileScript> path = new Stack<TileScript>();
    TileScript currentTile;
    TileScript nextTile;
    [SerializeField] Transform movePoint;
    Vector2 moveDirection;
    float walkSpeed = 5;
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        controller.enemyUnits.Add(this);
    }

    private void Start()
    {
        unitState = EnemyUnitState.IDLE;

        pathfinding = GetComponent<Pathfinding>();
        stats = GetComponent<UnitStats>();
        stats.OnDeath += OnDeath;
        currentTile = TileMethods.GetTileAtPosition(transform.position);
        currentTile.occupant = Occupant.ENEMY;
        currentTile.occupantObject = gameObject;

        movePoint.parent = null;
    }


    private void Update()
    {
        switch (unitState)
        {
            case EnemyUnitState.MYTURN:
                if (stats.currentAP > 0)
                {
                    PerformAbility();
                }
                else EndTurn();
                break;
            case EnemyUnitState.MOVING:
                pathfinding.ResetTiles();
                moveDirection = movePoint.position - transform.position;
                transform.Translate(walkSpeed * Time.deltaTime * moveDirection.normalized);
                if (moveDirection.magnitude <= Time.deltaTime * walkSpeed)
                {
                    if (path.Count > 0)
                    {
                        FollowPath();
                    }
                    else
                    {
                        unitState = EnemyUnitState.MYTURN;
                        currentTile = pathfinding.GetTileAtPosition(transform.position);
                        currentTile.occupant = Occupant.ENEMY;
                        currentTile.occupantObject = gameObject;
                        transform.position = currentTile.transform.position;
                    }
                }
                break;
        }
    }

    void PerformAbility()
    {
        currentTile = pathfinding.GetTileAtPosition(transform.position);
        pathfinding.Pathfinder(currentTile, movement);
        float minDistance = 10000;
        TileScript destinationTile = currentTile;
        PlayerUnit targetUnit = null;
        foreach (TileScript tile in pathfinding.selectableTiles)
        {
            foreach (PlayerUnit unit in GameManager.instance.playerUnitList)
            {
                float distance = Vector2.Distance(tile.transform.position, unit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    destinationTile = tile;
                    targetUnit = unit;
                }
            }
        }

        if (targetUnit == null) return;

        int targetDistance = pathfinding.GetDistance(currentTile, pathfinding.GetTileAtPosition(targetUnit.transform.position));

        if(targetDistance <= attack.range)
        {
            Attack(targetUnit);
        }
        else
        {
            Move(destinationTile);
        }
    }

    void Move(TileScript destinationTile)
    {
        if (destinationTile != currentTile)
        {
            stats.currentAP -= movement.APcost;
            GetPath(destinationTile);
            FollowPath();
        }
        else EndTurn();
    }

    void Attack(PlayerUnit playerUnit)
    {
        stats.currentAP -= attack.APcost;
        playerUnit.TakeDamage(attack.damage);
    }

    public void StartTurn()
    {
        unitState = EnemyUnitState.MYTURN;
        stats.currentAP = stats.maxAP;
    }

    public void EndTurn()
    {
        pathfinding.ResetTiles();
        unitState = EnemyUnitState.IDLE;
        controller.NextUnit();
    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        LeaveTile();
        Destroy(movePoint.gameObject);
        controller.enemyUnits.Remove(this);
    }

    public void GetPath(TileScript destinationTile)
    {
        TileScript[] tempPath = path.ToArray();
        System.Array.Reverse(tempPath);
        path.Clear();

        TileScript next = destinationTile;
        while (next != null)
        {
            path.Push(next);
            next = next.previousTile;
        }

        for (int i = 1; i < tempPath.Length; i++)
        {
            path.Push(tempPath[i]);
        }
    }

    public void FollowPath()
    {
        LeaveTile();
        unitState = EnemyUnitState.MOVING;
        nextTile = path.Pop();
        movePoint.position = nextTile.transform.position;
    }

    void LeaveTile()
    {
        currentTile.occupant = Occupant.EMPTY;
        currentTile.occupantObject = null;
    }
}
