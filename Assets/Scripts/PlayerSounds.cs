using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    /// <summary>
    /// try using an array, and every ground check and checkRadius to be the same thing for a much more clear script.
    /// </summary>


    [Header("Grass Walking")]
    [SerializeField]PlayerMovement playerMovementScript;
    [SerializeField]GameObject grassWalkingSource;
    public GameObject grassCheck;
    float grassCheckRadius = 0.1f;
    public LayerMask grassLayer;

    [Header("Dirt Walking")]
    [SerializeField] GameObject dirtWalkingSource;
    public GameObject dirtCheck;
    float dirtCheckRadius = 0.1f;
    public LayerMask dirtLayer;

    [Header("Water Walking")]
    [SerializeField] GameObject waterWalkingSource;
    public GameObject waterCheck;
    float waterCheckRadius = 0.1f;
    public LayerMask waterLayer;


    private void Update()
    {
        //if moving on grass
        bool onGrass = Physics2D.OverlapCircle(grassCheck.transform.position, grassCheckRadius, grassLayer);
        if (onGrass)
        {
            waterWalkingSource.SetActive(false);
            dirtWalkingSource.SetActive(false);
            if (playerMovementScript.rb.linearVelocity != new Vector2(0, 0))
            {
                grassWalkingSource.SetActive(true);

            }
            else
            {
                grassWalkingSource.SetActive(false);
                
            }
        }

        //if moving on dirt
        bool onDirt = Physics2D.OverlapCircle(dirtCheck.transform.position, dirtCheckRadius, dirtLayer);
        if (onDirt)
        {
            waterWalkingSource.SetActive(false);
            grassWalkingSource.SetActive(false);
            if (playerMovementScript.rb.linearVelocity != new Vector2(0, 0))
            {
                dirtWalkingSource.SetActive(true);
            }
            else
            {
                dirtWalkingSource.SetActive(false);
            }
        }

        bool onWater = Physics2D.OverlapCircle(waterCheck.transform.position, waterCheckRadius, waterLayer);
        if (onWater)
        {
            dirtWalkingSource.SetActive(false);
            grassWalkingSource.SetActive(false);
            if (playerMovementScript.rb.linearVelocity != new Vector2(0, 0))
            {
                waterWalkingSource.SetActive(true);
            }
            else
            {
                waterWalkingSource.SetActive(false);
            }
        }

    }
}
