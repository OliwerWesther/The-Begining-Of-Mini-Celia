using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public void Initialize(string inventoryId, Item item, Action<string> onDropClicked)
    {
        if (item != null && image != null)
        {
            if (item.icon != null)
            {
                image.sprite = item.icon;
                image.color = Color.white; // Ensures no transparency or unwanted tinting
                image.enabled = true;
            }
            else
            {
                // Disable image component if no icon is set on the ScriptableObject
                image.enabled = false;
            }
        }

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onDropClicked?.Invoke(inventoryId));
        }
    }
}