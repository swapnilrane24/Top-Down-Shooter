using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TopDownShooter
{
    public class HealthBarScript : MonoBehaviour
    {
        [SerializeField] private Image barImage;

        private void Awake()
        {
            barImage.fillAmount = 1;
        }

        public void SetValue(float value)
        {
            barImage.DOFillAmount(value, 0.2f);
        }
    }
}