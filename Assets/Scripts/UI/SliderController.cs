using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : MonoBehaviour
    {
        public event Action<float> TransparencyChanged;
        
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.onValueChanged.AddListener(ChangeTransparency);
        }

        private void ChangeTransparency(float arg0) => 
            TransparencyChanged?.Invoke(arg0);
    }
}