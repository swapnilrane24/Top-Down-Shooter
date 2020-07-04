using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TopDownShooter
{
    public abstract class BarScript : MonoBehaviour
    {
        [SerializeField] protected Image barImage;

        protected virtual void Awake()
        {
            barImage.fillAmount = 1;
        }

        public virtual void SetValue(float value)
        {
            barImage.DOFillAmount(value, 0.2f);
        }
    }
}