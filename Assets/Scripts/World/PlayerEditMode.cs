using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEditMode : MonoBehaviour
{
    public GameObject ovenPrefab;
    private GameObject heldObject;
    public void UpdateCall()
    {
        if (heldObject == null && Input.GetKeyDown(KeyCode.B))
        {
            heldObject = Instantiate(ovenPrefab);
        }
        else if (heldObject != null && Input.GetKeyDown(KeyCode.B))
        {
            Destroy(heldObject);
            heldObject = null;
        }
        else if (heldObject != null)
        {
            heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse() + new Vector3Int(1, 1, 0));
            if (Input.GetMouseButtonDown(0))
            {
                heldObject.GetComponent<PlaceableObject>().PlaceObject();
                heldObject = null;
            }
        }
    }
}
