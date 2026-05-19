using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsHandler : MonoBehaviour
{
    [SerializeField]private Window _currentWinsow;

    public void Open(Window window)
    {
        if(_currentWinsow != null) 
            _currentWinsow.Close();

        window.Open();
        _currentWinsow = window;
    }
}
