using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

    public enum PlayerState
    {
        PLAYERTURN, UNITSELECTED, IDLE
    }

public class PlayerScript : MonoBehaviour
{
    [SerializeField] bool autoName;
    PlayerUnit selectedUnit;
    bool canStartNextTurn;
    [System.NonSerialized] public PlayerState playerState;
    LayerMask mask;
    //GridScript grid;

    private void Start()
    {
        mask = LayerMask.GetMask("Tiles");
        //grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();

        GameManager.instance.StartPlayerTurn();

        InputManager.instance.controls.Combat.LeftClick.performed += ctx => LeftClick();
        InputManager.instance.controls.Combat.EndTurn.performed += ctx => EndTurn();

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        if (autoName)
        {
            for(int i = 0; i < GameManager.instance.playerUnitList.Count; i++)
            {
                GameManager.instance.playerUnitList[i].unitName = "Doug " + (i + 1).ToString(); 
            }
        }
    }

    public void StartTurn(object sender, System.EventArgs e)
    {
        playerState = PlayerState.PLAYERTURN;
        foreach(PlayerUnit unit in GameManager.instance.playerUnitList)
        {
            unit.StartTurn();
        }
    }

    void EndTurn()
    {
        if (playerState != PlayerState.PLAYERTURN) return;
        canStartNextTurn = true;
        foreach (PlayerUnit unit in GameManager.instance.playerUnitList)
        {
            if (unit.state != PlayerUnitState.IDLE) canStartNextTurn = false;
        }
        if (!canStartNextTurn) return;

        playerState = PlayerState.IDLE;
        GameManager.instance.StartEnemyTurn();
    }

    void LeftClick()
    {
        if (UIManager.instance.mouseOverUI) return;
        TileScript clickedTile = InputManager.instance.GetTileAtMouse();
        if (clickedTile == null) return; 
        switch (playerState)
        {
            case PlayerState.PLAYERTURN:
                SelectUnit(clickedTile);
                break;
            case PlayerState.UNITSELECTED:
                DeselectUnit(clickedTile);
                break;
        }
    }

    void SelectUnit(TileScript clickedTile)
    {
        if (clickedTile.occupant == Occupant.PLAYER)
        {
            selectedUnit = clickedTile.occupantObject.GetComponent<PlayerUnit>();
            selectedUnit.state = PlayerUnitState.SELECTED;
            playerState = PlayerState.UNITSELECTED;
            UIManager.instance.SetUIUnit(selectedUnit);            
        }
    }

    void DeselectUnit(TileScript clickedTile)
    {
        if (!clickedTile.selectable)
        {
            if (selectedUnit != null)
            {
                selectedUnit.state = PlayerUnitState.IDLE;
                selectedUnit.UnselectUnit();
                selectedUnit = null;
            }
            playerState = PlayerState.PLAYERTURN;
            UIManager.instance.HideUnitUI();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.OnStartPlayerTurn += StartTurn;
    }

    private void OnDisable()
    {
        GameManager.instance.OnStartPlayerTurn -= StartTurn;
    }
}
