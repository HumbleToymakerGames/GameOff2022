using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplianceContextActionUI : MonoBehaviour
{
    public TextMeshProUGUI applianceNameText;
    public SimpleButton buttonPrefab;
    public GameObject applianceFunctionElPrefab;

    private Appliance _currentAppliance;
    public void InitializeWithAppliance(Appliance appliance)
    {
        gameObject.SetActive(false);

        foreach(Transform child in transform)
        {
            if (child.gameObject.name != "MenuTitleText") Destroy(child.gameObject);
        }

        _currentAppliance = appliance;
        applianceNameText.text = _currentAppliance.GetApplianceName();

        List<ApplianceFunction> functions = _currentAppliance.GetApplianceFunctions();
        foreach(ApplianceFunction function in functions)
        {
            GameObject newFunctionEl = Instantiate(applianceFunctionElPrefab, transform);
            ApplianceFunctionUIElement newFunctionScript = newFunctionEl.GetComponent<ApplianceFunctionUIElement>();
            newFunctionScript.SetToApplianceFunction(function);
            newFunctionScript.GetStartFunctionButton().onClick.AddListener(() => appliance.FunctionClicked(function));

            // newButton.button.onClick.AddListener(() => appliance.FunctionClicked(function));
        }
        SimpleButton backButton = Instantiate(buttonPrefab, transform);
        backButton.SetButtonText("Back");
        backButton.button.onClick.AddListener(() => UIManager.Instance.CloseApplianceContextPanel());
    }
}
