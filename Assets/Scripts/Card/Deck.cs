using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> _prefabs;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Card> _hand;

    [SerializeField] private List<Slot> _slots;

    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Map _map;

    [SerializeField] private int _maxCardsCount;

    public bool IsCardDragging { get; private set; }

    private void Awake()
    {
        LoadCards();
        
        if(_maxCardsCount > _cards.Count)
            _maxCardsCount = _cards.Count;
    }

    private void Start()
    {
        IsCardDragging = false;
        TakeCards(_maxCardsCount);
    }


    public void TakeCards(int count)
    {
        if (_hand.Count < _maxCardsCount)
        {
            for (int i = 0; i < count; i++)
            {
                Card card = GetNewCard();
                Slot slot = GetFreeSlot();

                if (card == null)
                    continue;

                if (slot != null)
                {
                    card = Instantiate(card, slot.transform, false);

                    card.Droped += PlayCard;
                    card.CardDrager.OnStartDrag += SetDragging;
                    card.CardDrager.OnDragEnd += RemoveDragging;
                    slot.Put();
                }

                if (card == null)
                    return;

                _hand.Add(card);
            }
        }
    }

    public void PlayRandomCards(Vector3 position, int randomPrice)
    {
        List<Card> playableCards = _hand.Where(card => card.Price <= _wallet.Count).ToList();

        while (playableCards.Count > 0)
        {
            Card randomCard = playableCards[Random.Range(0, playableCards.Count)];

            SpawnCard(position, randomCard);

            playableCards = _hand.Where(card => card.Price <= _wallet.Count).ToList();
        }

    }

    private void SpawnCard(Vector3 spawnPosition, Card card)
    {
        for (int i = 0; i < card.UnitsCount; i++)
            _spawner.Spawn(spawnPosition + GetRandomOffset(), card.UnitPrefabe);

        _hand.Remove(card);
        _wallet.Remove(card.Price);

        TakeCards(1);
    }

    private void PlayCard(Vector3 spawnPosition, Card card)
    {
        if (_map.CanSpawn(spawnPosition))
        {
            if (_wallet.Count >= card.Price)
            {
                if (card.transform.parent.TryGetComponent<Slot>(out Slot slot))
                    slot.Pull();

                card.Droped -= PlayCard;
                card.CardDrager.OnStartDrag -= SetDragging;
                card.CardDrager.OnDragEnd -= RemoveDragging;

                SpawnCard(spawnPosition, card);
                Destroy(card.gameObject);
            }
        }
    }

    private Card GetNewCard()
    {
        List<Card> availableCards = _cards
            .Where(card => _hand.Any(handCard => handCard.Id == card.Id) == false)
            .ToList();

        if (availableCards.Count == 0)
            return null;

        return availableCards[Random.Range(0, availableCards.Count)];
    }

    private Slot GetFreeSlot()
    {
        foreach (Slot slot in _slots)
            if (slot != null && slot.IsFull == false)
            return slot;

        return null;
    }

    private Vector3 GetRandomOffset()
    {
        float minOffset = 0;
        float maxOffset = 0.5f;

        return new Vector3(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset));
    }

    private void SetDragging()
    {
        IsCardDragging = true;
    }

    private void RemoveDragging()
    {
        IsCardDragging = false;
    }

    private void LoadCards()
    {
        List<CardSaveData> loadedData = new List<CardSaveData>();

        foreach (CardSaveData data in YG2.saves.DeckCards)
        {
            foreach (Card prefabe in _prefabs)
            {
                if (prefabe.Id == data.Id)
                {
                    if (loadedData.Contains(data) == false)
                    {
                        loadedData.Add(data);
                        Card card = Instantiate(prefabe);
                        _cards.Add(card);
                        card.Init(data);
                    }
                }
            }
        }
    }
}
