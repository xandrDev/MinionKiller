using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxFactory))]
public class Minion : Entity
{
    [SerializeField]
    private int _damageValue = 5;

    private BoxFactory _boxFactory;

    private NavMeshAgent _navMeshAgent;
    private Entity _target;
    private float _attackTimerValue;
    private float _attackStopTimerValue = 1.0f;
    private bool _isAttackTimerStart;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
            throw new Exception("Can't find NavMeshAgent component");

        _boxFactory = GetComponent<BoxFactory>();
    }

    private void OnEnable()
    {
        OnKilled += MinionKilled;
    }

    private void OnDisable()
    {
        OnKilled -= MinionKilled;
    }

    public void SetTarget(Entity target)
    {
        _target = target;
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

    private void MinionKilled()
    {
        _boxFactory.CreateBox(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
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
