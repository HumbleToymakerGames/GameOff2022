using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]

public class Item : ScriptableObject
{

    public string itemName;
    public bool stackable;
    public Sprite itemIcon;
}
