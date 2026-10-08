using UnityEditor.Build.Content;
using UnityEngine;

public class WaterTrail : MonoBehaviour
{
    public PlayerMovement pm;

    [Header("Effects")]
    public GameObject WaterPulseEffect;
    public GameObject WaterTrailEffect;

    [Header("")]
    public LayerMask waterLayer;
    public GameObject waterCheck;
    public float waterCheckRadius;

    [Header("Gizmos")]
    public bool showGizmo;
    
    private void Update()
    {
        bool waterDetection = Physics2D.OverlapCircle(waterCheck.transform.position, waterCheckRadius, waterLayer);
        if (waterDetection)
        {
            if (pm.rb.linearVelocity == new Vector2(0, 0))
            {
                WaterPulseEffect.SetActive(true);
                WaterTrailEffect.SetActive(false);
            }
            else
            {
                WaterPulseEffect.SetActive(false);
                WaterTrailEffect.SetActive(true);
            }
            print("Water Detected");
        }
        else
        {
            WaterPulseEffect.SetActive(false);
            WaterTrailEffect.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(waterCheck.transform.position, waterCheckRadius);
        }
        
    }
    
}
