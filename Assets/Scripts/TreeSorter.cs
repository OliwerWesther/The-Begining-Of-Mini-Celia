using UnityEngine;

public class TreeSorter : MonoBehaviour
{
    Transform player;
    SpriteRenderer treeRend;
    SpriteRenderer logRend;
    float playerOffset;
    float treeOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        treeRend = GetComponent<SpriteRenderer>();
        logRend = GetComponentInChildren<SpriteRenderer>();
        playerOffset =  player.GetChild(4).GetChild(0).position.y - player.position.y;
        treeOffset = transform.GetChild(0).GetChild(0).position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y + playerOffset < transform.position.y + treeOffset)
        {
            treeRend.sortingOrder = -1;
            logRend.sortingOrder = -1;
        }
        else
        {
            treeRend.sortingOrder = 1;
            logRend.sortingOrder = 1;
        }
    }
}
