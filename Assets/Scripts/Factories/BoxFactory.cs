using System.Collections.Generic;
using UnityEngine;

public class BoxFactory : GenericFactory<Box>
{
    private List<Color> _boxColors = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.yellow
    };

    public Color ChangeBoxMaterialColor(GameObject box)
    {
        var colorIndex = Random.Range(0, _boxColors.Count);
        var boxColor = _boxColors[colorIndex];
        box.GetComponent<Renderer>().material.color = boxColor;
        return boxColor;
    }

    public void CreateBox(Vector3 spawnPosition)
    {
        var newBox = GetInstance(spawnPosition);
        newBox.Color = ChangeBoxMaterialColor(newBox.gameObject);
    }
}
