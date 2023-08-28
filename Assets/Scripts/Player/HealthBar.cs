using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetSlider(int amount) {
        healthSlider.value = amount;
    }

    public void SetSliderMax(int amount) {
        healthSlider.maxValue = amount;
        SetSlider(amount);
    }
}
