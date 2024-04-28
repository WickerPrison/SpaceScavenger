using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get;private set; }
    LayerMask mask;
    GridScript grid;
    public Controls controls;

    private void Awake()
    {
        instance = this;
        controls = new Controls();
    }

    private void Start()
    {
        mask = LayerMask.GetMask("Tiles");
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
    }

    private void Update()
    {

    }

    public TileScript GetTileAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10, mask);

        if (hit.collider != null)
        {
            return hit.transform.gameObject.GetComponent<TileScript>();
        }
        else return null;
    }

    void Testing()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10, mask);

        if (hit.collider != null)
        {
            grid.ResetTiles();
            TileScript tileScript = hit.transform.gameObject.GetComponent<TileScript>();
            tileScript.SetTileColor(Color.red);
            List<TileScript> adjacentTiles = tileScript.GetAdjacencyList();
            foreach (TileScript tile in adjacentTiles)
            {
                tile.SetTileColor(Color.blue);
            }
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
