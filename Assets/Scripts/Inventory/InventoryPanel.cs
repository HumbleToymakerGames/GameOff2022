using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<InventoryButton> buttons;

    // Start is called before the first frame update
    void Start()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }
    //sets the amount of slots that need to be filled
    private void SetIndex()
    {
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }
    //shows whats being filled in the inventory slots
    private void Show()
    {
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            if(inventory.slots[i].item == null)
            {
                buttons[i].Clean();

            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
}
