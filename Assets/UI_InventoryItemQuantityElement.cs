using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventoryItemQuantityElement : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI quantityText;

    private void Start()
    {
        image = GameObject.Find("Image").GetComponent<Image>();
        quantityText = GameObject.Find("Quantity").GetComponent<TextMeshProUGUI>();
    }

    public void ConfigureForItem(SlotClass itemQuantity)
    {
        if (itemQuantity == null) return;
        image.sprite = itemQuantity.GetItem().itemIcon;
        quantityText.text = itemQuantity.GetQuantity().ToString();

    }
}
