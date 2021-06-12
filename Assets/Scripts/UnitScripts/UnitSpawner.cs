using System;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private UnitController[] _spawnableUnits;
	[SerializeField] private MapGenerator _map;
	[SerializeField, Range(1, 10)] private int _initialSpawn = 1;
	public UnityEvent onGoalReached;

	private void Start() {
		for (int i = 0; i < _initialSpawn; i++) {
			SpawnUnit();
		}
	}

	[ContextMenu("spawn")]
	public void SpawnUnit() {
		UnitController unit = Instantiate(_spawnableUnits[Random.Range(0, _spawnableUnits.Length)], transform);
		Cell[] path = GeneratePath();
		unit.Initialize(path);
		unit.onGoalReached += onGoalReached.Invoke;
	}

	private Cell[] GeneratePath() {
		/*Cell[] spawns = _map.GetSpawnPoints();
		Cell startCell = spawns[Random.Range(0, spawns.Length)];
		Random.seed = System.DateTime.Now.Millisecond;
		Cell endCell = spawns[Random.Range(0, spawns.Length)];*/

		Cell startCell = _map.GetPoint();
		Cell endCell = _map.GetPoint(startCell);
		return Pathfinding.GetPath(startCell, endCell);
	}
}

