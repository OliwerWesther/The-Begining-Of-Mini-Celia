using UnityEngine;

public class Plant : Interface
{
    [Header("Plant")]
    [SerializeField]public Inventory inventoryScript;

    private void Awake()
    {
        inventoryScript = FindAnyObjectByType<Inventory>();
    }

    protected override void Update()
    {
        base.Update();

        if (inRange && timeHeld >= timeToHold)
        {
            inventoryScript.canPickup = true;
            inventoryScript.PickUp(gameObject);
        }
        else
        {
            inventoryScript.canPickup = false;
        }
    }
}
