using UnityEngine;

public class Interface : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The same layer as the player.")]
    public LayerMask Player;
    [Tooltip("The UI gameobject")]
    public GameObject uIObject;

    [Header("Varibles")]
    [Tooltip("The range for the UI to pop up.")]
    [Range(0,3)]public float range = 1.5f;
    [Tooltip("The time for the player to hold down the Interact button to finish the interaction.")]
    public float timeToHold = 10f;

    [Header("Interaction")]
    [Tooltip("The button to pick up or interact with certain things.")]
    public KeyCode Interact = KeyCode.E;

    [Header("Live Varibles")]
    [Tooltip("The current time the the Interact button has been held down.")]
    public float timeHeld;
    [Tooltip("The current amount the letters in ui should wobble.")]
    public float wobblePower;
    [Tooltip("True if the player is in the range. False if the player is to far away.")]
    public bool inRange;
    [Tooltip("TaskDone is true if a certain task is completed, such as Time Held reaching TimeToHold")]
    public bool taskDone;

    [Header("Debug")]
    [Tooltip("Enable Gizmos & Prints")]
    public bool debug;

    public void Reset() // add when script is added to object.
    {
        Player = LayerMask.GetMask(layerNames: "Player"); // automaticly sets the layer to the player layer.
        Transform uI = transform.Find("UI"); // tries to find the UI child object.

        if (uI != null) { uIObject = uI.gameObject; } // if it finds the UI child object, Connect it in the inspector.
        else { print("Could not find uI child object"); } // if it fails to find the UI child object, Send the print.
    }

    public void Awake() // if it did not connect when the script is added to the object, add it on start of game.
    {
        Player = LayerMask.GetMask(layerNames: "Player");
        Transform uI = transform.Find("UI");
        if (uI != null) { uIObject = uI.gameObject; } 
    }

    protected virtual void Update()
    {
        inRange = Physics2D.OverlapCircle(transform.position, range, Player);
        if (inRange) { uIObject.SetActive(true); if (debug) { print("Interface: In range"); } } //set uI component to Active. 
        else { uIObject.SetActive(false); if (debug) { print("Interface: Outside range"); } } // set uI component to Inactive.

        if (inRange && Input.GetKey(Interact) || taskDone)
        {
            timeHeld += Time.deltaTime;
            if (timeHeld >= timeToHold) { timeHeld = timeToHold; taskDone = true; }; //lock the time at timeToHold.
            wobblePower = Mathf.Lerp(0, timeToHold, timeHeld / timeToHold); //lock the WobblePower at timeToHold.
            if (debug) { print("wobblePower: " + wobblePower); }
        }
        else
        {
            wobblePower -= Time.deltaTime; //remove the wobblePower as time goes without holding down the Interact button.
            if(wobblePower <= 0) { wobblePower = 0; }; //lock the wobblePower to a minimum of 0.
            timeHeld -= Time.deltaTime; //remove some time timeHeld when outside of range.
            if(timeHeld <= 0) { timeHeld = 0f; } //lock the time to minimum of 0.
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
