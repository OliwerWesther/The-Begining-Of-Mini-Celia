using UnityEngine;
using UnityEngine.Internal;

public class PlayerFollow : MonoBehaviour
{
    public GameObject player;
    [Range(-20f,0f)]public float ZPosition = -11f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,ZPosition);
    }
}
