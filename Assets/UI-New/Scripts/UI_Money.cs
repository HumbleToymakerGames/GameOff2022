using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Money : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _moneyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventHandler.ShopMoneyDidChangeEvent += ConfigureMoneyUIForMoney;
    }

    private void OnDisable()
    {
        EventHandler.ShopMoneyDidChangeEvent -= ConfigureMoneyUIForMoney;
    }

    private void ConfigureMoneyUIForMoney(int netChange, int newTotal)
    {
        _moneyText.text = "$" + newTotal.ToString();
    }
}
