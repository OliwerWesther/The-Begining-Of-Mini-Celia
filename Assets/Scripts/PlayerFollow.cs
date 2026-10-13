using UnityEngine;
using UnityEngine.Internal;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    [Range(-20f,0f)]public float ZPosition = -11f;

    private void Start()
    {
        if (player == null)
        {
            PlayerMovement movementScript = FindFirstObjectByType<PlayerMovement>();
            if (movementScript != null) { player = movementScript.transform; }
            else { Debug.Log("PlayerFollow: No GameObject with a PlayerMovement script was found.", this); };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; };
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,ZPosition);
    }
}
