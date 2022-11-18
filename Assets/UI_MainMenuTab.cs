using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuTab : MonoBehaviour
{
    public MainMenuState stateToOpen;
    public UI_MainMenu mainMenu;

    public void OnClickButton()
    {
        mainMenu.OpenMainMenuToPanel(stateToOpen);
    }
}
