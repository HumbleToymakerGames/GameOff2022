using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemQuantityUIElement : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;

    public void SetToItemQuantity(ItemQuantity itemQuantity)
    {
        itemImage.sprite = itemQuantity.item.itemIcon;
        itemQuantityText.text = itemQuantity.itemQuantity.ToString();
    }
}
