using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Misc Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{

    public override ItemClass GetItem() { return this; }
    public override ApplianceClass GetAppliance() { return null; }
    public override MiscClass GetMisc() { return this; }
    public override IngredientClass GetIngredient() { return null; }
    public override PlaceableObjectClass GetPlaceableObject() { return null; }
}
