using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplianceContextActionUI : MonoBehaviour
{
    public TextMeshProUGUI applianceName;
    public GameObject button;

    private Appliance _currentAppliance;

    void InitializeWithAppliance(Appliance appliance)
    {
        gameObject.SetActive(false);
        _currentAppliance = appliance;
        applianceName.text = _currentAppliance.GetApplianceName();
        //transform.GetChild(0).gameObject;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
