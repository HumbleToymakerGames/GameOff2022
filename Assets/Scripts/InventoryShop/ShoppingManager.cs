using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingManager : MonoBehaviour
{
    //[SerializeField] private List<BuyItemSO> BuyItemSO = new List<BuyItemSO>();
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private SlotClass[] supplierIngredientItems;

    private SlotClass[] items;
    private GameObject[] slots;

    public void Start()
    {

        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];



        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();

        }



        //set all slots
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        SetSupplierList();

        RefreshUI();


    }


    private void Update()
    {


    }


    public void RefreshUI()
    {
       

        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;

                if (items[i].GetItem().isStackable)
                {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity() + "";

                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }

            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";

            }
        }


    }



    public bool Add(ItemClass item, int quantity)
    {
        //check if inventory contains item
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)//this is an empty slot
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }


        }

        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubtractQuantity(1);
            }
            else
            {

                int slotToRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }


                items[slotToRemoveIndex].Clear();
            }

        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }

    //Remove for placement
    public bool Remove(ItemClass item, int quantity)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubtractQuantity(quantity);
            }
            else
            {

                int slotToRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }

                PlacementPanel.RemoveButtonOfItem(temp.GetItem());
                items[slotToRemoveIndex].Clear();
            }

        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }


    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
                return items[i];
        }

        return null;
    }


    //Checks items for placement
    public bool Contains(ItemClass item, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item && items[i].GetQuantity() >= quantity)
                return true;
        }

        return false;
    }

    public SlotClass[] GetItems()
    {
        return items;
    }

    public void AddItemsToInventory(InventoryManager inventory)
    {

        for (int i = 0; i < items.Length; i++)
        {
            inventory.Add(items[i].GetItem(), items[i].GetQuantity());

            
        }

    }





    private void SetSupplierList()
    {
        for (int i = 0; i < supplierIngredientItems.Length; i++)
        {
            items[i] = supplierIngredientItems[i];

        }
    }
}
