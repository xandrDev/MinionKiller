using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerScore))]
public class Player : Entity
{
    [SerializeField]
    private Weapon _currentWeapon = null;

    private PlayerScore _playerScore;

    void Start()
    {
        _playerScore = GetComponent<PlayerScore>();
    }

    private void OnEnable()
    {
        OnKilled += ReloadScene;
    }

    private void OnDisable()
    {
        OnKilled -= ReloadScene;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        var box = other.gameObject.GetComponent<Box>();
        if (box != null)
        {
            _currentWeapon.BullenColor = box.Color;
            _playerScore.IncreaseBoxCount(box.ColorName);
            box.PickUp();
        }
    }
}
