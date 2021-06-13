using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_#", menuName = "Map/Create Map", order = 1)]
public class Map : ScriptableObject {
    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject goal;
    public Array2DInt _map = new Array2DInt(8,8);
}
