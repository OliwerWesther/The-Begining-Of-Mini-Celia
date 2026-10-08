using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DroppedItem : MonoBehaviour
{
    [Header("settings")]
    [SerializeField]
    bool autoStart;

    [SerializeField]
    float enabledPickupDelay = 3.0f;

    [Header("State")]
    public Item item;
    public bool pickedUp = false;


    public void Start()
    {
        if (autoStart && item != null)
        {
            Initialize(item);
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transform.position, Quaternion.identity);
       // droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(EnablePickup(enabledPickupDelay));
    }
    IEnumerator EnablePickup(float dealy)
    {
        yield return new WaitForSeconds(dealy);
        GetComponent<Collider2D>().enabled = true;
    }
}