using System.Diagnostics.Tracing;
using UnityEditor.Rendering;
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

    public float timeHeld;
    public float timeToHold = 10f;
    
    // Update is called once per frame
    void Update()
    {
        bool inRange = Physics2D.OverlapCircle(transform.position, range, Player);
        if (inRange) { uI.SetActive(true); if (debug) { print("Interface: In range"); } }
        else { uI.SetActive(false); if (debug) { print("Interface: Outside range"); } }

        if (inRange)
        {
            timeHeld += Time.deltaTime;
            if(timeHeld >= timeToHold) { timeHeld = timeToHold; }; //lock the time at timeToHold.
            float wobblePower = Mathf.Lerp(0, timeToHold, timeHeld / timeToHold); //lock the WobblePower at timeToHold.
            if (debug) { print("wobblePower: " + wobblePower); } 
        }
        else
        {
            timeHeld -= Time.deltaTime; //remove some time timeHeld when outside of range.
            if(timeHeld <= 0) { timeHeld = 0f; } //lock the time to 0.
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
