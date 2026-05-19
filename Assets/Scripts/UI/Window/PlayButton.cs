using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonHandler
{
    [SerializeField] private LevelHandler _levelHandler;

    public override void HandleClick()
    {
        _levelHandler.Play();    
    }
}
