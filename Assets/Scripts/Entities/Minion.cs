using System;
using UnityEngine;
using UnityEngine.AI;

public class Minion : Entity
{
    [SerializeField]
    private int _damageValue = 5;

    private MinionFactory _parent;
    private NavMeshAgent _navMeshAgent;
    private Entity _target;
    private float _attackTimerValue;
    private float _attackStopTimerValue = 1.0f;
    private bool _isAttackTimerStart;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
            throw new Exception("Can't find NavMeshAgent component");
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void SetTarget(Entity target)
    {
        _target = target;
    }

    public void SetParent(MinionFactory parent)
    {
        _parent = parent;
    }

    private bool MoveToTarget()
    {
        return _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void RotateToTarget()
    {
        Vector3 direction = _target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up); 
    }

    private void AttackTarget()
    {
        _target.TakeDamage(_damageValue);
    }

    public override void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= minHealth)
        {
            _parent.MinionKilled(transform.position);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
        if (distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            if (_isAttackTimerStart)
            {
                if (_attackTimerValue < _attackStopTimerValue)
                {
                    _attackTimerValue += Time.deltaTime;
                }
                else
                {
                    _attackTimerValue = 0.0f;
                    AttackTarget();
                }
            }
            else
            {
                AttackTarget();
                _isAttackTimerStart = true;
                _attackTimerValue = 0.0f;
            }
        }
        else
        {
            _isAttackTimerStart = false;
        }

        MoveToTarget();
        RotateToTarget();
    }
}
