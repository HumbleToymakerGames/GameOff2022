using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ItemQuantityElement : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;

    public void SetToItemQuantity(ItemQuantity itemQuantity)
    {
        if (itemQuantity.item != null) itemImage.sprite = itemQuantity.item.itemIcon;
        itemQuantityText.text = itemQuantity.itemQuantity.ToString();
    }
}
