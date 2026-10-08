using UnityEngine;

public class LeafOpacity : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public float opacity = 0.5f;

    public Material mat;

    public bool debug;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //spriteRenderer.color = new Color(0, 0, 0, opacity);
            //mat.color = new Color(0, 0, 0, opacity);
            if (debug)
            {
                print("Player Entered");
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //spriteRenderer.color = new Color(1, 1, 1, 1);
            //mat.color = new Color(1, 1, 1, 1);
            if (debug)
            {
                print("Player Exited");
            }
            
        }
    }
}
