using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatrolPoint
{
    public Transform point;
    public float waitTime;
}

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private List<PatrolPoint> _patrolPoints;

    private int _currentPointIndex = 0;
    private bool _isChasing = false;
    private Transform _playerTransform;
    private Rigidbody _rb;
    private Vector3 _currentTarget;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(PatrolRoutine());
    }

    private void FixedUpdate()
    {
        // Sigue al player
        if (_isChasing && _playerTransform != null)
        {
            _currentTarget = _playerTransform.position;
            MoveTowardsTarget(_currentTarget);
        }
        else
        {
            MoveTowardsTarget(_currentTarget);
        }
    }

    private IEnumerator PatrolRoutine()
    {
        // Siempre que no este chaseando
        while (true)
        {
            if (_isChasing) yield break;

            PatrolPoint currentPoint = _patrolPoints[_currentPointIndex];
            float sqrTolerance = 0.3f;

            _currentTarget = currentPoint.point.position;

            while ((_currentTarget - transform.position).sqrMagnitude > sqrTolerance)
            {
                if (_isChasing) yield break;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(currentPoint.waitTime);
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
        }
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude < 0.01f) return;

        Vector3 move = direction.normalized * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + move);

        Vector3 lookAt = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(lookAt);
    }


    public void ChasePlayer(Transform player)
    {
        _isChasing = true;
        _playerTransform = player;
        StopAllCoroutines();
    }
}
