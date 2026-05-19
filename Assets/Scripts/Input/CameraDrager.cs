using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(InputHandler))]
public class CameraDrager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _mapCollider;
    [SerializeField] private float _speed;

    private Bounds _mapBounds;

    private InputHandler _inputHandler;

    private Vector3 _dragStartPosition;
    private bool _isDragging;

    private void Awake()
    {
        _mapBounds = _mapCollider.bounds;
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        if (_inputHandler.IsLeftMousButtonDown && _isDragging == false)
        {
            Vector3 clickPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _isDragging = true;
            _dragStartPosition = clickPostion;

        }

        if (_isDragging && _inputHandler.IsLeftMousButtonPressed && _inputHandler.IsCardDragging == false)
        {
            Vector3 currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = _dragStartPosition - currentDragPosition;

            Vector3 nextPosition = transform.position + difference * _speed;

            float cameraHeight = Camera.main.orthographicSize * 2;
            float cameraWidth = cameraHeight * Camera.main.aspect;

           

            nextPosition.x = Mathf.Clamp(nextPosition.x, _mapBounds.min.x + cameraWidth, _mapBounds.max.x - cameraWidth);
            nextPosition.y = Mathf.Clamp(nextPosition.y, _mapBounds.min.y + cameraHeight, _mapBounds.max.y - cameraHeight);

            transform.position = nextPosition;
        }

        if (_inputHandler.IsLeftMousButtonUp)
            _isDragging = false;
    }
}
