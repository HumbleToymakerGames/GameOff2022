using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : SingletonMonoBehaviour<CustomerManager>
{
    public List<CustomerArchetypeSO> potentialCustomerArchetypes = new List<CustomerArchetypeSO>();
    public GameObject customerSpawnParent;

    private List<Customer> _customersInShop = new List<Customer>();
    private List<GameObject> _customerObjectPool = new List<GameObject>();

    private GameObject customerPrefab;
    private Transform entrance;

    private void Start()
    {
        customerPrefab = Resources.Load<GameObject>("Prefabs/NPC");
        entrance = GameObject.FindGameObjectWithTag("Entrance").transform;
        customerSpawnParent = gameObject;
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
        GameObject customerObject = GetUnusedCustomerOrInstantiate();
        customerObject.GetComponent<WorldCustomer>().InitializeWithCustomer(newCustomer);
        customerObject.GetComponent<NpcMovement>().movementState = NPCMovementState.Random;
        _customersInShop.Add(newCustomer);
        customerObject.SetActive(true);
    }

    public void RemoveCustomer(Customer customerToRemove)
    {
        customerToRemove.worldCustomer.GetComponent<NpcMovement>().StartNPCMoveToExit();
        _customersInShop.Remove(customerToRemove);
    }
    private GameObject GetUnusedCustomerOrInstantiate()
    {
        if (_customerObjectPool.Count > 0)
        {
            foreach (GameObject customer in _customerObjectPool)
            {
                if (customer.activeInHierarchy == false) return customer;
            }
        }

        GameObject newCustomer = Instantiate(customerPrefab, customerSpawnParent.transform);
        newCustomer.transform.position = entrance.transform.position + new Vector3(0, newCustomer.transform.localScale.y/2, 0);
        _customerObjectPool.Add(newCustomer);
        newCustomer.SetActive(false);
        return newCustomer;
    }
}
