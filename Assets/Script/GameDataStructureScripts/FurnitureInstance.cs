using System;
using UnityEngine;

[System.Serializable]
public class FurnitureInstance
{
	// field
	[SerializeField] FurnitureData furniture;
	[SerializeField] Vector3 position;
	[SerializeField] Quaternion rotation;
	[SerializeField] bool isAllocated;
	[SerializeField] int slotNumber;

	// property
	public FurnitureData Furniture { get { return furniture; } set { furniture = value; } }

	public Vector3 Position { get { return position; } set { position = value; } }

	public Quaternion Rotation { get { return rotation; } set { rotation = value; } }

	public bool IsAllocated { get { return isAllocated; } set { isAllocated = value; } }

	public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } }

	// constructor
	// constructor -> no parameter -> set default
	public FurnitureInstance()
	{
		furniture = null;
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
	}

	// constructor -> make furniture uid
	public FurnitureInstance( int id, int _slotNumber )
	{
		furniture = DataManager.FindFurnitureDataByID( id );
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
		slotNumber = _slotNumber;
	}

	// constructor -> make data (use data)
	public FurnitureInstance( FurnitureData data, int _slotNumber )
	{
		furniture = data;
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
		slotNumber = _slotNumber;
	}

	// constructor -> make data ( allocated data )
	public FurnitureInstance( int _id, int _slotNumber, bool _allocated, Vector3 _position, Quaternion _rotation )
	{
		furniture = DataManager.FindFurnitureDataByID( _id );
		slotNumber = _slotNumber;
		isAllocated = _allocated;
		position = _position;
		rotation = _rotation;
	}

	// constructor -> copy instance
	public FurnitureInstance( FurnitureInstance data, int _slotNumber )
	{
		furniture = data.Furniture;
		position = data.position;
		rotation = data.rotation;
		isAllocated = data.isAllocated;
		slotNumber = _slotNumber;
	}

	// constructor -> copy instance
	public FurnitureInstance( FurnitureInstance data )
	{
		furniture = data.Furniture;
		position = data.position;
		rotation = data.rotation;
		isAllocated = data.isAllocated;
		slotNumber = data.slotNumber;
	}

	// public method
	public void AllocateInstance( int _slotNumber )
	{
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = true;
		slotNumber = _slotNumber;
	}

}
