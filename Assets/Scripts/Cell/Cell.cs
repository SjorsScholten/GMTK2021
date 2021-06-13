using System;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class Cell : MonoBehaviour, INode {
    public MapGrid parentGrid;
    public bool walkable;
    public int gridX;
    public int gridY;

    public IEnumerable<INode> Neighbours => _neighbours;
    
    protected List<Cell> _neighbours = new List<Cell>();

    private Transform _transform;

    public Vector3 GridCoord => new Vector3(gridX, 0, gridY);

    protected virtual void Awake() {
        _transform = GetComponent<Transform>();
    }

    public void SetXY(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public float CostTo(INode neighbour)
    {
        return 1;
    }

    public float EstimatedCostTo(INode goal)
    {
        Cell goalCell = goal as Cell;
        int xDif = goalCell.gridX - gridX;
        int yDif = goalCell.gridY - gridY;

        if (xDif < 0)
            xDif = Mathf.Abs(xDif);
        if (yDif < 0)
            yDif = Mathf.Abs(yDif);
        
        return xDif + yDif;
    }
    
    public void AddNeighbours(List<Cell> neighbours)
    {
        _neighbours.AddRange(neighbours);

        if (this is Crossing && _neighbours.Count > 0)
        {
            Crossing crossing = this as Crossing;
            crossing.SetCanBeCrossed();
        }
    }

    public bool HasNeighborInDirection(Vector3 direction) {
        if (direction == Vector3.zero) return false;
        int x = Mathf.RoundToInt(gridX + direction.x);
        int y = Mathf.RoundToInt(gridY + direction.z);
        return parentGrid.GetCellObject(x, y); 
    }

    public virtual Vector3 GetPosition(Vector3 headingDirection = new Vector3()) {
        return _transform.position;
    }
    
    public virtual bool CanPass(Unit unit) {
        return walkable;
    }

    public override string ToString()
    {
        return $"{gridX}, {gridY}";
    }
    
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + new Vector3(0,0.3f,0), ToString());
    }
#endif
}
