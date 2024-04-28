using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    MOVEMENT, ATTACK
}

[CreateAssetMenu]
public class AbilityProfile : ScriptableObject
{
    public Sprite abilityIcon;
    public AbilityType abilityType;
    public int APcost;
    public int range;
    public int damage;
    public Color outlineColor;
    public Color selectableColor;
    public bool allTilesPathable;
    public List<Occupant> pathableTiles;
    public List<Occupant> selectableTiles;
    public bool allFowStatesSelectable;
    public List<FOWstate> selectableFowStates;
    public BaseAbility abilityScript;
}
