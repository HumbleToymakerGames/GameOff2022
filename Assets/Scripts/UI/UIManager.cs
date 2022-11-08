using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public GameObject uiParent;    
    public ApplianceContextActionUI applianceContextActionUiPrefab;
    private ApplianceContextActionUI _applianceContextUI;

    public GameObject worldUiParent;
    public GameObject progressBarPrefab;

    public float inWorldUIPositionYOffset = 0.5f;

    private List<GameObject> _progressBarObjectPool = new List<GameObject>();

    public void ShowApplianceContextPanel(Appliance appliance, Vector2 position)
    {
        if (_applianceContextUI == null)
        {
            _applianceContextUI = Instantiate(applianceContextActionUiPrefab, uiParent.transform);
        }
        _applianceContextUI.InitializeWithAppliance(appliance);
        _applianceContextUI.gameObject.SetActive(true);
        // TODO: set position to clicked position
    }

    public void CloseApplianceContextPanel()
    {
        _applianceContextUI.gameObject.SetActive(false);
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
