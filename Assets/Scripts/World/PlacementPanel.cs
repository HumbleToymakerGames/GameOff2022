using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class PlacementPanel
{
    private static bool menuLoaded = false;
    private static float menuButtonMargin = 5f;

    /// <summary>
    /// Enables the object placement menu and loads in all placeable objects. Will need to be hooked up to the inventory system eventually if we plan to have furniture be bought
    /// </summary>
    private static void LoadObjectMenu(SlotClass[] items = null)
    {
        if (menuLoaded)
            UnloadObjectMenu();
        //if (!menuLoaded)
        //{
        //IList<ItemClass> placeableObjects = Resources.LoadAll<PlaceableObjectClass>("Data/PlaceableObjects");
        IList<SlotClass> placeableObjects = new List<SlotClass>();
        if (items != null)
        {
            foreach (SlotClass s in items)
            {
                if (s.GetItem() != null)
                {
                    placeableObjects.Add(s);
                }
            }
        }
        GameObject placementPanel = GameObject.FindGameObjectWithTag("PlacementPanel");
        GameObject objectSelectButtonPrefab = Resources.Load<GameObject>("Prefabs/ObjectSelectButton");
        placementPanel.GetComponent<Image>().enabled = true;
        float menuPadding = 12f;
        RectTransform placementRect = placementPanel.GetComponent<RectTransform>();
        int maxPerRow = (int)((placementRect.rect.width - menuPadding * 2) / (objectSelectButtonPrefab.GetComponent<RectTransform>().rect.width + menuButtonMargin)) + 1;
        int maxRows = (int)((placementRect.rect.height - menuPadding * 2) / (objectSelectButtonPrefab.GetComponent<RectTransform>().rect.height + menuButtonMargin));
        int y = 0;
        int x = 0;
        for (int i = 0; i < placeableObjects.Count; i++)
        {
            GameObject button = Object.Instantiate(objectSelectButtonPrefab, placementPanel.transform);
            RectTransform rect = button.GetComponent<RectTransform>();
            ItemClass item = placeableObjects[i].GetItem();
            button.GetComponent<ObjectSelectButton>().item = item;
            button.GetComponent<ObjectSelectButton>().placeableObjectSO = item.GetPlaceableObject();
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = placeableObjects[i].GetQuantity().ToString();

            rect.localPosition += new Vector3(-(placementRect.rect.width / 2 - rect.rect.width / 2 - menuPadding - (x * (menuButtonMargin + rect.rect.width))), placementRect.rect.height / 2 - rect.rect.height / 2 - menuPadding - (y * (menuButtonMargin + rect.rect.height)));
            
            button.SetActive(true);

            Button btn = button.GetComponent<Button>();
            SlotClass tempSlot = placeableObjects[i];
            btn.onClick.AddListener(() => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEditMode>().SetObject(button, tempSlot); });

            x++;
            if (x >= maxPerRow)
            {
                x = 0;
                y++;
            }
        }
        //}
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
    public static void ShowPlacementMenu(bool shown, SlotClass[] items = null)
    {
        if (shown)
            LoadObjectMenu(items);
        else
            UnloadObjectMenu();
    }

    public static void RemoveButtonOfItem(ItemClass item)
    {
        IList<GameObject> toBeDestroyed = new List<GameObject>();
        for (int i = GameObject.FindGameObjectWithTag("PlacementPanel").transform.childCount - 1; i >= GameObject.FindGameObjectWithTag("PlacementPanel").transform.childCount; i--)
        {
            GameObject child = GameObject.FindGameObjectWithTag("PlacementPanel").transform.GetChild(i).gameObject;

            if (child.CompareTag("ObjectSelectButton"))
            {
                if (child.GetComponent<ObjectSelectButton>().item == item)
                {
                    Debug.Log("Destroy: " + child);
                    Object.Destroy(child);
                }
            }
        }
    }
}

