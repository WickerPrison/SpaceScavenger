using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerUnitSO : ScriptableObject
{
    public string unitName;
    public int maxHealth;
    public int maxAP;
    public int carryCapacity;
}
