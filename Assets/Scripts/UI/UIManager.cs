using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public GameObject uiParent;    
    public ApplianceContextActionUI applianceContextActionUiPrefab;
    private ApplianceContextActionUI _applianceContextUI;

    public GameObject worldUiParent;
    public GameObject progressBarPrefab;

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
