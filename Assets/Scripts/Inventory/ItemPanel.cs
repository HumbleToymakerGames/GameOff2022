using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<InventoryButton> buttons;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        SetSourcePanel();
        SetIndex();
        Show();

    }

    private void SetSourcePanel()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetItemPanel(this);
        }
    }
    private void OnEnable()
    {
        Clear();
        Show();
    }
    //sets the amount of slots that need to be filled
    private void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }
    //shows whats being filled in the inventory slots
    public void Show()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();

            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    public void Clear()
    {
        for(int i= 0; i < buttons.Count; i++)
        {
            buttons[i].Clean();
        }
    }

    public void SetInventory(ItemContainer newInventory)
    {
        inventory = newInventory;
    }
    public virtual void OnClick(int id)
    {

    }
}
