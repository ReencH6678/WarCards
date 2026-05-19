using UnityEngine;
using TMPro;

public class HealthViewText : HealthView
{
    [SerializeField] private TextMeshProUGUI _haelthCountText;

    public override void UpdateHealthView(float healthCount, float maxHealthCount)
    {
        if (healthCount >= 0)
            _haelthCountText.text = $"{healthCount} / {maxHealthCount}";
    }
}
