using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardViewer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private TextMeshProUGUI _price;

    public void Load(Card card)
    {
        _image.sprite = card.UnitSprite;
        _lvl.text = card.Level.ToString();
        _price.text = card.Price.ToString();

    }
}
