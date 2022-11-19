using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    [SerializeField] public List<SlotClass> startingItems = new List<SlotClass>();
    private List<SlotClass> _items = new List<SlotClass>();

    //[SerializeField] private List<CraftingRecipe> craftingRecipes = new List<CraftingRecipe>();
    //[SerializeField] private GameObject itemCursor;
    //[SerializeField] private GameObject slotHolder;
    //[SerializeField] private ItemClass itemToAdd;
    //[SerializeField] private ItemClass itemToRemove;
    //private GameObject[] slots;
    //private SlotClass movingSlot;
    //private SlotClass tempSlot;
    //private SlotClass originalSlot;
    //bool isMovingItem;


    public List<SlotClass> GetInventory()
    {
        return _items.FindAll(item => item.GetItem().isSellable == false);
    }

    public List<SlotClass> GetStoreStock()
    {
        return _items.FindAll(item => item.GetItem().isSellable == true);
    }

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
        AddStartingItems();
    }

    private void AddStartingItems()
    {
        if (startingItems.Count == 0) return;

        foreach(SlotClass slot in startingItems)
        {
            Debug.Log(slot.GetItem().GetType());
            Add(slot.GetItem(), slot.GetQuantity());
        }
    }

    public bool Add(ItemClass item, int quantity)
    {
        //check if inventory contains item
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            _items.Add(new SlotClass(item, quantity));
        }

        EventHandler.CallInventoryDidChangeEvent();
        return true;
    }

    public bool CanAfford(SlotClass item)
    {
        SlotClass foundItem = Contains(item.GetItem());
        if (foundItem == null) return false;
        if (foundItem.GetQuantity() < item.GetQuantity()) return false;
        return true;
    }


    public bool Remove(ItemClass item, int quantity)
    {

        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() >= quantity)
            {
                temp.SubtractQuantity(quantity);
            }
            else
            {

                int slotToRemoveIndex = 0;

                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }


                _items[slotToRemoveIndex].Clear();
            }

        }
        else
        {
            return false;
        }

        EventHandler.CallInventoryDidChangeEvent();
        return true;
    }

    public ItemClass GetRandomItemInStock()
    {
        List<SlotClass> flattenedStock = FlattenInventory(GetStoreStock());
        if (flattenedStock == null || flattenedStock.Count == 0) return null;

        int randomIndex = Random.Range(0, flattenedStock.Count - 1);
        return flattenedStock[randomIndex].GetItem();
    }


    private void Update()
    {
        /*
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
        */

    }

    // Given a List of SlotClass entities with quantities of any value,
    // returns a List of SlotClass entities where each quantity is 1
    // and items are repeated if necessary.
    // Useful for randomly selecting an item in a stock while accounting 
    // for which items are more likely to be selected
    private List<SlotClass> FlattenInventory(List<SlotClass> inventory)
    {
        if (inventory == null || inventory.Count == 0) return null;

        List<SlotClass> returnInventory = new();

        foreach(SlotClass item in inventory)
        {
            for (int i = 0; i < item.GetQuantity(); i++)
            {
                returnInventory.Add(new SlotClass(item.GetItem(), 1));
            }
        }
        return returnInventory;
    }


    private void OnApplianceFunctionDidComplete(ApplianceFunction function, SlotClass itemQuantity)
    {
        Add(itemQuantity.GetItem(), itemQuantity.GetQuantity());
    }

    private void OnInventorySupplyDeliveryWasMade()
    {
        
    }

    //looks through all items there and determines if its there

    /*
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
    */

    /*
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




        //RefreshUI();
        return true;
    }
    */

    public int GetCountOfItem(ItemClass item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].GetItem() == item)
                return _items[i].GetQuantity();
        }

        return 0;
    }


    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].GetItem() == item)
                return _items[i];
        }

        return null;
    }

    //Checks items for crafting
    public bool Contains(ItemClass item, int quantity)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].GetItem() == item && _items[i].GetQuantity() >= quantity)
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


    /*
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
    */
}
