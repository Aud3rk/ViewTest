using System;
using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;

public class ItemController
{
    public event Action<bool> SelectAllEvent;
        
    private readonly SliderController _sliderController;
    private readonly ToggleController _visibilityToggleController;

    private readonly List<GameObject> _selectedItems;
    private readonly List<GameObject> _items;

    private readonly DataSaver _dataSaver;

    public ItemController(SliderController sliderController, ToggleController visibilityToggleController)
    {
        _items = new List<GameObject>();
        _selectedItems = new List<GameObject>();
        _dataSaver = new DataSaver();

        _sliderController = sliderController;
        _visibilityToggleController = visibilityToggleController;

        _sliderController.TransparencyChanged += ChangeTransparency;
        _visibilityToggleController.ToggleChanged += VisibleItems;
    }

    public void AddItem(GameObject gameObject) =>
        _items.Add(gameObject);

    public void SelectItem(GameObject gameObject)
    {
        _selectedItems.Add(gameObject);
        if (_selectedItems.Count.Equals(_items.Count)) 
            SelectAllEvent?.Invoke(true);
    }

    public void UnselectItem(GameObject gameObject)
    {
        if (_selectedItems.Exists(x=>x.Equals(gameObject))) 
            _selectedItems.Remove(gameObject);
    }

    public void ChangedSelected(bool value)
    {
        if (value) SelectAllItems();
        else UnselectItems();
        SaveAllItems();
    }

    private void UnselectItems()
    {
        if (_selectedItems.Count>0) 
            _selectedItems.RemoveAll(x => x != null);
    }

    private void SelectAllItems()
    {
        if (_selectedItems.Count.Equals(_items.Count)) return;
        foreach (GameObject item in _items.FindAll(x => _selectedItems.Exists(y => y.Equals(x))))
            SelectItem(item);
    }

    private void VisibleItems(bool obj)
    {
        foreach (GameObject item in _selectedItems) 
            item.SetActive(obj);
        SaveAllItems();
    }

    private void ChangeTransparency(float value)
    {
        foreach (GameObject item in _selectedItems)
        {
            Color color = item.GetComponent<Renderer>().material.color;
            color.a = value;
            item.GetComponent<Renderer>().material.color = color;
        }
        SaveAllItems();

            
    }

    private void SaveAllItems()
    {
        List<ItemData> list = new List<ItemData>();
        foreach (GameObject item in _items)
        {
            var data = new ItemData();
            var position = item.transform.position;
            data.Position = new Vector3Modify(position.x, position.y,
                position.z);
            data.Visibility = item.GetComponent<Renderer>().material.color.a;
            data.IsActive = item.activeSelf;
            data.Id = item.GetComponent<InteractionItem>().Id;
            list.Add(data);
        }

        _dataSaver.SaveData(list);
    }

    ~ItemController()
    {
        _sliderController.TransparencyChanged -= ChangeTransparency;
        _visibilityToggleController.ToggleChanged -= VisibleItems;
    }
}