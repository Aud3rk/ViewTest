using System;
using UnityEngine;

public class InteractionItem : MonoBehaviour
{
    public event Action<InteractionItem> OnClickEvent;
    public event Action<bool> ChangeActivity;
    public int Id { get; set; }

    private void OnMouseDown() => 
        OnClickEvent?.Invoke(this);

    private void OnEnable() => 
        ChangeActivity?.Invoke(true);

    private void OnDisable() => 
        ChangeActivity?.Invoke(false);
        
        
}