using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;

    }


    //sets item in slot
    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.itemIcon;

        if (slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }


    //removes sprite and hides icon and text
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.gameObject.SetActive(false);
    }
}
