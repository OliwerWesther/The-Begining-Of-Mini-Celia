using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField] Transform uiInventoryParent;

    [Header("State")]
    [SerializeField] SerializedDictionary<string, GameObject> inventoryUI = new();

    private void Start()
    {
        // Tell the persistent Inventory component that this is the active UI for this scene
        if (Inventory.Instance != null)
        {
            Inventory.Instance.RegisterUI(this);
        }
    }

    public void AddUIItem(string inventoryId, Item item)
    {
        var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
        
        // Pass false to maintain proper local UI scale and position inside layout groups
        itemUI.transform.SetParent(uiInventoryParent, false);

        inventoryUI.Add(inventoryId, itemUI.gameObject);

        // Wire drop action directly to persistent Inventory instance
        itemUI.Initialize(inventoryId, item, Inventory.Instance.DropItem);
    }

    public void RemoveUIItem(string inventoryId)
    {
        if (inventoryUI.TryGetValue(inventoryId, out GameObject itemUI))
        {
            inventoryUI.Remove(inventoryId);
            Destroy(itemUI);
        }
    }
}