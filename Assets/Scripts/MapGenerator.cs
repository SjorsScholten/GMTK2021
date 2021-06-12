using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private Map _mapObject;
    [SerializeField] private Vector2 _offset;
    
    private MapGrid _mapGrid;
    private Array2DInt _map;
    
    void Awake()
    {
        _map = _mapObject._map;
        _mapGrid = new MapGrid();
        SpawnMap();
        AddNeighbours();
    }

    public Cell GetCell(int x, int y)
    {
        Cell cell = _mapGrid.GetCellObject(x, y);
        if (cell)
            return cell;
        return null;
    }
    
    private void SpawnMap()
    {
        Transform mapHolder = SpawnMapHolder();
        for (int y = 0; y < _map.sizeY; y++)
        {
            for (int x = 0; x < (_map.sizeX); x++)
            {
                int index = _map.GetValue(x, y);
                if (index > _prefabs.Count)
                    index = 0;
                GameObject road = _prefabs[index];
                Vector3 position = new Vector3(x * _offset.x, 0f, y * _offset.y);
                GameObject cellObject = Instantiate(road, position, Quaternion.Euler(Vector3.zero), mapHolder.transform);
                Cell cell = cellObject.GetComponent<Cell>();
                
                if (!cell)
                    continue;
                
                cell.SetXY(x, y);
                cellObject.name = cell.ToString();
                _mapGrid.AddToGrid(x, y, cell);
            }
        }
    }

    private Transform SpawnMapHolder()
    {
        const string holderName = "Map";
        Transform folder = transform.Find(holderName);
        if (folder) DestroyImmediate(folder.gameObject);
        return new GameObject(holderName).transform;
    }

    private void AddNeighbours()
    {
        for (int x = 0; x < _map.sizeY; x++)
        {
            for (int y = 0; y < _map.sizeX; y++)
            {
                Cell cell = GetCell(x, y);
                if (!cell)
                    continue;
                
                Vector2Int up = new Vector2Int(cell.gridX, cell.gridY + 1);
                Vector2Int down = new Vector2Int(cell.gridX, cell.gridY - 1);
                Vector2Int left = new Vector2Int(cell.gridX - 1, cell.gridY);
                Vector2Int right = new Vector2Int(cell.gridX + 1, cell.gridY);
                List<Cell> neighbours = new List<Cell>();
                AddNeighbours(ref neighbours, up);
                AddNeighbours(ref neighbours, down);
                AddNeighbours(ref neighbours, left);
                AddNeighbours(ref neighbours, right);
                cell.AddNeighbours(neighbours);
            }
        }
    }

    private void AddNeighbours(ref List<Cell> neighbours, Vector2Int index)
    {
        if (index.x < 0 || index.x >= _map.sizeX || index.y < 0 || index.y >= _map.sizeY)
            return;

        Cell neighbour = GetCell(index.x, index.y);
        if(neighbour && neighbour.walkable)
            neighbours.Add(neighbour);
    }

    public Cell[] GetSpawnPoints() {
        List<Cell> spawns = new List<Cell>();
        for (int x = 0; x < _map.sizeX; x++) {
            for (int y = 0; y < _map.sizeY; y++) {
                Cell c = _mapGrid.GetCellObject(x, y);
                if(!c) continue;
                Road r = c as Road;
                if (r) spawns.Add(r);
            }
        }

        return spawns.ToArray();
    }

    public Cell GetPoint(Cell start = null)
    {
        return _mapGrid.GetPoint(start);
    }
}