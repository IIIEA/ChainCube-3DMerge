using System;
using UnityEngine;

[Serializable]
public class ColorData
{
    [SerializeField] private long _number;
    [SerializeField] private Color _color;

    public long Nubmer => _number;
    public Color Color => _color;
}
