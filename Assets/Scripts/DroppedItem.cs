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

    [Header("Animation")]
    private bool enableAnimation = false;
    private float animationSpeed = 1.5f;
    private float animationHight = 0.05f;
    Vector3 basePostition;

    [Header("State")]
    public Item item;
    public bool pickedUp;


    [Header("Shadow")]
    [SerializeField] private GameObject shadowPrefab;
    private Vector3 shadowOffset;
    private GameObject shadowInstance;

    public bool CanBePickedUp { get; private set; } = true;

    private void Reset()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (itemCollider == null) itemCollider = GetComponent<Collider2D>();
    }

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (itemCollider == null) itemCollider = GetComponent<Collider2D>();
    }

    private void Update() { ItemAnimation(); ShadowAnimation(); }

    public void Initialize(Item newItem)
    {
        //Animation
        enableAnimation = true;
        basePostition = transform.position;
        animationHight = 0.05f;
        animationSpeed = 1.5f;

        item = newItem;
        pickedUp = false;

        if (itemCollider == null) itemCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null && item != null)
        {
            spriteRenderer.sprite = item.icon;
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = sortingOrder;
        }

        //shadow
        SpawnShadow();

        if (enabledPickupDelay > 0)
        {
            StartCoroutine(PickupDelayRoutine());
        }
        else if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
    }

    private void SpawnShadow()
    {
        shadowOffset = new Vector3(0, -0.4f, 0);

        // Fallback: Loads shadow automatically if shadowPrefab is null
        if (shadowPrefab == null)
        {
            shadowPrefab = Resources.Load<GameObject>("ShadowPrefab");
        }

        if (shadowPrefab != null && shadowInstance == null)
        {
            Vector3 spawnPos = basePostition + shadowOffset;

            // Passing 'transform' here automatically parents shadowInstance to this object
            shadowInstance = Instantiate(shadowPrefab, spawnPos, Quaternion.identity, transform);

            SpriteRenderer shadowRenderer = shadowInstance.GetComponent<SpriteRenderer>();
            if (shadowRenderer != null)
            {
                shadowRenderer.sortingLayerName = sortingLayerName;
                shadowRenderer.sortingOrder = sortingOrder - 1;
            }
        }
    }
    
    public void ItemAnimation()
    {
        if (enableAnimation)
        {
            float newY = basePostition.y + Mathf.Sin(Time.time * animationSpeed) * animationHight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
    public void ShadowAnimation()
    {
        if (enableAnimation && shadowInstance != null)
        {
            shadowInstance.transform.position = new Vector3(transform.position.x, basePostition.y + shadowOffset.y, transform.position.z);
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