using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _damage = 0;
    [SerializeField]
    private float _life = 1.0f;

    private void Awake()
    {
        Destroy(gameObject, _life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var entity = collision.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
