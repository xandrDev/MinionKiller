using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private Dictionary<ColorName, int> colorToCountMap = new Dictionary<ColorName, int>()
    {
        { ColorName.green, 0 },
        { ColorName.red, 0 },
        { ColorName.yellow, 0 }
    };

    public Action<int> OnGreenCountChanged;
    public Action<int> OnRedCountChanged;
    public Action<int> OnYellowCountChanged;

    public void IncreaseBoxCount(ColorName colorName)
    {
        colorToCountMap[colorName]++;

        switch (colorName)
        {
            case ColorName.green:
                OnGreenCountChanged?.Invoke(colorToCountMap[colorName]);
                break;
            case ColorName.red:
                OnRedCountChanged?.Invoke(colorToCountMap[colorName]);
                break;
            case ColorName.yellow:
                OnYellowCountChanged?.Invoke(colorToCountMap[colorName]);
                break;
            default:
                break;
        }
    }
}
