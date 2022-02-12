using System;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    protected Vector3 _previousCameraPosition;
    [SerializeField] protected Camera _camera;
    protected Transform _cameraTransform;
    protected Vector3 _difference;
    public Vector2 Speed;

    public bool MoveOppositeDirection;

    protected void OnEnable()
    {
        if (_camera != null)
        {
            _cameraTransform = _camera.transform;
            _previousCameraPosition = _cameraTransform.position;
        }
    }

    protected void LateUpdate()
    {
        ProcessParallax();
    }   

    private void ProcessParallax()
    {
        _difference = _cameraTransform.position - _previousCameraPosition;
        float direction = (MoveOppositeDirection) ? -1f : 1f;
        transform.position += Vector3.Scale(_difference, Speed) * direction;
        _previousCameraPosition = _cameraTransform.position;
    }
}
