using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
    List<EnemyUnit> unitsThisTurn = new List<EnemyUnit>();

    void StartTurn(object sender, System.EventArgs e)
    {
        unitsThisTurn = new List<EnemyUnit>(enemyUnits);
        if(unitsThisTurn.Count > 0)
        {
            unitsThisTurn[0].StartTurn();
        }
        else
        {
            GameManager.instance.StartPlayerTurn();
        }
    }

    public void NextUnit()
    {
        unitsThisTurn.RemoveAt(0);
        if(unitsThisTurn.Count > 0 )
        {
            unitsThisTurn[0].StartTurn();
        }
        else
        {
            GameManager.instance.StartPlayerTurn();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.OnStartEnemyTurn += StartTurn;
    }

    private void OnDisable()
    {
        GameManager.instance.OnStartEnemyTurn -= StartTurn;
    }
}
