using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentCollisionHandler : MonoBehaviour
{
    private Tilemap _tilemap;
    private TilemapCollider2D _tilemapCollider2D;
    private Vector3Int _tileHitCellPosition;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _tilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    public void RemoveTile(Vector3 worldCoordinates)
    {
        // Convert world hit location to a cell position on the tilemap.
        _tileHitCellPosition = _tilemap.WorldToCell(worldCoordinates);
        
        //Make the tile at this coordinate dissapear.
        _tilemap.SetTile(_tileHitCellPosition, null);

        //Refresh tilemap collider.
        _tilemapCollider2D.enabled = false;
        _tilemapCollider2D.enabled = true;
    }
}
