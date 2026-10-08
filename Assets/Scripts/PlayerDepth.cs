using UnityEngine;

public class PlayerDepth : MonoBehaviour
{

    public GameObject player;
    public float YAxis;
    SpriteRenderer Sr;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= YAxis)
        {
            Sr.renderingLayerMask -= 1;        }
        else return;
    }
}
