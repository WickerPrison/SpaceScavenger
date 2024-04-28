using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    [SerializeField] Color playerTurnColor;
    [SerializeField] Color enemyTurnColor;
    string playerTurnString = "Your Turn";
    string enemyTurnString = "Enemy Turn";
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Instance_OnStartPlayerTurn(object sender, System.EventArgs e)
    {
        text.color = playerTurnColor;
        text.text = playerTurnString;
    }

    private void Instance_OnStartEnemyTurn(object sender, System.EventArgs e)
    {
        text.color = enemyTurnColor;
        text.text = enemyTurnString;
    }

    private void OnEnable()
    {
        GameManager.instance.OnStartPlayerTurn += Instance_OnStartPlayerTurn;
        GameManager.instance.OnStartEnemyTurn += Instance_OnStartEnemyTurn;
    }

    private void OnDisable()
    {
        GameManager.instance.OnStartPlayerTurn -= Instance_OnStartPlayerTurn;
        GameManager.instance.OnStartEnemyTurn -= Instance_OnStartEnemyTurn;
    }
}
