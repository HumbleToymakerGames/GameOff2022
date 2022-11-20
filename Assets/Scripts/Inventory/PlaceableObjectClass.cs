using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Placeable Object Class", menuName = "Item/PlaceO")]
public class PlaceableObjectClass : ItemClass
{
    public string name;
    public Sprite sprite;
    public TileType type;
    public bool supportsDeskItems;
    public int pixelHeight = 32;
    public Mask placementMask;
    public ApplianceSO applianceSOIfApplicable;
    public Vector3Int usePositionOffsetIfApplicable;

    public bool lightEmitter = false;
    public Vector3 lightOffset;
    public float lightIntensity;
    public Color lightColor;


    public override ItemClass GetItem() { return this; }
    public override ApplianceClass GetAppliance() { return null; }
    public override MiscClass GetMisc() { return null; }
    public override IngredientClass GetIngredient() { return null; }
    public override PlaceableObjectClass GetPlaceableObject() { return this; }
}
