using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [Header("Borders")] 
    [SerializeField] private Transform leftHighPoint;
    [SerializeField] private Transform rightHighPoint;
        
    [Header("Prefabs")]
    [SerializeField] private List<InteractionItem> itemsList;
    [SerializeField] private ButtonModel buttonPrefab; 

    public InteractionItem InstantiateItem()
    {
        int id = GetRandomId(itemsList.Count);
        InteractionItem item = Instantiate(itemsList[id]);
        item.Id = id;
        item.transform.position = GetRandomPos(leftHighPoint.position, rightHighPoint.position);
        return item;
    }

    public InteractionItem InstantiateItem(ItemData itemData)
    {
        InteractionItem item= Instantiate(itemsList[itemData.Id]);
        item.transform.position = new Vector3(itemData.Position.X, itemData.Position.Y, itemData.Position.Z);
        Color color = item.GetComponent<Renderer>().material.color;
        color.a = itemData.Visibility;
        item.GetComponent<Renderer>().material.color = color;
        item.gameObject.SetActive(itemData.IsActive);
        item.Id = itemData.Id;
        return item;
    }

    public ButtonModel InstantiateButton(Transform buttonCollectionTransform) =>
        Instantiate(buttonPrefab, buttonCollectionTransform);
    private int GetRandomId(int Max)
    {
        var random = new System.Random();
        return random.Next(Max);
    }

    public Vector3 GetRandomPos(Vector3 leftLowPos, Vector3 rightHighPos)
    {
        var random = new System.Random();
        int x = random.Next((int)leftLowPos.x,(int) rightHighPos.x);
        int y = random.Next((int)leftLowPos.y,(int) rightHighPos.y);
        int z = random.Next((int)leftLowPos.z,(int) rightHighPos.z);
        return new Vector3(x, y, z);

    }
}