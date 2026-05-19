using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class InventoryLoader : MonoBehaviour
{
    [SerializeField] private RectTransform _inventoryTransform;
    [SerializeField] private List<Card> _cardPrefabs;

    [SerializeField] private RectTransform _canvas;

    public List<Card> LoadInventory()
    {
        List<Card> cards = new List<Card>();

        foreach (CardSaveData data in YG2.saves.InventoryCards)
        {
            if (data == null)
                continue;

            Card prefabe = _cardPrefabs.First(card => card.Id == data.Id);
            Card loadedCard = Instantiate(prefabe, _inventoryTransform);

            loadedCard.Init(data);
            loadedCard.CardDrager.SetCanvasTranform(_canvas);

            cards.Add(loadedCard);
        }

        return cards;
    }

    public List<Card> LoadDeck(List<UISlot> slots)
    {
        List<Card> cards = new List<Card>();

        foreach (CardSaveData data in YG2.saves.DeckCards)
        {
            if (data == null)
                continue;

            Card prefabe = _cardPrefabs.First(card => card.Id == data.Id);
            Card loadedCard = Instantiate(prefabe);

            loadedCard.Init(data);
            loadedCard.CardDrager.SetCanvasTranform(_canvas);

            cards.Add(loadedCard);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                if (i < slots.Count)
                {
                    slots[i].Put(cards[i].RectTransform);
                }
            }
        }

        return cards;
    }
}
