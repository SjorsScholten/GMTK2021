using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapGrid {
    private Dictionary<Vector2Int, Cell> _grid = new Dictionary<Vector2Int, Cell>();

    public void AddToGrid(int x, int y, Cell cell)
    {
        Vector2Int index = new Vector2Int(x, y);
        if (_grid.ContainsKey(index))
            return;
        
        _grid.Add(index, cell);
    }

    public Cell GetCellObject(int x, int y)
    {
        Vector2Int index = new Vector2Int(x, y);
        if (_grid.ContainsKey(index))
            return _grid[index];

        return null;
    }

    public Cell GetPoint(Cell start = null)
    {
        List<Cell> cellsInGrid = _grid.Values.ToList();
        cellsInGrid.RemoveAll(c => c.Neighbours.Count() > 1 || !c.Neighbours.Any() || c.Equals(start));
        return cellsInGrid.Count > 0 ? cellsInGrid[Random.Range(0, cellsInGrid.Count)] : null;
    }
    
    public Cell[] GetFurthestPath(Cell start)
    {
        List<Cell> cellsInGrid = _grid.Values.ToList();
        cellsInGrid.RemoveAll(c => c.Neighbours.Count() > 1 || !c.Neighbours.Any() || c.Equals(start));
        if (cellsInGrid.Count <= 0)
            return null;

        Cell[] furthestPath = null;
        int pathCount = 0;

        foreach (Cell[] path in cellsInGrid.Select(cell => Pathfinding.GetPath(start, cell)).Where(path => path.Count() > pathCount))
        {
            pathCount = path.Count();
            furthestPath = path;
        }
        return furthestPath;

    }
}
