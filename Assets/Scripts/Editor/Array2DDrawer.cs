using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Array2DDrawer : PropertyDrawer {
	
	protected abstract int ElementWidth { get; }

	protected virtual int ElementHeight
	{
		get { return (int) EditorGUIUtility.singleLineHeight; }
	}

	private int _setSizeX;
	private int _setSizeY;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		SerializedProperty sizeY = property.FindPropertyRelative("sizeY");
		return (ElementHeight * sizeY.intValue) + EditorGUIUtility.singleLineHeight;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		SerializedProperty arrayProp = property.FindPropertyRelative("array");
		SerializedProperty sizeX = property.FindPropertyRelative("sizeX");
		SerializedProperty sizeY = property.FindPropertyRelative("sizeY");

		Rect setSizeXRect = new Rect(position.x, position.y, 20, EditorGUIUtility.singleLineHeight);
		Rect setSizeYRect = new Rect(setSizeXRect.xMax, position.y, 20, EditorGUIUtility.singleLineHeight);
		Rect setSizeButton = new Rect(setSizeYRect.xMax + 5, position.y, 100, EditorGUIUtility.singleLineHeight);
		if (_setSizeX == 0 && sizeX.intValue > 0) _setSizeX = sizeX.intValue;
		if (_setSizeY == 0 && sizeY.intValue > 0) _setSizeY = sizeY.intValue;

		_setSizeX = EditorGUI.IntField(setSizeXRect, _setSizeX);
		_setSizeY = EditorGUI.IntField(setSizeYRect, _setSizeY);

		if (GUI.Button(setSizeButton, "Resize Array"))
		{
			SetArraySize(arrayProp, sizeX, sizeY, _setSizeX, _setSizeY);
		}

		position = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width,
			position.height);

		int yOffset = 0;
		for (int y = 0; y < sizeY.intValue; y++)
		{
			int xOffset = 0;
			for (int x = 0; x < sizeX.intValue; x++)
			{
				Rect rect = new Rect(position.x + xOffset, position.y + yOffset, ElementWidth, ElementHeight);

				int index = XYToIndex(sizeX, sizeY, x, y);
				SerializedProperty prop = arrayProp.GetArrayElementAtIndex(index);
				EditorGUI.PropertyField(rect, prop, GUIContent.none);
				xOffset += ElementWidth + 5;
			}

			yOffset += ElementHeight;
		}
		
		EditorGUI.EndProperty();
	}

	int XYToIndex(SerializedProperty sizeX, SerializedProperty sizeY, int x, int y)
	{
		return (x + y * sizeX.intValue);
	}

	void SetArraySize(SerializedProperty array, SerializedProperty xSize, SerializedProperty ySize, int x, int y)
	{
		xSize.intValue = x;
		ySize.intValue = y;
		array.arraySize = x * y;
	}
}

[CustomPropertyDrawer(typeof(Array2DGameObject))] // This ties the Array2DDrawer to the Array2DBool
public class Array2DGameObjectDrawer : Array2DDrawer
{
	protected override int ElementWidth
	{
		// The Array2DDrawer class below does all the hard stuff; here we can just say how wide each element should be.
		// Try changing this number to see what happens to the drawer
		get { return 60; } 
	}
}

[CustomPropertyDrawer(typeof(Array2DInt))] // This ties the Array2DDrawer to the Array2DBool
public class Array2DIntDrawer : Array2DDrawer
{
	protected override int ElementWidth
	{
		// The Array2DDrawer class below does all the hard stuff; here we can just say how wide each element should be.
		// Try changing this number to see what happens to the drawer
		get { return 25; } 
	}
}