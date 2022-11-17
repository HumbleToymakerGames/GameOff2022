using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Shop/BuyFurniture")]
public class BuyFurnitureSO : ScriptableObject
{
    
    
    public SlotClass outputItem;

    

    public void BuyItem(NurseryShopManager nursery)
    {
        nursery.Add(outputItem.GetItem(), outputItem.GetQuantity());

    }

    
}
