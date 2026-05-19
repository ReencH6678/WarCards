using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthViewSlider : HealthView
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _health.Changed += UpdateHealthView;
    }

    private void OnDisable()
    {
        _health.Changed -= UpdateHealthView;
    }

    public override void UpdateHealthView(float count, float maxCount)
    {
        float divisor = 100;

        _slider.value = count / divisor;
        _slider.fillRect.gameObject.SetActive(count > 0);
    }
}
