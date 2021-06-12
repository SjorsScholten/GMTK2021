using System;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private UnitController[] _spawnableUnits;
	[SerializeField] private MapGenerator _map;
	public UnityEvent onGoalReached;

	private void Start() {
		SpawnUnit();
	}

	[ContextMenu("spawn")]
	public void SpawnUnit() {
		UnitController unit = Instantiate(_spawnableUnits[Random.Range(0, _spawnableUnits.Length)], transform);
		Cell startCell = _map.GetCell(0, 7);
		Cell endCell = _map.GetCell(7, 0);
		Cell[] path = Pathfinding.GetPath(startCell, endCell);
		unit.Initialize(path);
		unit.onGoalReached += onGoalReached.Invoke;
	}
}

