using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected int health;
    protected int maxHealth = 100;
    protected int minHealth = 0;
    public virtual void TakeDamage(int damegeValue) { }
}
