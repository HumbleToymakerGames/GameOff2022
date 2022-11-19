using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public GameObject worldUiParent;
    public GameObject progressBarPrefab;
    public float inWorldUIPositionYOffset = 0.5f;
    public GameObject mainMenu;

    private GameObject _appliancePopupMenuPanel;
    private UI_AppliancePopupMenuPanel _appliancePopupMenuPanelScript;
    private bool _isMainMenuDisplaying = false;

    private List<GameObject> _progressBarObjectPool = new List<GameObject>();

    private void Start()
    {
        _appliancePopupMenuPanel = GameObject.Find("AppliancePopupMenuPanel");
        if (_appliancePopupMenuPanel != null)
        {
            _appliancePopupMenuPanelScript = _appliancePopupMenuPanel.GetComponent<UI_AppliancePopupMenuPanel>();
            _appliancePopupMenuPanel.SetActive(false);
        }
    }

    public void ShowApplianceContextPanel(Appliance appliance)
    {
        _appliancePopupMenuPanelScript.InitializeWithAppliance(appliance);
        _appliancePopupMenuPanel.SetActive(true);
    }

    public void CloseApplianceContextPanel()
    {
        _appliancePopupMenuPanel.SetActive(false);
        _appliancePopupMenuPanelScript.ClearApplianceContext();
    }

    public void PreviewApplianceFunction(ApplianceFunction function)
    {
        _appliancePopupMenuPanelScript.PreviewApplianceFunction(function);
    }
    
    public GameObject PlaceProgressBarForApplianceFunction(ApplianceFunction applianceFunction, Vector2 position)
    {
        GameObject progressBar = GetUnusedProgressBarOrInstantiate();
        progressBar.transform.position = new Vector2(position.x, position.y + inWorldUIPositionYOffset);
        Sprite sprite = applianceFunction.SpriteForOutputProduct();

        if (sprite != null)
        {
            progressBar.GetComponent<ApplianceFunctionProgressBar>().image.sprite = sprite;
        }

        progressBar.SetActive(true);
        return progressBar;
    }

    public void ToggleMainMenu()
    {
        if (_isMainMenuDisplaying)
        {
            _isMainMenuDisplaying = false;
            mainMenu.SetActive(false);
        } else
        {
            _isMainMenuDisplaying = true;
            mainMenu.SetActive(true);
            mainMenu.GetComponent<UI_MainMenu>().OpenPanel();
        }
    }

    private GameObject GetUnusedProgressBarOrInstantiate()
    {
        if (_progressBarObjectPool.Count > 0)
        {
            foreach (GameObject bar in _progressBarObjectPool)
            {
                if (bar.activeInHierarchy == false) return bar;
            }
        }

        GameObject newBarGO = Instantiate(progressBarPrefab, worldUiParent.transform);
        _progressBarObjectPool.Add(newBarGO);
        newBarGO.SetActive(false);
        return newBarGO;
    }
    
}
