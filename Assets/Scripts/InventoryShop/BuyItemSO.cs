using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shop", menuName = "Shop/BuyItem")]
public class BuyItemSO : ScriptableObject
{
    public List<SlotClass> outputItems = new List<SlotClass>();
    //public SlotClass outputItem;

   
}
