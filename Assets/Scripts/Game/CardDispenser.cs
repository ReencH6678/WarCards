using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CardDispenser : MonoBehaviour
{
    [SerializeField] private List<Card> _winCards;
    [SerializeField] private int _count;

    public List<Card> DispendPrize()
    {
        List<Card> cards = new List<Card>();

        Debug.Log(_count);

        for (int i = 0; i < _count; i++)
        {
            Card card = _winCards[Random.Range(0, _winCards.Count)];
            YG2.saves.InventoryCards.Add(card.GetSaveData());

            cards.Add(card);
        }

        YG2.SaveProgress();

        return cards;
    }
}
