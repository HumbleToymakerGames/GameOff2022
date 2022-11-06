using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool placed = false;
    public bool placeOnStart = false;

    public void Start()
    {
        if (placeOnStart)
        {
            PlaceObject();
        }
    }

    public void PlaceObject()
    {
        placed = true;

        MapInformation.SetTileWalkability(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), false);
    }

    public void Update()
    {
        if (placed == false && Player.controlState != ControlState.Edit)
        {
            Destroy(gameObject);
        }
    }
}
