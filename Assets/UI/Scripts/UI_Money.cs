using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Money : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

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
        moneyText.text = "$" + newTotal.ToString();
    }
}
