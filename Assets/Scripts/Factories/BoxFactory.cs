using System.Collections.Generic;
using UnityEngine;

public class BoxFactory : GenericFactory<Box>
{
    private Dictionary<ColorName, Color> _colors = new Dictionary<ColorName, Color>()
    {
        { ColorName.green, Color.green },
        { ColorName.red, Color.red },
        { ColorName.yellow, Color.yellow },
    };

    public ColorName GetRandomName()
    {
        return (ColorName)Random.Range(0, System.Enum.GetValues(typeof(ColorName)).Length);
    }

    //public Color GetRandomColor()
    //{
    //    var colorIndex = (ColorName)Random.Range(0, _colors.Count);
    //    return _colors[colorIndex];
    //}

    public void CreateBox(Vector3 spawnPosition)
    {
        var newBox = GetInstance(spawnPosition);
        var colorName = GetRandomName();

        newBox.GetComponent<Renderer>().material.color = _colors[colorName];
        newBox.Color = _colors[colorName];
        newBox.ColorName = colorName;
    }
}
