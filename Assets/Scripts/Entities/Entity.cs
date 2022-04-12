using System;
using UnityEngine;


[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    protected EntityHealth _entityHealth = null;

    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
    }

    public Action OnKilled;

    public void TakeDamage(int damageValue)
    {
        _entityHealth.Health -= damageValue;

        if (!_entityHealth.IsAlive)
        {
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}
