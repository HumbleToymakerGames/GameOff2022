using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    public bool isSellable = false;
    public int baseCost = 0;

    public abstract ItemClass GetItem();
    public abstract ApplianceClass GetAppliance();
    public abstract MiscClass GetMisc();
    public abstract IngredientClass GetIngredient();
    public abstract PlaceableObjectClass GetPlaceableObject();

}
