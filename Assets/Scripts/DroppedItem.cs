using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class DroppedItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool autoStart;
    [SerializeField] private float enabledPickupDelay = 1f;

    [Header("Sorting Settings")]
    [SerializeField] private string sortingLayerName = "Default";
    [SerializeField] private int sortingOrder = 1;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D itemCollider;

    [Header("State")]
    public Item item;
    public bool pickedUp;

    public bool CanBePickedUp { get; private set; } = true;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (itemCollider == null) itemCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Item newItem)
    {
        item = newItem;
        pickedUp = false;

        if (itemCollider == null) itemCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null && item != null)
        {
            spriteRenderer.sprite = item.icon;
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = sortingOrder;
        }

        if (enabledPickupDelay > 0)
        {
            StartCoroutine(PickupDelayRoutine());
        }
        else if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
    }

    private IEnumerator PickupDelayRoutine()
    {
        CanBePickedUp = false;

        // Temporarily disable the collider during the spawn delay
        if (itemCollider != null) itemCollider.enabled = false;

        yield return new WaitForSeconds(enabledPickupDelay);

        CanBePickedUp = true;

        // Force the collider back on so the player can pick it up
        if (itemCollider != null) itemCollider.enabled = true;
    }
}