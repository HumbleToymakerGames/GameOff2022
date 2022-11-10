using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlaceableObject", menuName = "ScriptableObjects/PlaceableObject")]
public class PlaceableObjectSO : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public TileType type;
    public ApplianceSO applianceSOIfApplicable;
    public Vector3Int usePositionOffsetIfApplicable;
}
