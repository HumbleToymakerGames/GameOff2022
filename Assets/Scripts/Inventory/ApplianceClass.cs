using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Appliance")]
public class ApplianceClass : ItemClass
{
    public string applianceName;
    public List<ApplianceFunctionSO> functions = new List<ApplianceFunctionSO>();



    public override ItemClass GetItem() { return this; }
    public override ApplianceClass GetAppliance() { return this; }
    public override MiscClass GetMisc() { return null; }
    public override IngredientClass GetIngredient() { return null; }
    public override PlaceableObjectClass GetPlaceableObject() { return null; }
    

}
