using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private int _health;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    protected int _minHealth = 0;

    public bool IsAlive { get { return _health > 0; } }

    public Action<int> OnHealthChanged;

    private void Start()
    {
        _health = _maxHealth;
    }

    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, _minHealth, _maxHealth);
            OnHealthChanged?.Invoke(_health);
        }
    }
}
