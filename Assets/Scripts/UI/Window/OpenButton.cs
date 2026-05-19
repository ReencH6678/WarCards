using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : ButtonHandler
{
    [SerializeField] private Window _window;
    [SerializeField] private WindowsHandler _windowsHandler;

    public override void HandleClick()
    {
        _windowsHandler.Open(_window);
    }
}
