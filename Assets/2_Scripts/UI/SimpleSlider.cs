using UnityEngine;
using UnityEngine.UI;

public class SimpleSlider : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void Fill(float percent)
    {
        fillImage.fillAmount = percent;
    }
}
