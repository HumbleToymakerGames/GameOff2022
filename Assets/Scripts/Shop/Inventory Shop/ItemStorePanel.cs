using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    [SerializeField] Trading trading;
    public override void OnClick(int id)
    {
        BuyItem(id);
        Show();
    }

    private void BuyItem(int id)
    {
        trading.BuyItem(id);
    }
}
