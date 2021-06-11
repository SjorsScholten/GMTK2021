using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private UnitController prefabUnit;

	[ContextMenu("spawn")]
	public void SpawnUnit() {
		UnitController unit = Instantiate(prefabUnit, transform);
	}
}

