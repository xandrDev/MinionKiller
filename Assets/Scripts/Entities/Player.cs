using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField]
    private DataIndicator _dataIndicator = null;
    [SerializeField]
    private ColorChanger _colorChanger = null;
    [SerializeField]
    private Weapon _currentWeapon;

    private Color _bulletColor;

    void Start()
    {
        if (_dataIndicator == null)
            throw new System.Exception("DataIndicator is not defined");

        if (_colorChanger == null)
            throw new System.Exception("ColorChanger is not defined");
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
            _bulletColor = box.Color;
            _currentWeapon.BullenColor = box.Color;
            _dataIndicator.IncreaseBoxCount(box.Color);
            box.PickUp();
        }
    }
}
