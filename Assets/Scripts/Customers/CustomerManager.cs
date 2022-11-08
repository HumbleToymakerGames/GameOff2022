using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : SingletonMonoBehaviour<CustomerManager>
{
    public List<CustomerArchetypeSO> potentialCustomerArchetypes = new List<CustomerArchetypeSO>();

    private List<Customer> _customersInShop = new List<Customer>();

    private GameObject customerPrefab;
    private Transform entrance;

    private void Start()
    {
        customerPrefab = Resources.Load<GameObject>("Prefabs/NPC");
        entrance = GameObject.FindGameObjectWithTag("Entrance").transform;
    }

    // every minute, there is a chance that a customer will enter your shop
    // the chance a customer will arrive is a factor of your popularity
    // each customer will have a craved food based on their archetype
    // customers have different archetypes which are all SO

    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += CustomerManagerTick;
    }

    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= CustomerManagerTick;
    }

    private void CustomerManagerTick(int hour, int minutes)
    {
        foreach(CustomerArchetypeSO archetype in potentialCustomerArchetypes)
        {
            float rng = Random.Range(0f, 1f);
            if (rng <= archetype.chanceToSpawn) {
                SpawnCustomer(archetype);
                break;
            }
        }
    }

    private void SpawnCustomer(CustomerArchetypeSO archetype)
    {
        Customer newCustomer = new Customer(archetype);
        GameObject customerObject = Instantiate(customerPrefab, entrance.position, Quaternion.identity);
        customerObject.GetComponent<WorldCustomer>().customer = newCustomer;
        _customersInShop.Add(newCustomer);
    }

    public void RemoveCustomer(Customer customerToRemove)
    {
        _customersInShop.Remove(customerToRemove);

        customerToRemove.worldCustomer.exiting = true;
    }
}
