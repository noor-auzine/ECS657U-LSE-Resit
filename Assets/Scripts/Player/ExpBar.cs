using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField]
    public Slider expSlider;

    public void SetSlider(int amount) {
        expSlider.value = amount;
    }

    public void SetSliderMax(int amount) {
        expSlider.maxValue = amount;
        SetSlider(amount);
    }
}
