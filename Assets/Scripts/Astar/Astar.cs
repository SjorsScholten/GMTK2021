using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
	{
		private class NodeSorter : IComparer<INode>
		{
			private Dictionary<INode, float> _fScore;

			public NodeSorter(Dictionary<INode, float> f)
			{
				_fScore = f;
			}

			public int Compare(INode x, INode y)
			{
				if (x != null && y != null)
					return _fScore[x].CompareTo(_fScore[y]);
				else
					return 0;
			}
		}

		private static List<INode> closed;
		private static List<INode> open;
		private static Dictionary<INode, INode> cameFrom;
		private static Dictionary<INode, float> gScore;
		private static Dictionary<INode, float> hScore;
		private static Dictionary<INode, float> fScore;

		static AStar()
		{
			closed = new List<INode>();
			open = new List<INode>();
			cameFrom = new Dictionary<INode, INode>();
			gScore = new Dictionary<INode, float>();
			hScore = new Dictionary<INode, float>();
			fScore = new Dictionary<INode, float>();
		}

		// this function is the C# implementation of the algorithm presented on the wikipedia page
		// start and goal are the nodes in the graph we should find a path for
		//
		// returns null if no path is found
		//
		// this function is NOT thread-safe (due to using static data for GC optimization)
		public static IList<INode> GetPath(INode start, INode goal)
		{
			if (start == null || goal == null)
			{
				return null;
			}

			closed.Clear();
			open.Clear();
			open.Add(start);

			cameFrom.Clear();
			gScore.Clear();
			hScore.Clear();
			fScore.Clear();

			gScore.Add(start, 0f);
			hScore.Add(start, start.EstimatedCostTo(goal));
			fScore.Add(start, hScore[start]);

			NodeSorter sorter = new NodeSorter(fScore);
			INode current,
						from = null;
			float tentativeGScore;
			bool tentativeIsBetter;

			while (open.Count > 0)
			{
				current = open[0];
				if (current == goal)
				{
					return ReconstructPath(new List<INode>(), cameFrom, goal);
				}
				open.Remove(current);
				closed.Add(current);

				if (current != start)
				{
					from = cameFrom[current];
				}
				foreach (INode next in current.Neighbours)
				{
					if (from != next && !closed.Contains(next))
					{
						tentativeGScore = gScore[current] + current.CostTo(next);
						tentativeIsBetter = true;

						if (!open.Contains(next))
						{
							open.Add(next);
						}
						else
						if (tentativeGScore >= gScore[next])
						{
							tentativeIsBetter = false;
						}

						if (tentativeIsBetter)
						{
							cameFrom[next] = current;
							gScore[next] = tentativeGScore;
							hScore[next] = next.EstimatedCostTo(goal);
							fScore[next] = gScore[next] + hScore[next];
						}
					}
				}
				open.Sort(sorter);
			}
			return null;
		}

		private static IList<INode> ReconstructPath(IList<INode> path, Dictionary<INode, INode> cameFrom, INode currentNode)
		{
			if (cameFrom.ContainsKey(currentNode))
			{
				ReconstructPath(path, cameFrom, cameFrom[currentNode]);
			}
			path.Add(currentNode);
			return path;
		}
	}