using System;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
	// field
	[SerializeField] int uid;
	[SerializeField] string name;
	[SerializeField] int height;
	[SerializeField] int widthX;
	[SerializeField] int widthZ;
	[SerializeField] int level;
	[SerializeField] string objectName;
	[SerializeField] FunctionType functionType;
	[SerializeField] AllocateType allocateType;

	// property
	public int UID { get { return uid; } }

	public string Name { get { return name; } }

	public int Height { get { return height; } }

	public int WidthX { get { return widthX; } }

	public int WidthZ { get { return widthZ; } }

	public int Level { get { return level; } }

	public string ObjectName { get { return objectName; } }

	public FunctionType Function { get { return functionType; } }

	public AllocateType Allocate { get { return allocateType; } }

	// enum type
	// function type
	public enum FunctionType : int
	{
		Default = 0,
		CreateObject = 1,
		SellObject = 2,
		DecorateObject = 3,
		StorageObject = 4}

	;

	// allocate type
	public enum AllocateType : int
	{
		Default = 0,
		Field = 1,
		Wall = 2,
		Wherever = 3}

	;

	// constructor - no parameter -> set default;
	public FurnitureData()
	{
		uid = 0;
		height = 0;
		widthX = 0;
		widthZ = 0;
		objectName = null;
		functionType = FunctionType.Default;
		allocateType = AllocateType.Default;
	}

	// constructor - all parameter -> set up data
	public FurnitureData( int _uid, string _name, int _height, int _widthX, int _widthZ, int _level, string _objectName, string _guide, AllocateType _allocateType )
	{
		uid = _uid;
		name = _name;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		level = _level;
		objectName = _objectName;

		// allocate function type
		switch( _uid / 10000 )
		{
			case 1:
				functionType = FunctionType.CreateObject;
				break;
			case 2:
				functionType = FunctionType.SellObject;
				break;
			case 3:
				functionType = FunctionType.DecorateObject;
				break;
			case 4:
				functionType = FunctionType.StorageObject;
				break;
		}

		allocateType = _allocateType;
	}

	// constructor - data parameter -> make same data instance
	public FurnitureData( FurnitureData data )
	{
		uid = data.uid;
		height = data.height;
		widthX = data.widthX;
		widthZ = data.widthZ;
		objectName = data.objectName;
		functionType = data.functionType;
		allocateType = data.allocateType;
	}
}