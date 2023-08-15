using UnityEngine;
using UnityEngine.UI;

namespace dragoni7
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public void SetMaxHealth(float health) => slider.maxValue = health;
        public void SetHealth(float health) => slider.value = health;
    }
}
