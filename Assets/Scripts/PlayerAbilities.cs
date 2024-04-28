using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerUnit unit;
    UnitStats stats;
    Pathfinding pathfinder;

    private void Start()
    {
        unit = GetComponent<PlayerUnit>();
        pathfinder = GetComponent<Pathfinding>();
        stats = GetComponent<UnitStats>();
    }

    public void SetupAbility(AbilityProfile profile)
    {
        if(profile.abilityScript != null)
        {
            string test = profile.abilityScript.GetType().Name;
            Type t = Type.GetType(test);
            gameObject.AddComponent(t);
        }
    }

    public void UseAbility(AbilityProfile profile)
    {
        if (unit.state == PlayerUnitState.IDLE || unit.state == PlayerUnitState.MOVING || stats.currentAP < profile.APcost) return;
        switch (profile.abilityType)
        {
            case AbilityType.MOVEMENT:
                unit.currentProfile = profile;
                unit.state = PlayerUnitState.SELECTTARGET;
                break;
            case AbilityType.ATTACK:
                unit.currentProfile = profile;
                unit.state = PlayerUnitState.SELECTTARGET;
                break;
        }
    }

    public void SelectTarget(AbilityProfile profile, TileScript selectedTile)
    {
        switch (profile.abilityType)
        {
            case AbilityType.MOVEMENT:
                stats.LoseAP(profile.APcost);
                unit.GetPath(selectedTile);
                unit.FollowPath();
                break;
            case AbilityType.ATTACK:
                if(selectedTile.occupantObject != null)
                {
                    UnitStats enemyUnit = selectedTile.occupantObject.GetComponent<UnitStats>();
                    enemyUnit.TakeDamage(profile.damage);
                    stats.LoseAP(stats.currentAP);
                    pathfinder.ResetTiles();
                    unit.state = PlayerUnitState.IDLE;
                }
                break;
        }
    }
}
