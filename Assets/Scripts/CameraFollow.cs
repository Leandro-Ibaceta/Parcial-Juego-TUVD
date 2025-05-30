using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _initialOffset;

    private void Start()
    {
        if (_target == null)
        {
            enabled = false;
            return;
        }

        _initialOffset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _initialOffset;
        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
    }
}
