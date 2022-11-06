using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemQuantity
{
    public Item item;
    public int itemQuantity;
}

[CreateAssetMenu(fileName = "NewApplianceFunction", menuName = "ScriptableObjects/Appliance/Function")]
public class ApplianceFunctionSO : ScriptableObject
{
    public string functionName;
    public ItemQuantity[] inputs;
    public ItemQuantity[] outputs;
    public float hoursToMake;
}
