using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer
{
    private CustomerArchetypeSO _archetype;
    // private ItemClass _preferredItem;
    // private ItemClass _fallbackItem;
    private bool _hasPurchasedItem = false;
    private GameTime _timeWillLeave;
    private GameTime _timeWillStartPurchaseAttempt;
    private int _secondsBeforeStartPurchaseAttempt = Random.Range(7,15); 

    //Reference to world customer script for removal/gameobject interactions
    public WorldCustomer worldCustomer;

    public Customer(CustomerArchetypeSO archetype)
    {
        _archetype = archetype;
        EventHandler.AdvanceGameMinuteEvent += CustomerTick;
        //_preferredItem = ChooseRandomItemFrom(_archetype.preferredItemPool);
        //_fallbackItem = ChooseRandomItemFrom(_archetype.fallbackItemPool);
        // Debug.Log("A " + _archetype.archetypeName + " has entered the shop. They are looking for a " + _preferredItem.itemName + " but will settle for a " + _fallbackItem.itemName);
    }

    public void SetTimeUntilVariables()
    {
        _timeWillLeave = TimeManager.AddMinutesToGameTime(TimeManager.GetCurrentGameTime(), _archetype.minutesWillWait);
        _timeWillStartPurchaseAttempt = TimeManager.AddMinutesToGameTime(TimeManager.GetCurrentGameTime(), _secondsBeforeStartPurchaseAttempt);
    }

    public void Despawn()
    {
        EventHandler.AdvanceGameMinuteEvent -= CustomerTick;
    }

    private void CustomerTick(int hours, int minutes)
    {
        if (worldCustomer.exiting == true) return;
        if (_hasPurchasedItem == true) return;
        if (TimeManager.GetCurrentGameTime() > this._timeWillLeave) CustomerLeavesDisappointed();
        if (TimeManager.GetCurrentGameTime() > _timeWillStartPurchaseAttempt) AttemptToPurchaseItem();
    }

    private void AttemptToPurchaseItem()
    {
        ItemClass itemToPurchase = InventoryManager.Instance.GetRandomItemInStock();
        if (itemToPurchase == null) return;

        InventoryManager.Instance.Remove(itemToPurchase, 1);
        ShopManager.Instance.AddMoney(itemToPurchase.GetItem().baseCost);
        _hasPurchasedItem = true;
        CustomerManager.Instance.BeginRemoveCustomer(this);
    }


    private void CustomerLeavesDisappointed()
    {
        //Debug.Log("The " + _archetype.archetypeName + " left because the shop did not have a " + _preferredItem.itemName + " or a " + _fallbackItem.itemName + ".");
        worldCustomer.exiting = true;
        CustomerManager.Instance.BeginRemoveCustomer(this);
    }


    
    // This method is not used because we are not using the "preferredItem" mechanic for now"
    private ItemClass ChooseRandomItemFrom(List<WeightedDesiredItem> weightedItemList)
    {
        int denominator = 0;
        foreach (WeightedDesiredItem weightItem in weightedItemList)
        {
            denominator += weightItem.weight;
        }

        float chosen = Random.Range(0f, denominator);
        int startingNumber = 0;
        foreach (WeightedDesiredItem weightItem in weightedItemList)
        {
            //Debug.Log("Chosen is " + chosen + ". Is it between " + startingNumber + " and " + (startingNumber + weightItem.weight) + "?");
            if (chosen > startingNumber && chosen <= startingNumber + weightItem.weight)
            {
                return weightItem.item;
            } else
            {
                startingNumber += weightItem.weight;
            }
        }
        return weightedItemList[0].item;
    }
}
