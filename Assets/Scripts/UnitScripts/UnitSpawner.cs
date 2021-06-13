using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private UnitController[] _spawnableUnits;
	[SerializeField] private UnitController[] _spawnableGoalUnits;
	[SerializeField] private MapGenerator _map;
	[SerializeField, Range(1, 100)] private int _initialSpawn = 1;
	public UnityEvent onGoalReached;
	public UnityEvent onPathReached;

	private void Start() {
		for (int i = 0; i < _initialSpawn; i++) SpawnUnit();

		Invoke(nameof(SpawnGoalUnit), 2f);
	}

	[ContextMenu("spawn")]
	public void SpawnUnit()
	{
		UnitController unit = Instantiate(_spawnableUnits[Random.Range(0, _spawnableUnits.Length)], transform);
		Cell[] path = GeneratePath();
		unit.Initialize(path);
		unit.onGoalReached += onPathReached.Invoke;
	}

	private Cell[] GeneratePath() {
		Cell startCell = _map.GetPoint();
		Cell endCell = _map.GetPoint(startCell);
		return Pathfinding.GetPath(startCell, endCell);
	}

	private void SpawnGoalUnit()
	{
		UnitController unit = Instantiate(_spawnableGoalUnits[Random.Range(0, _spawnableGoalUnits.Length)], transform);
		Cell[] path = _map.GetFurthestPath(_map.GetGoal());
		path = path.Reverse().ToArray();
		unit.Initialize(path, false);
		unit.onGoalReached += onGoalReached.Invoke;
		unit.onGoalReached += GoalReached;
	}

	private static void GoalReached()
	{
		ArrayList controllersList =  new ArrayList();
		controllersList.AddRange(FindObjectsOfType<UnitController>().ToList());
		foreach (UnitController controller in controllersList)
		{
			Destroy(controller);
		}
	}
}

