using System;
using UnityEngine;

[Serializable]
public class Borders
{
    public float minXOffset = 1.5f;
    public float maxXOffset = 1.5f;
    public float minYOffset = 1.5f;
    public float maxYOffset = 1.5f;

    [HideInInspector] public float MinX, MaxX, MinY, MaxY;
}
