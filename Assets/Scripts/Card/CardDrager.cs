using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minScale;

    private RectTransform _canvasTransform;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Transform _startTransporm;

    private bool _canSpawn;

    public Action<PointerEventData> Placed;
    public Action<Card> DragStarted;

    public Action OnStartDrag;
    public Action OnDragEnd;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Card>(out Card card))
            DragStarted?.Invoke(card);

        _startPosition = _rectTransform.position;
        _startScale = _rectTransform.localScale;

        _startTransporm = _rectTransform.parent;

        _rectTransform.SetParent(_canvasTransform, true);
        _rectTransform.SetAsLastSibling();

        _canvasGroup.blocksRaycasts = false;

        OnStartDrag?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
        Decrease();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.SetParent(_startTransporm, true);
        _rectTransform.position = _startPosition;
        _rectTransform.localScale = _startScale;

        _canvasGroup.blocksRaycasts = true;

        OnDragEnd?.Invoke();
        Placed?.Invoke(eventData);
    }

    public void SetCanvasTranform(RectTransform canvas)
    {
        _canvasTransform = canvas;
    }

    private void Decrease()
    {
        float sqrDistance = (_startPosition - _rectTransform.position).sqrMagnitude;

        float distanceRatio = Mathf.InverseLerp(0, _maxDistance * _maxDistance, sqrDistance);
        float scaleRotion = Mathf.Lerp(1f, _minScale, distanceRatio);

        _rectTransform.localScale = _startScale * scaleRotion;
    }

    private Vector3 GetWorldPosition()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return worldPosition;
    }
}