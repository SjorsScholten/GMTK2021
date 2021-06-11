using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    
    [SerializeField] private Array2DGameObject _map = new Array2DGameObject(8,8);
    [SerializeField] private Vector2 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform mapHolder = SpawnMapHolder();
        for (int y = 0; y < _map.sizeY; y++)
        {
            for (int x = 0; x < (_map.sizeX); x++)
            {
                GameObject road = _map.GetValue(x, y);
                if(!road)
                    continue;
                
                Vector3 position = new Vector3(x * _offset.x, 0f, y * _offset.y);
                Instantiate(road, position, Quaternion.Euler(Vector3.zero), mapHolder.transform);
            }
        }
    }

    private Transform SpawnMapHolder()
    {
        const string holderName = "Map";
        Transform folder = transform.Find(holderName);
        if (folder) DestroyImmediate(folder.gameObject);
        return new GameObject(holderName).transform;
    }
}
