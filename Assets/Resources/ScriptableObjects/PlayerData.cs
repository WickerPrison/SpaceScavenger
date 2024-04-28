using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public List<PlayerUnitSO> barracks;
    public List<PlayerUnitSO> squad;
    public int unitNum;
}
