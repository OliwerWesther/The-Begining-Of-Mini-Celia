using System.Diagnostics.Tracing;
using UnityEngine;

public class Interface : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The same layer as the player.")]
    public LayerMask Player;
    [Tooltip("The UI gameobject")]
    public GameObject uI;

    [Header("Varibles")]
    [Tooltip("The range for the UI to pop up.")]
    public float range;

    [Header("Debug")]
    [Tooltip("Enable Gizmos & Prints")]
    public bool debug;

    float timeHeld;
    float timeToHold = 10;
    
    // Update is called once per frame
    void Update()
    {
        bool inRange = Physics2D.OverlapCircle(transform.position, range, Player);
        if (inRange) { uI.SetActive(true); if (debug) { print("Interface: In range"); } }
        else { uI.SetActive(false); if (debug) { print("Interface: Outside range"); } }

        if (true)
        {
            timeHeld += Time.deltaTime;
            float wobblePower = Mathf.Lerp(0, 10, timeHeld / timeToHold);
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
