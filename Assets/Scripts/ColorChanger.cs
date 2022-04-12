﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Color _bulletColor;

    private List<Color> _boxColors = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.yellow
    };

    public void SetBulltColor(Color color)
    {
        _bulletColor = color;
    }

    public void ChangeBulletMaterialColor(GameObject bullet)
    {
        bullet.GetComponent<Renderer>().material.color = _bulletColor;
    }

    
}
