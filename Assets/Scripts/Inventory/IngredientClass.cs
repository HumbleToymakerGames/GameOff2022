using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Ingredient Class", menuName = "Item/Ingredient")]
public class IngredientClass : ItemClass
{

    public override ItemClass GetItem() { return this; }
    public override ApplianceClass GetAppliance() { return null; }
    public override MiscClass GetMisc() { return null; }
    public override IngredientClass GetIngredient() { return this; }
    public override PlaceableObjectClass GetPlaceableObject() { return null; }


}
