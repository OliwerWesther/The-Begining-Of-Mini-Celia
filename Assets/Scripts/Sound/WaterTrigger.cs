using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject duckSource;
    public GameObject gentleWaterSource;

    public AudioSource playerSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            duckSource.SetActive(true);
            gentleWaterSource.SetActive(true);

            playerSource.volume = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            duckSource.SetActive(false);
            gentleWaterSource.SetActive(false);

            playerSource.volume = 0.1f;
        }
    }
}