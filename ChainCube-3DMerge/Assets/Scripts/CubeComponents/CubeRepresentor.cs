using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PointsHolder))]
public class CubeRepresentor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private ColorBundleData _colorBundleData;
    [SerializeField] private TextMeshPro[] _texts;

    private PointsHolder _pointsHolder;

    private void Start()
    {
        _pointsHolder = GetComponent<PointsHolder>();
        SetPoints(_pointsHolder.Points);
        _pointsHolder.onPointsChanged += SetPoints;
    }

    public void OnDestroy()
    {
        try
        {
            _pointsHolder.onPointsChanged -= SetPoints;
        }
        catch (Exception e)
        {
            Debug.LogError("NullReference");
        }
    }

    private void SetPoints(long points)
    {
        foreach (var text in _texts)
        {
            text.text = points.ToString();
        }

        var color = _colorBundleData.ColorData.Find(x => x.Nubmer == points);

        if (color == null)
        {
            _renderer.material.color = _colorBundleData.DefaultColor;
        }
        else
        {
            _renderer.material.color = color.Color;
        }
    }
}
