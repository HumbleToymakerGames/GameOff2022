using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemQuantity
{
    public ItemClass item;
    public int itemQuantity;
}

[CreateAssetMenu(fileName = "NewApplianceFunction", menuName = "ScriptableObjects/Appliance/Function")]
public class ApplianceFunctionSO : ScriptableObject
{
    //public SlotClass[] inputItems;
    //public SlotClass outputItem;



    public string functionName;
    public List<ItemQuantity> inputs = new List<ItemQuantity>();
    public List<ItemQuantity> outputs = new List<ItemQuantity>();
    public float hoursToMake;
    public bool manual;
}
