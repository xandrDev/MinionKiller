using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataIndicator : MonoBehaviour
{
    [SerializeField]
    private EntityHealth _playerHealth = null;
    [SerializeField]
    private PlayerScore _playerScore = null;

    public Text HealthTxt;
    public Text RedBoxCountTxt;
    public Text YellowBoxCountTxt;
    public Text GreenBoxCountTxt;

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += PlayerHealthUpdate;
        _playerScore.OnGreenCountChanged += GreenBoxCountChanged;
        _playerScore.OnRedCountChanged += RedBoxCountChanged;
        _playerScore.OnYellowCountChanged += YellowBoxCountChanged;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= PlayerHealthUpdate;
        _playerScore.OnGreenCountChanged -= GreenBoxCountChanged;
        _playerScore.OnRedCountChanged -= RedBoxCountChanged;
        _playerScore.OnYellowCountChanged -= YellowBoxCountChanged;
    }

    private void GreenBoxCountChanged(int newScore)
    {
        GreenBoxCountTxt.text = string.Format("Green box count: {0:0}", newScore);
    }

    private void RedBoxCountChanged(int newScore)
    {
        RedBoxCountTxt.text = string.Format("Red box count: {0:0}", newScore);
    }

    private void YellowBoxCountChanged(int newScore)
    {
        YellowBoxCountTxt.text = string.Format("Yellow box count: {0:0}", newScore);
    }

    private void PlayerHealthUpdate(int health)
    {
        HealthTxt.text = string.Format("Health: {0:0}", health);
    }
}
