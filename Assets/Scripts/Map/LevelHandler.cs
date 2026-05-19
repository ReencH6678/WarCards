using System;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Level _defueltLevel;

    private Level _level;

    public event Action<Level> OnLevelChange;

    private void Awake()
    {
        _level = _defueltLevel;
        OnLevelChange?.Invoke(_level);
    }
    public void SetLevel(Level level)
    {
        if (level == null)
            return;

        _level = level;
        OnLevelChange?.Invoke(_level);
    }

    public void Play()
    {
        _level.Load();
    }
}
