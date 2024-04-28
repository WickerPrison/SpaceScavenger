using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Transform door1;
    [SerializeField] Transform door2;
    Vector3 openVector = new Vector3(0.1f, 1, 1);
    Vector3 closeVector = new Vector3(0.5f, 1, 1);
    bool doorOpen = false;
    [System.NonSerialized] public TileScript myTile;
    float animationTime = 0.2f;
    WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
    [System.NonSerialized] public Door partnerDoor;

    // Start is called before the first frame update
    void Start()
    {
        myTile = TileMethods.GetTileAtPosition(transform.position);
        myTile.interactable = this;
    }

    public void Interact(PlayerUnit playerUnit)
    {
        if (doorOpen)
        {
            if (myTile.occupant != Occupant.EMPTY || partnerDoor.myTile.occupant != Occupant.EMPTY) return;
            CloseDoor();
            partnerDoor.CloseDoor();
        }
        else
        {
            OpenDoor();
            partnerDoor.OpenDoor();
        }
    }

    public void OpenDoor()
    {
        doorOpen = true;
        myTile.occupant = Occupant.EMPTY;
        myTile.occupantObject = null;
        StopAllCoroutines();
        StartCoroutine(DoorAnimation(openVector));
    }

    public void CloseDoor()
    {
        doorOpen = false;
        myTile.occupant = Occupant.DOOR;
        myTile.occupantObject = gameObject;
        StopAllCoroutines();
        StartCoroutine(DoorAnimation(closeVector));
    }

    IEnumerator DoorAnimation(Vector3 targetVector)
    {
        Vector3 startVector = door1.localScale;
        float timer = animationTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Vector3 currentVector = Vector3.Lerp(targetVector, startVector, timer / animationTime);
            door1.localScale = currentVector;
            door2.localScale = currentVector;
            yield return endOfFrame;
        }
    }
}
