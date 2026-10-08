using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Climbable Tile", menuName = "Tiles/Climbable Tile")]
public class ClimbableTile : Tile
{
    [Header("Climb Properties")]
    [Tooltip("Offset relative to player position. e.g., (0, 1, 0) for straight up, (-1, 1, 0) for diagonal left.")]
    public Vector3 climbOffset = new Vector3(0f, 1f, 0f);
}