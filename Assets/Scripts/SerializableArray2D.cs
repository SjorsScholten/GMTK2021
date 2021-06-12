using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableArray2D<T>
{
    public int sizeX;
    public int sizeY;
	
    [SerializeField] private T[] array;

    public SerializableArray2D(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        array = new T[sizeX * sizeY];
    }

    public T GetValue(int x, int y)
    {
        if (x >= sizeX || y >= sizeY)
        {
            Debug.LogError("Value out of range! " + x + " " + sizeX + " " + y + " " + sizeY);
            return default(T);
        }

        return (array[x + (y * sizeX)]);
    }

    public void SetValue(int x, int y, T value)
    {
        array[x + (y * sizeX)] = value;
    }
}

[Serializable]
public class Array2DGameObject : SerializableArray2D<GameObject> {
    public Array2DGameObject(int sizeX, int sizeY) : base(sizeX, sizeY) {}
}

[Serializable]
public class Array2DInt : SerializableArray2D<int> {
    public Array2DInt(int sizeX, int sizeY) : base(sizeX, sizeY) {}
}