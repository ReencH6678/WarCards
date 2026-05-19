using UnityEngine;

public class LevelButton : ButtonHandler
{
    [SerializeField] private Level _level;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private Window _menu;
    [SerializeField] private WindowsHandler _windowsHandler;

    public override void HandleClick()
    {
        _levelHandler.SetLevel(_level);
        _windowsHandler.Open(_menu);
    }
}
