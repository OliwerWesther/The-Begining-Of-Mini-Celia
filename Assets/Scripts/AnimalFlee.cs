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

    // Keeps track of whether the animal is currently fleeing.
    private bool isFleeing = false;

    // Counts down while the animal is fleeing.
    // When this reaches 0, the animal is destroyed.
    private float fleeTimer = 0f;

    // Stores the direction the animal is currently trying to move.
    private Vector2 fleeDirection;


    private void Update()
    {
        // If the animal isn't fleeing, check if the player
        // has entered its detection radius.
        if (!isFleeing)
        {
            CheckForPlayer();
        }
        else
        {
            // If the animal is fleeing, handle its movement,
            // obstacle avoidance and timer.
            Flee();
        }
    }


    private void CheckForPlayer()
    {
        // OverlapCircle checks for a Collider2D inside the
        // detection radius around the animal.
        //
        // The playerLayer makes sure we only detect objects
        // assigned to the Player layer.
        //
        // If nothing is found, player will be null.
        Collider2D player = Physics2D.OverlapCircle(
            transform.position,
            detectionRadius,
            playerLayer
        );

        if (player != null)
        {
            StartFleeing(player.transform);
        }
    }


    private void StartFleeing(Transform player)
    {
        // Change the state so the animal is now fleeing.
        isFleeing = true;

        // Start the countdown.
        fleeTimer = fleeDuration;

        // Calculate the direction away from the player.
        //
        // Animal position - Player position gives us a vector
        // pointing from the player toward the animal.
        //
        // normalized() makes the vector have a length of 1,
        // so we can use it purely as a direction.
        fleeDirection =
            ((Vector2)transform.position - (Vector2)player.position).normalized;
    }


    private void Flee()
    {
        // Reduce the timer by the amount of time that passed
        // since the previous frame.
        //
        // Using Time.deltaTime means the timer is measured
        // in seconds rather than frames.
        fleeTimer -= Time.deltaTime;

        // Once the timer reaches zero, destroy the animal.
        if (fleeTimer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        // Check the available directions and find the best
        // direction that isn't blocked by an obstacle.
        fleeDirection = FindBestDirection(fleeDirection);

        // Move the animal in the chosen direction.
        //
        // Multiplying by Time.deltaTime makes the movement
        // independent of the game's frame rate.
        transform.position +=
            (Vector3)(fleeDirection * fleeSpeed * Time.deltaTime);
    }


    private Vector2 FindBestDirection(Vector2 currentDirection)
    {
        // Start by assuming that the current direction is
        // the best option.
        Vector2 bestDirection = currentDirection;

        // A very low starting score means that almost any
        // valid direction can become the new best direction.
        float bestScore = -Mathf.Infinity;


        // These are the directions the animal will consider.
        //
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
            // CircleCast checks whether there is an obstacle
            // in the direction we're considering.
            //
            // CircleCast is preferable to a simple Raycast here
            // because it gives the animal some width when checking.
            bool blocked = Physics2D.CircleCast(
                transform.position,
                animalRadius,
                direction,
                lookAheadDistance,
                obstacleLayer
            );

            // If an obstacle is found, ignore this direction
            // and move on to the next one.
            if (blocked)
                continue;


            // Dot compares the direction we're considering with
            // the direction the animal was already moving.
            //
            // A value close to 1 means the directions are similar.
            // A value close to 0 means they're roughly perpendicular.
            // A value close to -1 means they're opposite.
            //
            // This makes the animal prefer smooth turns instead
            // of constantly changing direction.
            float score = Vector2.Dot(direction, currentDirection);


            // If this direction is better than the previous best,
            // remember it.
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
