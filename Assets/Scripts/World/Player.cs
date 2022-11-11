using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Changes player interactions based on game state
    public static ControlState controlState = ControlState.Game;

    private Movement movement;
    private PlayerEditMode edit;

    public GameObject placementPanel;
    public GameObject objectSelectButtonPrefab;
    private bool menuLoaded = false;
    private float menuButtonMargin = 5f;

    private void Start()
    {
        movement = GetComponent<Movement>();
        edit = GetComponent<PlayerEditMode>();   
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(controlState != ControlState.Game)
                controlState = ControlState.Game;
            else
                controlState = ControlState.Edit;
        }

        switch (controlState)
        {
            case ControlState.Edit:
                LoadObjectMenu();
                edit.UpdateCall();
                break;
            case ControlState.Game:
                UnloadObjectMenu();
                break;
        }
    }

    private void LoadObjectMenu()
    {
        if (!menuLoaded)
        {
            placementPanel.SetActive(true);
            float menuPadding = 12f;
            RectTransform placementRect = placementPanel.GetComponent<RectTransform>();
            IList<PlaceableObjectSO> placeableObjects = Resources.LoadAll<PlaceableObjectSO>("Data/PlaceableObjects");
            int maxPerRow = (int)((placementRect.rect.width - menuPadding*2) / (objectSelectButtonPrefab.GetComponent<RectTransform>().rect.width + menuButtonMargin));
            int y = 0;
            int x = 0;
            for (int i = 0; i < placeableObjects.Count; i++)
            {
                GameObject button = Instantiate(objectSelectButtonPrefab, placementPanel.transform);
                RectTransform rect = button.GetComponent<RectTransform>();
                button.GetComponent<ObjectSelectButton>().placeableObjectSO = placeableObjects[i];

                rect.localPosition += new Vector3(-(placementRect.rect.width/2 - rect.rect.width/2 - menuPadding - (x * (menuButtonMargin + rect.rect.width))), placementRect.rect.height/2 - rect.rect.height / 2 - menuPadding - (y * (menuButtonMargin + rect.rect.height)));

                button.SetActive(true);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => { edit.GetComponent<PlayerEditMode>().SetObject(button); });

                x++;
                if (x > maxPerRow)
                {
                    x = 0;
                    y++;
                }    
            }
        }

        menuLoaded = true;
    }

    private void UnloadObjectMenu()
    {
        if(menuLoaded)
        {
            for (int i = placementPanel.transform.childCount - 1; i >= 0; i--)
                Destroy(placementPanel.transform.GetChild(i).gameObject);
            placementPanel.SetActive(false);
            menuLoaded = false;
        }
    }
}

public enum ControlState { Game, Edit };
