using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectButton : MonoBehaviour
{
    public PlaceableObjectClass placeableObjectSO;
    public ItemClass item;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = placeableObjectSO.sprite;
    }
}
