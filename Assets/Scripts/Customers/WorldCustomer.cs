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
}
