using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiParent;    
    public ApplianceContextActionUI applianceContextActionUiPrefab;
    private ApplianceContextActionUI _applianceContextUI;

    public GameObject worldUiParent;
    public GameObject progressBarPrefab;


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
        if (_applianceContextUI == null)
        {
            _applianceContextUI = Instantiate(applianceContextActionUiPrefab, uiParent.transform);
        }
        _applianceContextUI.InitializeWithAppliance(appliance);
        _applianceContextUI.transform.position = position;
        _applianceContextUI.gameObject.SetActive(true);
        // TODO: set position to clicked position
    }

    public void CloseApplianceContextPanel()
    {
        _applianceContextUI.gameObject.SetActive(false);
    }

    public GameObject SpawnProgressBar(Vector2 position)
    {
        GameObject progressBar = Instantiate(progressBarPrefab, worldUiParent.transform);
        progressBar.transform.position = position;
        return progressBar;
    }
}
