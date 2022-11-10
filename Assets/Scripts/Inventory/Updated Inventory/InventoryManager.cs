using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();

    [SerializeField] private GameObject itemCursor;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private SlotClass[] startingItems;

    private SlotClass[] items;


    private GameObject[] slots;
    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;

    bool isMovingItem;

    private void OnEnable()
    {
        EventHandler.ApplianceFunctionDidCompleteEvent += OnApplianceFunctionDidComplete;
    }

    private void OnDisable()
    {
        EventHandler.ApplianceFunctionDidCompleteEvent -= OnApplianceFunctionDidComplete;
    }

    public void Start()
    {

        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        


        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();

        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];

        }

        //set all slots
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }



        RefreshUI();

        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }


    private void Update()
    {
        //Handles Crafting
        if (Input.GetKeyDown(KeyCode.C))
        {
            Craft(craftingRecipes[0]);
        }


        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
        {
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }

        if (Input.GetMouseButtonDown(0)) //player clicked
        {
            //find the closet slot player clicked on

            if (isMovingItem)
            {
                EndItemMove();
            }
            else
            {
                BeginItemMove();
            }
        }





    }

    private void OnApplianceFunctionDidComplete(ApplianceFunction function, ItemQuantity itemQuantity)
    {
        Debug.Log("InventoryManager got ApplianceFunctionDidCompleteEvent");
        // The method run by the EventHandler when any appliancefunction completes
        // function: the ApplianceFunction SO that just completed
        // itemQuantity: a struct containing itemQuantity.item (class Item) and itemQuantity.itemQuantity (int)

        // Normally you would just run this method, but it does not work since Add() takes in a different datatype
        // Add(itemQuantity.item, itemQuantity.itemQuantity);
    }

    //looks through all items there and determines if its there
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

    //Remove for crafting
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

    //Checks items for crafting
    public bool Contains(ItemClass item, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item && items[i].GetQuantity() >= quantity)
                return true;
        }

        return false;
    }


    private void Craft(CraftingRecipe recipe)
    {
        if (recipe.CanCraft(this))
        {
            recipe.Craft(this);

        }
        else
        {
            Debug.Log("Can't craft that item");
        }
    }



    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();

        if (originalSlot == null || originalSlot.GetItem() == null)
        {
            return false;// there is no item to move
        }

        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;

    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {

            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())//they're not the same item
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    tempSlot = new SlotClass(originalSlot); //a = b
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());// b = c
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());//a = c
                    RefreshUI();
                    return true;
                }
            }
            else //place item as usual
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }

        isMovingItem = false;
        RefreshUI();
        return true;
    }


    private SlotClass GetClosestSlot()
    {
        Debug.Log(Input.mousePosition);
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
            {
                return items[i];
            }
        }

        return null;
    }
}
