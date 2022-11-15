using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer
{
    private CustomerArchetypeSO _archetype;
    private ItemClass _preferredItem;
    private ItemClass _fallbackItem;
    private GameTime _timeWillLeave;

    //Reference to world customer script for removal/gameobject interactions
    public WorldCustomer worldCustomer;

    public Customer(CustomerArchetypeSO archetype)
    {
        _archetype = archetype;
        EventHandler.AdvanceGameMinuteEvent += CustomerTick;
        _timeWillLeave = TimeManager.AddMinutesToGameTime(TimeManager.GetCurrentGameTime(), _archetype.minutesWillWait);
        _preferredItem = ChooseRandomItemFrom(_archetype.preferredItemPool);
        _fallbackItem = ChooseRandomItemFrom(_archetype.fallbackItemPool);
        Debug.Log("A " + _archetype.archetypeName + " has entered the shop. They are looking for a " + _preferredItem.itemName + " but will settle for a " + _fallbackItem.itemName);
    }

    public void Despawn()
    {
        EventHandler.AdvanceGameMinuteEvent -= CustomerTick;
    }

    private void CustomerTick(int hours, int minutes)
    {
        // ShopManager.LookForItem(preferredItem);
        // If found, buy item => leaveSatisfied
        // If not found, ShopManager.LookForItem(fallbackItem);
        // If found, buy item => leaveSomewhatSatisfied
        // Else, wait

        if (TimeManager.GetCurrentGameTime() == _timeWillLeave)
        {
            CustomerLeavesDisappointed();
        }
    }

    private void CustomerLeavesDisappointed()
    {
        Debug.Log("The " + _archetype.archetypeName + " left because the shop did not have a " + _preferredItem.itemName + " or a " + _fallbackItem.itemName + ".");
        CustomerManager.Instance.RemoveCustomer(this);
    }
    
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
