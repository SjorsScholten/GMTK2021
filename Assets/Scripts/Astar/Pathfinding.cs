using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding
{
    public static Cell[] GetPath(Cell start, Cell goal)
    {
        IList<INode> path = AStar.GetPath(start, goal);
        return path.Select(node => node as Cell).ToArray();
    }
}
