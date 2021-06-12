using System.Collections.Generic;
using UnitScripts;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cell : MonoBehaviour, INode, IRoad {
    
    public bool walkable;
    public int gridX;
    public int gridY;

    public IEnumerable<INode> Neighbours => _neighbours;
    
    protected List<Cell> _neighbours = new List<Cell>();

    private Transform _transform;
    private Vector3 _center = Vector3.zero;
    private int _slots = 2;
    public bool canPass = true;

    private void Awake() {
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

    public Vector3[] CornerVertices {
        get {
            Vector3[] corners = new Vector3[4];
            Vector3 position = _transform.position;
            corners[0] = position + (Vector3.left + Vector3.up) / 2;
            corners[1] = position + (Vector3.right + Vector3.up) / 2;
            corners[2] = position + (Vector3.right + Vector3.down) / 2;
            corners[3] = position + (Vector3.left + Vector3.down) / 2;
            return corners;
        }
    }
    
    public Vector3 Center {
        get {
            if (_center != Vector3.zero) return _center;
				
            foreach (Vector3 vertice in CornerVertices) _center += vertice;
            _center /= CornerVertices.Length;
				
            return _center;
        }
    }
    
    public bool CanPass() {
        return walkable && canPass && _slots > 0;
    }

    public bool Enter() {
        if (_slots > 0) {
            _slots--;
            return true;
        }

        return false;
    }

    public bool Exit() {
        _slots++;
        return true;
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
