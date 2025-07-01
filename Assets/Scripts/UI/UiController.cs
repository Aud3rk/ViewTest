using System.Collections.Generic;
using Camera;
using Data;
using UnityEngine;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private GameObject buttonCollection;
        [SerializeField] private SliderController sliderController;
        [SerializeField] private ToggleController visibilityToggleController;
        [SerializeField] private ToggleController selectAllToggleController;
        
        private SpawnerController _spawnerController;
        private CameraController _cameraController;

        private List<ButtonModel> _buttonList;
        private ItemController _itemController;
        private DataSaver _dataSaver;

        public void Init(SpawnerController spawnerController, CameraController cameraController)
        {
            _itemController = new ItemController(sliderController, visibilityToggleController);
            _buttonList = new List<ButtonModel>();
            _dataSaver = new DataSaver();

            _spawnerController = spawnerController;
            _cameraController = cameraController;
            
            selectAllToggleController.ToggleChanged += _itemController.ChangedSelected;
            _itemController.SelectAllEvent += selectAllToggleController.ChangeToggle;
        }

        public void LoadButtons()
        {
            Data.Data data = _dataSaver.LoadData();
            if (data.List != null) LoadLevel(data);
            else LoadLevel();
        }

        private void LoadLevel(Data.Data data)
        {
            foreach (var item in data.List)
            {
                InteractionItem itemGameObject = _spawnerController.InstantiateItem(item);
                ButtonModel buttonGameObject = _spawnerController.InstantiateButton(buttonCollection.transform);

                _itemController.AddItem(itemGameObject.gameObject);
                _buttonList.Add(buttonGameObject);
                _cameraController.SubscribeItem(itemGameObject);

                buttonGameObject.SetObject(itemGameObject);
                buttonGameObject.SetButtonName(itemGameObject.name);
                buttonGameObject.ItemSelected += _itemController.SelectItem;
                buttonGameObject.ItemUnselected += _itemController.UnselectItem;

                selectAllToggleController.ToggleChanged += buttonGameObject.ChangeSelection;
            }
        }

        private void LoadLevel()
        {
            for (int i = 0; i < 5; i++)
            {
                InteractionItem itemGameObject = _spawnerController.InstantiateItem();
                ButtonModel buttonGameObject = _spawnerController.InstantiateButton(buttonCollection.transform);

                _itemController.AddItem(itemGameObject.gameObject);
                _buttonList.Add(buttonGameObject);
                _cameraController.SubscribeItem(itemGameObject);

                buttonGameObject.SetObject(itemGameObject);
                buttonGameObject.SetButtonName(itemGameObject.name);
                buttonGameObject.ItemSelected += _itemController.SelectItem;
                buttonGameObject.ItemUnselected += _itemController.UnselectItem;

                selectAllToggleController.ToggleChanged += buttonGameObject.ChangeSelection;
            }
        }

        private void OnDisable() => 
            UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            selectAllToggleController.ToggleChanged -= _itemController.ChangedSelected;
            _itemController.SelectAllEvent -= selectAllToggleController.ChangeToggle;

            foreach (ButtonModel button in _buttonList)
            {
                button.ItemSelected -= _itemController.SelectItem;
                button.ItemUnselected -= _itemController.UnselectItem;

                selectAllToggleController.ToggleChanged -= button.ChangeSelection;
            }
        }
    }
}