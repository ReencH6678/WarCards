using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardsData : MonoBehaviour
{
    [SerializeField] private List<Card> _prefabs = new List<Card>();

    public Card GetCard(int id)
    {
        foreach(Card card in _prefabs)
        {
            if(card.Id == id) 
                return card;
        }

        return null;
    }
}
