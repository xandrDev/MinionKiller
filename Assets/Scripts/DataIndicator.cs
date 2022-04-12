using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataIndicator : MonoBehaviour
{
    [SerializeField]
    private EntityHealth _playerHealth;

    public Text HealthTxt;
    public Text RedBoxCountTxt;
    public Text YellowBoxCountTxt;
    public Text GreenBoxCountTxt;

    private Dictionary<Color, int> colorToCountMap = new Dictionary<Color, int>()
    {
        { Color.green, 0 },
        { Color.red, 0 },
        { Color.yellow, 0 }
    };

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += PlayerHealthUpdate;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= PlayerHealthUpdate;
    }

    private void Start()
    {
        RefreshCounterStatistic();
    }

    public void IncreaseBoxCount(Color boxColor)
    {
        colorToCountMap[boxColor]++;
        RefreshCounterStatistic();
    }

    private void RefreshCounterStatistic()
    {
        RedBoxCountTxt.text = string.Format("Red box count: {0:0}", colorToCountMap[Color.red]);
        GreenBoxCountTxt.text = string.Format("Green box count: {0:0}", colorToCountMap[Color.green]);
        YellowBoxCountTxt.text = string.Format("Yellow box count: {0:0}", colorToCountMap[Color.yellow]);
    }

    private void PlayerHealthUpdate(int health)
    {
        HealthTxt.text = string.Format("Health: {0:0}", health);
    }
}
