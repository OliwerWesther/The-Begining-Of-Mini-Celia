using UnityEngine;

public class AnimalFlee : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Flee")]
    [SerializeField] private float fleeSpeed = 5f;
    [SerializeField] private float fleeDuration = 3f;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float animalRadius = 0.3f;
    [SerializeField] private float lookAheadDistance = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    private bool isFleeing = false;

    private float fleeTimer = 0f;

    private Vector2 fleeDirection;


    private void Update()
    {
        if (!isFleeing)
        {
            CheckForPlayer();
        }
        else
        {
            Flee();
        }
    }


    private void CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (player != null)
        {
            StartFleeing(player.transform);
        }
    }


    private void StartFleeing(Transform player)
    {
        isFleeing = true;

        fleeTimer = fleeDuration;

        fleeDirection = ((Vector2)transform.position - (Vector2)player.position).normalized;
    }


    private void Flee()
    {
        fleeTimer -= Time.deltaTime;

        // Once the timer reaches zero, destroy the animal.
        if (fleeTimer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        fleeDirection = FindBestDirection(fleeDirection);

        transform.position += (Vector3)(fleeDirection * fleeSpeed * Time.deltaTime);
    }


    private Vector2 FindBestDirection(Vector2 currentDirection)
    {
        Vector2 bestDirection = currentDirection;

        // A very low starting score means that almost any,valid direction can become the new best direction.
        float bestScore = -Mathf.Infinity;

        // The diagonal vectors are normalized so that diagonal
        // movement isn't faster than horizontal or vertical movement.
        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,

            new Vector2(1, 1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, -1).normalized
        };


        foreach (Vector2 direction in directions)
        {
            bool blocked = Physics2D.CircleCast(transform.position,animalRadius,direction,lookAheadDistance,obstacleLayer);

            // If an obstacle is found, ignore this direction
            // and move on to the next one.
            if (blocked)
                continue;

            float score = Vector2.Dot(direction, currentDirection);

            if (score > bestScore)
            {
                bestScore = score;
                bestDirection = direction;
            }
        }


        // Return the best available direction.
        return bestDirection;
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the Unity Scene view.
        // This is only for visualization and doesn't affect gameplay.
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRadius
        );
    }
}
