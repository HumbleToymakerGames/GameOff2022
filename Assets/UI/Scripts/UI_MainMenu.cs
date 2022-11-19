using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject ingredientDeliveryPanel;
    public GameObject recipePanel;
    public GameObject storePanel;
    public GameObject achievementsPanel;
    public GameObject settingsPanel;

    public void OpenPanel()
    {
        OpenMainMenuToPanel(MainMenuState.IngredientDelivery);
    }

    public void OpenMainMenuToPanel(MainMenuState state)
    {
        HideAllPanels();
        PanelForMainMenuState(state).SetActive(true);
        mainMenu.SetActive(true);
        PanelForMainMenuState(state).GetComponent<I_UIMainMenuPanel>().Display();
    }

    private void HideAllPanels()
    {
        ingredientDeliveryPanel.SetActive(false);
        recipePanel.SetActive(false);
        storePanel.SetActive(false);
        achievementsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private GameObject PanelForMainMenuState(MainMenuState state)
    {
        switch (state)
        {
            case MainMenuState.IngredientDelivery:
                return ingredientDeliveryPanel;
            case MainMenuState.Recipe:
                return recipePanel;
            case MainMenuState.Store:
                return storePanel;
            case MainMenuState.Achievements:
                return achievementsPanel;
            case MainMenuState.Settings:
                return settingsPanel;
            default:
                return ingredientDeliveryPanel;
        }
    }

}
