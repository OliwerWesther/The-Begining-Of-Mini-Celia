using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider2D))]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int maxSlots = 20;

    [Header("Drop Location")]
    [SerializeField] new Vector3 dropLocation = new Vector3(0, -1, 0);

    [Header("References")]
    [SerializeField] AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField] GameObject droppedItemPrefab;

    [Header("Audio Clips")]
    [SerializeField] AudioClip pickUpItemAudio;
    [SerializeField] AudioClip dropItemAudio;

    [Header("State")]
    [SerializeField] SerializedDictionary<string, Item> inventory = new();

    private InventoryUI ui;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterUI(InventoryUI newUI)
    {
        ui = newUI;
        foreach (var kvp in inventory)
        {
            ui.AddUIItem(kvp.Key, kvp.Value);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("DroppedItem")) return;

        var droppedItem = other.GetComponent<DroppedItem>();
        if (droppedItem == null) return;

        // ANTI-DUPE: Ignore if already picked up OR still on drop delay cooldown
        if (droppedItem.pickedUp || !droppedItem.CanBePickedUp) return;

        // Instantly mark as picked up before processing
        droppedItem.pickedUp = true;

        if (AddItem(droppedItem.item))
        {
            other.enabled = false; // Disable collider instantly
            Destroy(other.gameObject);

            if (audioSource != null && pickUpItemAudio != null)
            {
                audioSource.PlayOneShot(pickUpItemAudio);
            }
        }
        else
        {
            // Revert flag if inventory was full
            droppedItem.pickedUp = false;
        }
    }

    public bool AddItem(Item item)
    {
        if (item == null) return false;
        if (inventory.Count >= maxSlots) return false;

        var inventoryId = Guid.NewGuid().ToString();
        inventory.Add(inventoryId, item);
        if (ui != null) ui.AddUIItem(inventoryId, item);
        return true;
    }

    public void DropItem(string inventoryId)
    {
        if (!inventory.TryGetValue(inventoryId, out var item)) return;

        inventory.Remove(inventoryId);
        if (ui != null) ui.RemoveUIItem(inventoryId);

        // Calculate spawn position and explicitly snap Z to 0
        Vector3 spawnPos = transform.position + dropLocation;
        spawnPos.z = 0f;

        var droppedItemObj = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);
        var droppedItem = droppedItemObj.GetComponent<DroppedItem>();

        if (droppedItem != null)
        {
            droppedItem.Initialize(item);
        }

        if (audioSource != null && dropItemAudio != null)
        {
            audioSource.PlayOneShot(dropItemAudio);
        }
    }
}