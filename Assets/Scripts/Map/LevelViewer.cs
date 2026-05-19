using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private Image _levelImage;
    [SerializeField] private LevelHandler _levelHandler;

    private void OnEnable()
    {
        _levelHandler.OnLevelChange += View;
    }

    private void OnDisable()
    {
        _levelHandler.OnLevelChange -= View;
    }

    public void View(Level level)
    {
        _levelImage.sprite = level.Image;
    }
}
