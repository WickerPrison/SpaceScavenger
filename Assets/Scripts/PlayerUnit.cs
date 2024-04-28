using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum PlayerUnitState
{
    IDLE, SELECTED, SELECTTARGET, MOVING
}

public class PlayerUnit : MonoBehaviour
{
    public string unitName;
    public LightProfile lightProfile;
    public List<AbilityProfile> abilityProfiles = new List<AbilityProfile>();
    UnitStats stats;
    [System.NonSerialized] public AbilityProfile currentProfile;
    [SerializeField] Transform movePoint;
    PlayerScript playerScript;
    Pathfinding pathfinding;
    [System.NonSerialized] public TileScript currentTile;
    Stack<TileScript> path = new Stack<TileScript>();
    [System.NonSerialized] public PlayerUnitState state;
    TileScript nextTile;
    Vector2 moveDirection;
    float walkSpeed = 4;
    PlayerAbilities abilities;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Start()
    {
        GameManager.instance.playerUnitList.Add(this);
        state = PlayerUnitState.IDLE;

        stats = GetComponent<UnitStats>();
        stats.OnDeath += OnDeath;
        pathfinding = GetComponent<Pathfinding>();
        abilities = GetComponent<PlayerAbilities>();

        foreach(AbilityProfile profile in abilityProfiles)
        {
            abilities.SetupAbility(profile);
        }

        InputManager.instance.controls.Combat.LeftClick.performed += ctx => LeftClick();
        InputManager.instance.controls.Combat.Interact.performed += ctx => Interact();
        InputManager.instance.controls.Combat.Ability1.performed += ctx => abilities.UseAbility(abilityProfiles[0]);
        InputManager.instance.controls.Combat.Ability2.performed += ctx => abilities.UseAbility(abilityProfiles[1]);
        //InputManager.instance.controls.Combat.Ability3.performed += ctx => abilities.UseAbility(abilityProfiles[2]);
        InputManager.instance.controls.Combat.Back.performed += ctx => Back();

        movePoint.parent = null;

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        currentTile = pathfinding.GetTileAtPosition(transform.position);
        currentTile.occupant = Occupant.PLAYER;
        currentTile.occupantObject = gameObject;
    }


    private void Update()
    {
        switch (state)
        {
            case PlayerUnitState.SELECTED:
                pathfinding.ResetTiles();
                currentTile.SetBorderColor(Color.blue);
                break;
            case PlayerUnitState.SELECTTARGET:
                pathfinding.Pathfinder(currentTile, currentProfile);
                break;
            case PlayerUnitState.MOVING:
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
                        state = PlayerUnitState.SELECTED;
                        currentTile.occupant = Occupant.PLAYER;
                        currentTile.occupantObject = gameObject;
                        transform.position = currentTile.transform.position;
                    }
                }
                break;
        }
    }

    public void StartTurn()
    {
        stats.currentAP = stats.maxAP;
    }

    public int CollectSalvage(int amount)
    {
        if(stats.carryCapacity >= stats.salvage + amount)
        {
            stats.salvage += amount;
            return 0;
        }
        else
        {
            stats.salvage = stats.carryCapacity;
            return amount - (stats.carryCapacity - stats.salvage);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        GameManager.instance.playerUnitList.Remove(this);
        LeaveTile();
        Destroy(movePoint.gameObject);
    }

    void LeftClick()
    {
        if (UIManager.instance.mouseOverUI) return;
        switch (state)
        {
            case PlayerUnitState.SELECTTARGET:
                TileScript destination = InputManager.instance.GetTileAtMouse();
                if (destination.selectable)
                {
                    abilities.SelectTarget(currentProfile, destination);
                }
                break;
        }
    }

    void Interact()
    {
        if (state != PlayerUnitState.SELECTED) return;
        List<TileScript> adjacent8 = TileMethods.Get8AjacentTiles(currentTile);
        foreach(TileScript tile in adjacent8)
        {
            if(tile.interactable != null)
            {
                tile.interactable.Interact(this);
            }
        }
    }

    void Back()
    {
        switch (state)
        {
            case PlayerUnitState.SELECTTARGET:
                state = PlayerUnitState.SELECTED;
                break;
            case PlayerUnitState.SELECTED:
                UnselectUnit();
                break;
        }
    }

    public void UnselectUnit()
    {
        state = PlayerUnitState.IDLE;
        playerScript.playerState = PlayerState.PLAYERTURN;
        pathfinding.ResetTiles();
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
        state = PlayerUnitState.MOVING;
        nextTile = path.Pop();
        movePoint.position = nextTile.transform.position;
    }

    void LeaveTile()
    {
        currentTile.occupant = Occupant.EMPTY;
        currentTile.occupantObject = null;
    }

    public void UseAbility(int index)
    {
        abilities.UseAbility(abilityProfiles[index]);
    }
}
