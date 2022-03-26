using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ColorBudleData", menuName = "Color Bundle Data", order = 51)]
public class ColorBundleData : ScriptableObject
{
    [SerializeField] private List<ColorData> _colorData;
    [SerializeField] private Color _defaultColor;

    public Color DefaultColor => _defaultColor;
    public List<ColorData> ColorData { get => _colorData; }
}
