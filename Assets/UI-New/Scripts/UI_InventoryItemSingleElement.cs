using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventoryItemSingleElement : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI xElement;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetDoesHave(bool doesHave)
    {
        if (doesHave == true)
        {
            xElement.gameObject.SetActive(false);
        } else
        {
            xElement.gameObject.SetActive(true);
        }
    }

}
