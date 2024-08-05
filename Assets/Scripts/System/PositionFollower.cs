using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _offsetPosition;

    private void Awake()
    {
        SetTarget(_followTarget);
    }

    private void SetTarget(Transform target)
    {
        _followTarget = target;
    }

    private void LateUpdate()
    {
        var targetPosition = _followTarget.position + _offsetPosition;
        transform.position = targetPosition;
    }

}
