using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEditMode : MonoBehaviour
{
    public GameObject ovenPrefab;
    private GameObject heldObject;

    private bool loadedObjects = false;

    private int selectedObject = 0;

    private IList<GameObject> placeableObjects = new List<GameObject>();
    public void UpdateCall()
    {
        if (!loadedObjects)
        {
            placeableObjects = Resources.LoadAll<GameObject>("Prefabs/PlaceableObjects");
        }

        //Object selection
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedObject++;
            if (heldObject != null)
                Destroy(heldObject);
            heldObject = null;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject != null)
                Destroy(heldObject);
            heldObject = null;
            selectedObject--;
        }

        if (selectedObject >= placeableObjects.Count)
            selectedObject = 0;
        else if (selectedObject < 0)
            selectedObject = placeableObjects.Count - 1;


        if (heldObject == null)
        {
            heldObject = Instantiate(placeableObjects[selectedObject]);
        }
        //else if (heldObject != null && Input.GetKeyDown(KeyCode.B))
        //{
        //    Destroy(heldObject);
        //    heldObject = null;
        //}
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
