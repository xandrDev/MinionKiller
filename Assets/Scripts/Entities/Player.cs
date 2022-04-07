using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField]
    private DataIndicator _dataIndicator;
    [SerializeField]
    private ColorChanger _colorChanger;

    void Awake()
    {
        if (_dataIndicator == null)
            throw new System.Exception("DataIndicator is not defined");

        if (_colorChanger == null)
            throw new System.Exception("ColorChanger is not defined");
    }

    private void Start()
    {
        health = maxHealth;
        _dataIndicator.healthDataUpdate(health);
    }

    public override void TakeDamage(int damageValue)
    {
        health -= damageValue;
        _dataIndicator.healthDataUpdate(health);

        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        var box = other.gameObject.GetComponent<Box>();
        if (box != null)
        {
            _colorChanger.SetBulltColor(box.Color);
            _dataIndicator.IncreaseBoxCount(box.Color);
            box.PickUp();
        }
    }
}
