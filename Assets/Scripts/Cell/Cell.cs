using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, INode {
    
    public bool walkable;
    public int gridX;
    public int gridY;

    public IEnumerable<INode> Neighbours => _neighbours;
    
    private List<Cell> _neighbours = new List<Cell>();
    
    public Cell(int gridX, int gridY)
    {
        this.gridX = gridX;
        this.gridY = gridY;
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
    }
}
