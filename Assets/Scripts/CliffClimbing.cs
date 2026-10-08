using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CliffClimbing : MonoBehaviour
{
    [System.Serializable]
    public struct LayerConfig
    {
        public string layerName;
        public LayerMask layerMask;
        public GameObject topCollider;
        public GameObject bottomCollider;
    }

    [Header("Controls & References")]
    [SerializeField] private KeyCode climbingButton = KeyCode.Space;
    [SerializeField] private Transform climbCheck;
    [SerializeField] private Tilemap cliffTilemap;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Collider2D playerCollider;

    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField, Range(0f, 0.3f)] private float groundCheckRadius = 0.01f;

    [Header("Layers")]
    [SerializeField] private LayerConfig[] layers;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private bool isClimbing;
    private int activeLayerIndex = -1;
    private static readonly int IsClimbingHash = Animator.StringToHash("isClimbing");

    private void Update()
    {

        if (Input.GetKeyDown(climbingButton) && !isClimbing)
        {
            TryClimbTilemap();
        }

        CheckAndSwapLayers();
    }

    private void CheckAndSwapLayers()
    {
        if (climbCheck == null || layers == null) return;

        for (int i = 0; i < layers.Length; i++)
        {
            Collider2D col = Physics2D.OverlapCircle(climbCheck.position, groundCheckRadius, layers[i].layerMask);
           
            bool isOverlapping = (col != null);

            if (isOverlapping)
            {
                // Only update GameObjects if the active layer actually changes
                if (activeLayerIndex != i)
                {
                    SetActiveLayer(i);
                }
                break; // Stop checking remaining layers once a match is found
            }
        }
    }

    private void SetActiveLayer(int newLayerIndex)
    {
        activeLayerIndex = newLayerIndex;

        if (debug)
        {
            Debug.Log($"Switched to: {layers[newLayerIndex].layerName}");
        }

        for (int i = 0; i < layers.Length; i++)
        {
            bool isActive = (i == newLayerIndex);

            if (layers[i].topCollider != null)
                layers[i].topCollider.SetActive(isActive);

            if (layers[i].bottomCollider != null)
                layers[i].bottomCollider.SetActive(isActive);
        }
    }

    private void TryClimbTilemap()
    {
        if (cliffTilemap == null) return;

        // 1. Check from climbCheck position (feet/legs) instead of transform.position
        Vector3 checkPos = climbCheck != null ? climbCheck.position : transform.position;
        Vector3Int cellPosition = cliffTilemap.WorldToCell(checkPos);

        // 2. Fetch only the exact tile at the climbCheck location
        TileBase tile = cliffTilemap.GetTile(cellPosition);

        if (tile is ClimbableTile climbableTile)
        {
            StartCoroutine(ClimbTileRoutine(climbableTile.climbOffset));
        }
    }

    private IEnumerator ClimbTileRoutine(Vector3 climbOffset)
    {
        isClimbing = true;

        if (playerCollider != null) playerCollider.enabled = false;
        if (playerAnimator != null) playerAnimator.SetBool(IsClimbingHash, true);

        if (debug) Debug.Log("Climbing started");

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + climbOffset;

        while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                climbSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;

        if (playerCollider != null) playerCollider.enabled = true;
        if (playerAnimator != null) playerAnimator.SetBool(IsClimbingHash, false);

        isClimbing = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (climbCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(climbCheck.position, groundCheckRadius);
        }
    }
}