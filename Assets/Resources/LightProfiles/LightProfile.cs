using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightType 
{
    NONE, CONE, RADIUS
}


[CreateAssetMenu]
public class LightProfile : ScriptableObject
{
    public LightType type;
    public float range;
    public float angle;
}
