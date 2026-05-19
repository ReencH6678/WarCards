using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

[RequireComponent(typeof(InventoryLoader), typeof(InventorySaver))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<UISlot> _slots;
    [SerializeField] private List<Card> _cards = new List<Card>();
    [SerializeField] private bool _needDefaultCards;

    private Dictionary<Card, UISlot> _deck = new Dictionary<Card, UISlot>();

    private Card _currentCard;

    private InventoryLoader _inventoryLoader;
    private InventorySaver _inventorySaver;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        _inventoryLoader = GetComponent<InventoryLoader>();
        _inventorySaver = GetComponent<InventorySaver>();

        if (_needDefaultCards)
        {
            YG2.SetDefaultSaves();
            _inventorySaver.SetDefaultCards();
        }

        foreach (UISlot slot in _slots)
            slot.Init();

        Load();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void OnEnable()
    {
        foreach (Card card in _cards)
        {
            card.CardDrager.Placed += HandleDrop;
            card.CardDrager.DragStarted += SetCurrentCard;
        }

        foreach (Card card in _deck.Keys)
        {
            card.CardDrager.Placed += HandleDrop;
            card.CardDrager.DragStarted += SetCurrentCard;
        }
    }

    private void OnDisable()
    {
        foreach (Card card in _cards)
        {
            card.CardDrager.Placed -= HandleDrop;
            card.CardDrager.DragStarted -= SetCurrentCard;
        }

        foreach (Card card in _deck.Keys)
        {
            card.CardDrager.Placed -= HandleDrop;
            card.CardDrager.DragStarted -= SetCurrentCard;
        }
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);

        _inventorySaver.SaveDeck(_deck.Keys.ToList());
        _inventorySaver.SaveInventory(_cards);

        YG2.SaveProgress();
    }

    private void HandleDrop(PointerEventData eventData)
    {
        GameObject dropObject = eventData.pointerCurrentRaycast.gameObject;

        if (dropObject.TryGetComponent<Card>(out Card card))
            HandleCardDrop(card);

        if (dropObject.TryGetComponent<UISlot>(out UISlot slot))
            HandleSlotDrop(slot);

        _inventorySaver.SaveDeck(_deck.Keys.ToList());
        _inventorySaver.SaveInventory(_cards);

        YG2.SaveProgress();
    }

    private void HandleCardDrop(Card card)
    {
        if (_currentCard.TryGetComponent<CardUniter>(out CardUniter cardUniter))
        {
            if (cardUniter.TryUniteCard(card))
            {
                card.CardDrager.Placed -= HandleDrop;
                card.CardDrager.DragStarted -= SetCurrentCard;

                _cards.Remove(card);
            }
        }
        else
        {
            _cards.Add(card);

            _deck[card].Put(_currentCard.RectTransform);

            _deck[_currentCard] = _deck[card];
            _cards.Remove(_currentCard);
            _deck.Remove(card);
        }
    }

    private void HandleSlotDrop(UISlot slot)
    {
        slot.Put(_currentCard.RectTransform);

        if (_deck.ContainsKey(_currentCard) == false)
            _deck.Add(_currentCard, slot);
        else
            _deck[_currentCard] = slot;

        _cards.Remove(_currentCard);
    }

    private void SetCurrentCard(Card card)
    {
        if (card != null)
            _currentCard = card;
    }

    private void Load()
    {
        List<Card> deckCards = _inventoryLoader.LoadDeck(_slots);

        for (int i = 0; i < deckCards.Count; i++)
            _deck.Add(deckCards[i], _slots[i]);

        foreach (Card card in _inventoryLoader.LoadInventory())
            _cards.Add(card);
    }
}
