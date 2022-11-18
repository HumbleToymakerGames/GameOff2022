using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBuy : MonoBehaviour
{
    public ItemClass itemToAdd;
    
    public void AddItem()
    {
        InventoryManager.Instance.Add(itemToAdd, 1);
    }
}
