using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager instance;

    [SerializeField]
    private Tilemap tileMap;
    [SerializeField] 
    private List<Vector3> locations = new List<Vector3>();

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        GetTiles();
    }

    public void SetTileMap(Tilemap tilemap)
    {
        this.tileMap = tilemap;
        GetTiles();
    }

    private void GetTiles()
    {
        for (int x = tileMap.cellBounds.xMin; x < tileMap.cellBounds.xMax; x++)
        {
            for (int y = tileMap.cellBounds.yMin; y < tileMap.cellBounds.yMax; y++)
            {
                Vector3Int localLocation = new Vector3Int(
                    x: x,
                    y: y,
                    z: 0);

                Vector3 location = tileMap.GetCellCenterWorld(localLocation);
                if (tileMap.HasTile(localLocation))
                {
                    if (!locations.Contains(location))
                    {
                        locations.Add(localLocation);
                    }
                }
            }
        }
    }

    public bool IsPlaceableArea(Vector3Int cellPos)
    {
        if (tileMap.GetTile(cellPos) == null)
        {
            return false;
        }
        return true;
    }

    public List<Vector3>  GetCellsPosition()
    {
        return locations;
    }

    public Vector3Int GetObjCell(Vector3 position)
    {
        Vector3Int cellPosition = tileMap.WorldToCell(position);
        return cellPosition;
    }

    public Vector3 PositonToMove(Vector3Int cellPosition)
    {
        return tileMap.GetCellCenterWorld(cellPosition);
    }
}
