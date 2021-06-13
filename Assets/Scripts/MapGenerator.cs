using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Map _mapObject;
    [SerializeField] private Vector2 _offset;
    
    private MapGrid _mapGrid;
    private Array2DInt _map;
    private List<GameObject> _prefabs;
    private Vector2Int _goalIndex;
    private Cell _goal;

    void Awake()
    {
        _map = _mapObject._map;
        _prefabs = _mapObject.prefabs;
        _mapGrid = new MapGrid();
        SpawnMap();
        AddNeighbours();
    }

    public Cell GetGoal()
    {
        List<Vector2Int> directions = new List<Vector2Int>();
        directions.Add(new Vector2Int(_goalIndex.x - 1, _goalIndex.y));
        directions.Add(new Vector2Int(_goalIndex.x + 1, _goalIndex.y));

        foreach (Cell goalCell in directions.Select(direction => GetCell(direction.x, direction.y)).Where(goalCell => goalCell))
        {
            _goal = goalCell;
            Debug.Log($"Goal cell is: {_goal}");
            return _goal;
        }
        Debug.LogError("No goal cell found");
        return null;
    }

    public Cell GetPoint(Cell start = null)
    {
        return _mapGrid.GetPoint(start);
    }

    public Cell[] GetFurthestPath(Cell start)
    {
        return _mapGrid.GetFurthestPath(start);
    }

    private Cell GetCell(int x, int y)
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
                if (road.Equals(_mapObject.goal))
                    _goalIndex = new Vector2Int(x, y);
                
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
}