using System.Collections.Generic;
using UnityEngine;
using YG;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private List<Card> _defaultCards;

    public void SaveDeck(List<Card> cards)
    {
        YG2.saves.DeckCards.Clear();

        foreach (var card in cards)
        {
            YG2.saves.DeckCards.Add(card.GetSaveData());
        }
    }

    public void SaveInventory(List<Card> cards)
    {
        YG2.saves.InventoryCards.Clear();

        foreach (var card in cards)
        {
            if(card == null)
                continue;

            YG2.saves.InventoryCards.Add(card.GetSaveData());
        }
    }

    public void SetDefaultCards()
    {
        YG2.saves.InventoryCards.Clear();

        foreach (Card card in _defaultCards)
        {
            YG2.saves.InventoryCards.Add(card.GetSaveData());
        }
    }
}
