using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWindow : Window
{
    [SerializeField] private Transform _cardsSlot;

    public void SetWinCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
          Card createdCard =  Instantiate(card, _cardsSlot.position, Quaternion.identity);
            createdCard.transform.parent = _cardsSlot;
            createdCard.transform.localScale = Vector3.one;
        }
    }
}
