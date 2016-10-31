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
	public FurnitureData Furniture { get { return furniture; } }

	public Vector3 Position { get { return position; } set { position = value; } }

	public Quaternion Rotation { get { return rotation; } set { rotation = value; } }

	public bool IsAllocated { get { return isAllocated; } }

	public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } }

	// constructor
	// constructor -> no parameter -> set default
	public FurnitureInstance()
	{
		furniture = new FurnitureData();
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
	}

	// constructor -> make furniture uid
	public FurnitureInstance( int uid )
	{
		furniture = new FurnitureData( DataManager.FindFurnitureDataByUID( uid ) );
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
	}

	// constructor -> make data (use data)
	public FurnitureInstance( FurnitureData data )
	{
		furniture = new FurnitureData( data );
		position = Vector3.zero;
		rotation = Quaternion.identity;
		isAllocated = false;
	}

	// constructor -> copy instance
	public FurnitureInstance( FurnitureInstance data )
	{
		furniture = new FurnitureData( data.Furniture );
		position = data.position;
		rotation = data.rotation;
		isAllocated = data.isAllocated;
	}


}
