using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlacementPanel
{
    private static bool menuLoaded = true;
    private static float menuButtonMargin = 5f;

    /// <summary>
    /// Enables the object placement menu and loads in all placeable objects. Will need to be hooked up to the inventory system eventually if we plan to have furniture be bought
    /// </summary>
    private static void LoadObjectMenu()
    {
        if (!menuLoaded)
        {
            GameObject placementPanel = GameObject.FindGameObjectWithTag("PlacementPanel");
            GameObject objectSelectButtonPrefab = Resources.Load<GameObject>("Prefabs/ObjectSelectButton");
            placementPanel.GetComponent<Image>().enabled = true;
            float menuPadding = 12f;
            RectTransform placementRect = placementPanel.GetComponent<RectTransform>();
            IList<PlaceableObjectClass> placeableObjects = Resources.LoadAll<PlaceableObjectClass>("Data/PlaceableObjects");
            int maxPerRow = (int)((placementRect.rect.width - menuPadding * 2) / (objectSelectButtonPrefab.GetComponent<RectTransform>().rect.width + menuButtonMargin));
            int y = 0;
            int x = 0;
            for (int i = 0; i < placeableObjects.Count; i++)
            {
                GameObject button = Object.Instantiate(objectSelectButtonPrefab, placementPanel.transform);
                RectTransform rect = button.GetComponent<RectTransform>();
                button.GetComponent<ObjectSelectButton>().placeableObjectSO = placeableObjects[i];

                rect.localPosition += new Vector3(-(placementRect.rect.width / 2 - rect.rect.width / 2 - menuPadding - (x * (menuButtonMargin + rect.rect.width))), placementRect.rect.height / 2 - rect.rect.height / 2 - menuPadding - (y * (menuButtonMargin + rect.rect.height)));

                button.SetActive(true);

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEditMode>().SetObject(button); });

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

    /// <summary>
    /// Disables the object placement menu and destroys all buttons inside of it
    /// </summary>
    private static void UnloadObjectMenu()
    {
        if (menuLoaded)
        {
            GameObject placementPanel = GameObject.FindGameObjectWithTag("PlacementPanel");
            placementPanel.GetComponent<Image>().enabled = false;
            for (int i = placementPanel.transform.childCount - 1; i >= 0; i--)
                Object.Destroy(placementPanel.transform.GetChild(i).gameObject);
            menuLoaded = false;
        }
    }

    /// <summary>
    /// Allows you to set if the panel is shown or not
    /// </summary>
    /// <param name="shown"></param>
    public static void ShowPlacementMenu(bool shown)
    {
        if (shown)
            LoadObjectMenu();
        else
            UnloadObjectMenu();
    }
}
