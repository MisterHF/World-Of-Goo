using UnityEngine;
using UnityEngine.InputSystem;
public class CameraDrag : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private  Bounds _cameraBounds;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0F),
            new Vector3(maxX, maxY, 0.0f)
            );
    }
    public void OnDrag(InputAction.CallbackContext context) 
    {
        if(context.started) _origin = _mainCamera.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
        _isDragging = context.started || context.performed;
    }

    private void LateUpdate()
    {
        if(!_isDragging) return;

        _difference = GetMousePosition - transform.position;
        
        _targetPosition = _origin - _difference;
        _targetPosition = GetCameraBounds();
        
        transform.position = _targetPosition;
    }

    public Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
            );
    }
    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
}
