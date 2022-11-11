using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryItemSingleElement : MonoBehaviour
{
    public Image image;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
