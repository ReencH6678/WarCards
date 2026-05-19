using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Transform _cardScrolView;

    private RectTransform _currentCard;
    private RectTransform _rectTransform;

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Put(RectTransform card)
    {
        if (_currentCard != null)
                _currentCard.SetParent(_cardScrolView);

        card.position = _rectTransform.position;
        card.SetParent(_rectTransform);

        _currentCard = card;
    }
}
