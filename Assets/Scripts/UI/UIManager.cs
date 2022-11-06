using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public GameObject uiParent;    
    public ApplianceContextActionUI applianceContextActionUiPrefab;
    private ApplianceContextActionUI _applianceContextUI;

    public GameObject worldUiParent;
    public GameObject progressBarPrefab;

    private List<ApplianceFunctionProgressBar> _progressBarObjectPool = new List<ApplianceFunctionProgressBar>();

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

    public ApplianceFunctionProgressBar PlaceProgressBarForApplianceFunction(ApplianceFunction applianceFunction, Vector2 position)
    {

        ApplianceFunctionProgressBar progressBar = GetUnusedProgressBarOrInstantiate();
        progressBar.transform.position = position;
        progressBar.image.sprite = applianceFunction.ImageForOutputProduct();
        progressBar.SetActive(true);
        return progressBar;

    }

    private ApplianceFunctionProgressBar GetUnusedProgressBarOrInstantiate()
    {
        if (_progressBarObjectPool.Count > 0)
        {
            foreach (ApplianceFunctionProgressBar bar in _progressBarObjectPool)
            {
                if (bar.activeInHierarchy == false) return bar;
            }
        }

        GameObject newBarGO = Instantiate(progressBarPrefab, worldUiParent.transform);
        ApplianceFunctionProgressBar newBar = newBarGO.GetComponent<ApplianceFunctionProgressBar>();
        _progressBarObjectPool.Add(newBar);
        newBar.SetActive(false);
        return newBar;
    }
}
