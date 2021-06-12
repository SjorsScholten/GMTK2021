using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private Dictionary<Vector2Int, GameObject> _grid = new Dictionary<Vector2Int, GameObject>();

    public void AddToGrid(int x, int y, GameObject gameObject)
    {
        Vector2Int index = new Vector2Int(x, y);
        if (_grid.ContainsKey(index))
            return;
        
        _grid.Add(index, gameObject);
    }

    public GameObject GetCellObject(int x, int y)
    {
        Vector2Int index = new Vector2Int(x, y);
        if (_grid.ContainsKey(index))
            return _grid[index];

        return null;
    }
}
