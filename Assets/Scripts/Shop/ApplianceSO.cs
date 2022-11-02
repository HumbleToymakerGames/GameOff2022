using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAppliance", menuName = "ScriptableObjects/Appliance/Appliance")]
public class ApplianceSO : ScriptableObject
{
    public string applianceName;
    public ApplianceFunctionSO[] functions;
}
