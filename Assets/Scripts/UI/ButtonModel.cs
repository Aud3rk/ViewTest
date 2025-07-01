using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ButtonModel : MonoBehaviour
    {
        public event Action<GameObject> ItemSelected; 
        public event Action<GameObject> ItemUnselected; 

        [SerializeField] private ToggleController toggleController;
        [SerializeField] private GameObject mark;
        [SerializeField] private TMP_Text name;
        
        private InteractionItem _interactableObject;

        private void Start() => 
            toggleController.ToggleChanged += ButtonClicked;

        public void SetObject(InteractionItem gameObject)
        {
            _interactableObject = gameObject;
            _interactableObject.ChangeActivity += MarkVisibility;
        }

        public void ChangeSelection(bool obj) => 
            toggleController.ChangeToggle(obj);

        public void SetButtonName(string buttonName) => 
            name.text = buttonName.Substring(0, 3);

        private void MarkVisibility(bool value) => 
            mark.SetActive(value);

        private void ButtonClicked(bool obj)
        {
            if (obj) ItemSelected?.Invoke(_interactableObject.gameObject);
            else ItemUnselected?.Invoke(_interactableObject.gameObject);
        }

        private void OnDisable() => 
            _interactableObject.ChangeActivity -= MarkVisibility;
    }
}