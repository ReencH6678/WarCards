using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int LeftMouseButtonIndex = 0;

    [SerializeField] private Deck _cardDrager;

    public bool IsLeftMousButtonPressed { get; private set; }
    public bool IsLeftMousButtonDown { get; private set; }
    public bool IsLeftMousButtonUp { get; private set; }
    public bool IsCardDragging { get; private set; }

    private void Update()
    {
        IsLeftMousButtonPressed = Input.GetMouseButton(LeftMouseButtonIndex);
        IsLeftMousButtonUp = Input.GetMouseButtonUp(LeftMouseButtonIndex);
        IsLeftMousButtonDown = Input.GetMouseButtonDown(LeftMouseButtonIndex);

        IsCardDragging = _cardDrager.IsCardDragging;
    }
}
