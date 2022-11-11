using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewApplianceFunction", menuName = "ScriptableObjects/Appliance/Function")]
public class ApplianceFunctionSO : ScriptableObject
{
    public string functionName;
    public List<SlotClass> inputItems = new List<SlotClass>();
    public SlotClass outputItem;
    public float hoursToMake;
    public bool manual;
}
