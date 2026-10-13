using UnityEngine;

public class LeafOpacity : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public float opacity = 0.5f;




    public bool debug;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetOpacity(opacity);
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
            SetOpacity(1f);
            if (debug)
            {
                print("Player Exited");
            }
            
        }
    }
    private void SetOpacity(float value)
    {
        Color color = spriteRenderer.color;
        color.a = value;
        spriteRenderer.color = color;
        spriteRenderer.material.SetColor("_ColorVariation", color);
    }
}
