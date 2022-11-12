using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCustomer : MonoBehaviour
{
    public Customer customer;

    public GameObject self;

    public bool exiting = false;

    private void Start()
    {
        self = gameObject;
        customer.worldCustomer = this;
    }

    // Need this because if we use an object pool, old customer WorldCustomer instances need to be reused
    public void InitializeWithCustomer(Customer newCustomer)
    {
        customer = newCustomer;
        exiting = false;
        customer.worldCustomer = this;
    }
}
