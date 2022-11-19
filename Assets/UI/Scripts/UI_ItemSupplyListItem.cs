using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemSupplyListItem : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemUnitPrice;
    public Button decreaseOrderButton;
    public Button increaseOrderButton;
    public TMP_InputField orderInputField;
    public TextMeshProUGUI totalPrice;

    private int _orderAmount = 0;
    private ItemClass _ingredient;


    public void ConfigureForIngredient(ItemClass ingredient)
    {
        _ingredient = ingredient;
        itemImage.sprite = _ingredient.itemIcon;
        itemName.text = _ingredient.itemName;
        itemUnitPrice.text = _ingredient.baseCost.ToString();
        UpdateQuantityCalculations();
    }

    private void UpdateQuantityCalculations()
    {
        orderInputField.text = _orderAmount.ToString();
        totalPrice.text = "$"+(_orderAmount * _ingredient.baseCost).ToString();
    }

    public void DecrementOrderQuantity()
    {
        if (_orderAmount == 0) return;
        _orderAmount--;
        UpdateQuantityCalculations();
    }

    public void IncrementOrderQuantity()
    {
        _orderAmount++;
        UpdateQuantityCalculations();
    }
}
