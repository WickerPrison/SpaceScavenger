using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvage : MonoBehaviour, IInteractable
{
    int resources;
    TileScript myTile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        myTile = TileMethods.GetTileAtPosition(transform.position);
        myTile.occupant = Occupant.SALVAGE;
        myTile.occupantObject = gameObject;
        myTile.interactable = this;
        resources = Random.Range(3, 10);
    }

    public void Interact(PlayerUnit playerUnit)
    {
        resources = playerUnit.CollectSalvage(resources);
        UIManager.instance.UpdateSalvageUI(playerUnit);

        if(resources == 0)
        {
            myTile.occupant = Occupant.EMPTY;
            myTile.occupantObject = null;
            myTile.interactable = null;
            Destroy(gameObject);
        }

    }
}
