using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ItemQuantityElement : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;

    public void SetToItemQuantity(SlotClass itemQuantity)
    {
        if (itemQuantity.GetItem() != null) itemImage.sprite = itemQuantity.GetItem().itemIcon;
        itemQuantityText.text = itemQuantity.GetQuantity().ToString();
    }
}
