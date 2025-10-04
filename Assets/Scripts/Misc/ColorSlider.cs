using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour {
    [SerializeField] Image fillImage;

    public void UpdateSliderColor() {
        float lerpValue = gameObject.GetComponent<Slider>().value / gameObject.GetComponent<Slider>().maxValue;

        Color lerpColor = Color.Lerp(Color.red, Color.green, lerpValue);
        fillImage.color = lerpColor;
    }
}
