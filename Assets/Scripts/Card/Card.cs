using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardDrager), typeof(CardUniter))]
public class Card : MonoBehaviour
{
    private CardDrager _cardDrager;
    private RectTransform _rectTransform;
    private UICardViewer _viewer;
    private CardUniter _cardUniter;

    public event Action<Vector3, Card> Droped;

    [field: SerializeField] public Unit UnitPrefabe { get; private set; }
    [field: SerializeField] public Sprite UnitSprite { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int UnitsCount { get; private set; }
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public int Level { get; private set; }

    public CardUniter CardUniter => _cardUniter;
    public CardDrager CardDrager => _cardDrager;
    public RectTransform RectTransform => _rectTransform;

    private void Awake()
    {
        _cardDrager = GetComponent<CardDrager>();
        Init(GetSaveData());    
    }

    private void OnEnable()
    {
        _cardDrager.Placed += Drop;
    }

    public void Init(CardSaveData data)
    {
        _viewer = GetComponent<UICardViewer>();
        _rectTransform = GetComponent<RectTransform>();
        _cardUniter = GetComponent<CardUniter>();

        Level = data.Level;
        Price = data.Price;

        
        _viewer.Load(this);
        _rectTransform.localScale = Vector3.one;
    }

    public void SetSlot(Vector3 slotPosition)
    {
        _rectTransform.localPosition = slotPosition;
    }

    public CardSaveData GetSaveData()
    {
        return new CardSaveData { Id = Id, Level = Level, Price = Price };
    }

    public void AddLevel()
    {
        Level++;
        _viewer.Load(this);
    }

    private void Drop(PointerEventData data)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
            Droped?.Invoke(hit.point, this);
    }
}
