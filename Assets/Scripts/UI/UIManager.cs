using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiParent;
    public ApplianceContextActionUI applianceContextActionUiPrefab;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowApplianceContextPanel(Appliance appliance, Vector2 position)
    {
        ApplianceContextActionUI contextUi = Instantiate(applianceContextActionUiPrefab, uiParent.transform);
        contextUi.InitializeWithAppliance(appliance);
        contextUi.transform.position = position;
        contextUi.gameObject.SetActive(true);
        // TODO: set position to clicked position
    }
}
