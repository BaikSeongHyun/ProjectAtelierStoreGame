using System;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
	// field
	[SerializeField] int id;
	[SerializeField] string file;
	[SerializeField] string name;
	[SerializeField] string guide;
	[SerializeField] int height;
	[SerializeField] int widthX;
	[SerializeField] int widthZ;
	[SerializeField] int step;
	[SerializeField] int[] firstDataTable;
	[SerializeField] int[] secondDataTable;
	[SerializeField] int slotLength;
	[SerializeField] FunctionType functionType;
	[SerializeField] AllocateType allocateType;

	// property
	public int ID { get { return id; } }

	public string File { get { return file; } }

	public string Name { get { return name; } }

	public string Guide { get { return guide; } }

	public int Height { get { return height; } }

	public int WidthX { get { return widthX; } }

	public int WidthZ { get { return widthZ; } }

	public int Step { get { return step; } }

	public int[] SellItemGroupSet { get { return firstDataTable; } }

	public int[] SellItemCountSet { get { return secondDataTable; } }

	public int SlotLength { get { return slotLength; } }

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
		functionType = FunctionType.Default;
		id = 0;
		file = null;
		name = null;
		guide = null;
		height = 0;
		widthX = 0;
		widthZ = 0;
		functionType = FunctionType.Default;
		step = 0;

		allocateType = AllocateType.Default;
	}

	// constructor - all parameter -> set up data
	public FurnitureData( int _type, int _id, string _file, string _name, string _guide, int _height, int _widthX, int _widthZ, int _step, int[]_firstDataTable, int[] _secondDataTable, int _slotLength, AllocateType _allocateType )
	{
		functionType = ReturnType( _type );
		id = _id;
		file = _file;
		name = _name;
		guide = _guide;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		step = _step;
		firstDataTable = _firstDataTable;
		secondDataTable = _secondDataTable;
		slotLength = _slotLength;

		// allocate function type
		switch( _type )
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
			default:
				Debug.Log( "error" );
				break;
		}

		allocateType = _allocateType;
	}

	FunctionType ReturnType( int _temp )
	{
		// allocate function type
		FunctionType type;

		switch( _temp )
		{
			case 1:
				type = FunctionType.CreateObject;
				break;
			case 2:
				type = FunctionType.SellObject;
				break;
			case 3:
				type = FunctionType.DecorateObject;
				break;
			case 4:
				type = FunctionType.StorageObject;
				break;
			default:
				type = FunctionType.Default;
				Debug.Log( "error" );
				break;
		}

		return type;
	}
}