using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleController : MonoBehaviour
    {
        public event Action<bool> ToggleChanged; 

        [SerializeField] private Toggle toggle;

        private void Start() => 
            toggle.onValueChanged.AddListener(InvokeEvent);

        private void InvokeEvent(bool arg0) => 
            ToggleChanged?.Invoke(arg0);

        public void ChangeToggle(bool value) => 
            toggle.isOn = value;
    }
}