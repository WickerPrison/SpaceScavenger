using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum EdgeType
{
    INTERIOR, SPACE, WALL
};

[CreateAssetMenu]
public class GenSquareProfile : ScriptableObject
{
    public Sprite sprite;
    public TextAsset csv;
    public EdgeType top;
    public EdgeType bottom;
    public EdgeType left;
    public EdgeType right;
}
