using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// game player instance data
[System.Serializable]
public class PlayerData
{
	// game data field
	[SerializeField] string name;
	[SerializeField] string charType;
	[SerializeField] int level;
	[SerializeField] int fame;
	[SerializeField] int charm;
	[SerializeField] int gold;
	[SerializeField] int gem;
	[SerializeField] StoreData haveStoreData;
	[SerializeField] ItemInstance[] haveItemSet;
	[SerializeField] FurnitureInstance[] haveFurnitureSet;
	[SerializeField] List<FurnitureInstance> allocateFurnitureSet;
	[SerializeField] List<FurnitureObject> allocateFurnitureObjectSet;

	// property
	public string Name { get { return name; } set { name = value; } }

	public string CharacterType { get { return charType; } set { charType = value; } }

	public int Level { get { return level; } set { level = value; } }

	public int Fame { get { return fame; } set { fame = value; } }

	public int Charm { get { return charm; } set { charm = value; } }

	public int Gold { get { return gold; } set { gold = value; } }

	public int Gem { get { return gem; } set { gem = value; } }

	public StoreData StoreData { get { return haveStoreData; } set { haveStoreData = value; } }

	public ItemInstance[] ItemSet { get { return haveItemSet; } set { haveItemSet = value; } }

	public FurnitureInstance[] FurnitureSet { get { return haveFurnitureSet; } set { haveFurnitureSet = value; } }

	public List<FurnitureInstance> AllocateFurnitureSet { get { return allocateFurnitureSet; } }

	public List<FurnitureObject> AllocateFurnitureObjectSet { get { return allocateFurnitureObjectSet; } set { allocateFurnitureObjectSet = value; } }

	// consturctor - no parameter
	public PlayerData()
	{
		level = 1;
		name = "";
		allocateFurnitureSet = new List<FurnitureInstance>();
		allocateFurnitureObjectSet = new List<FurnitureObject>();
	}

	// public method
	// add furniture data -> use only data
	public bool AddFurnitureData( FurnitureData data )
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null || haveFurnitureSet[ i ].Furniture == null || haveFurnitureSet[ i ].Furniture.ID == 0 )
			{
				haveFurnitureSet[ i ] = new FurnitureInstance( data, i );
				return true;
			}
		}

		Debug.Log( "Furniture inventory is full" );
		return false;
	}

	// add furniture data -> move instance data
	public bool AddFurnitureData( int id )
	{
		
		for( int i = 0; i < ( int ) haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null || haveFurnitureSet[ i ].Furniture == null || haveFurnitureSet[ i ].Furniture.ID == 0 )
			{
				haveFurnitureSet[ i ] = new FurnitureInstance( DataManager.FindFurnitureDataByID( id ), i );
				haveFurnitureSet[ i ].IsAllocated = false;
				return true;
			}
		}

		Debug.Log( "Furniture inventory is full" );
		return false;
	}

	// allocate furniture instance
	public bool AllocateFurnitureInstance( int index, int presentStepIndex )
	{	
		int processIndex = index + (presentStepIndex * (haveFurnitureSet.Length / 3));
		try
		{
			if( haveFurnitureSet[ processIndex ] == null || haveFurnitureSet[ processIndex ].Furniture == null || haveFurnitureSet[ processIndex ].Furniture.ID == 0 )
				return false;
			else
			{
				allocateFurnitureSet.Add( new FurnitureInstance( haveFurnitureSet[ processIndex ] ) );
				allocateFurnitureSet[ allocateFurnitureSet.Count - 1 ].AllocateInstance( allocateFurnitureSet.Count - 1 );
				haveFurnitureSet[ processIndex ] = new FurnitureInstance();
				return true;
			}
		}
		catch( Exception e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			return false;
		}
	}

	// delete allocate furniture instance
	public bool DeleteAllocateFurniture( int index )
	{	
		// reload slot number
		for( int i = 0; i < allocateFurnitureSet.Count; i++ )
		{
			allocateFurnitureSet[ i ].SlotNumber = i;
			allocateFurnitureObjectSet[ i ].InstanceData.SlotNumber = i;
		}

		// remove furniture
		allocateFurnitureObjectSet[ index ].ObjectAllocateOff();
		allocateFurnitureObjectSet.RemoveAt( index );
		allocateFurnitureSet.RemoveAt( index );

		// reload slot number
		for( int i = 0; i < allocateFurnitureSet.Count; i++ )
		{
			allocateFurnitureSet[ i ].SlotNumber = i;
			allocateFurnitureObjectSet[ i ].InstanceData.SlotNumber = i;
		}

		return true;		
	}

	// sell furniture instance
	public bool SellAllocateFurniture( int index )
	{
		try
		{
			// remove object
			allocateFurnitureObjectSet[ index ].ObjectAllocateOff();
			allocateFurnitureObjectSet.RemoveAt( index );

			// gold add
			gold += ( int ) (allocateFurnitureSet[ index ].Furniture.Price * 0.8f);

			// remove furniture
			allocateFurnitureSet.RemoveAt( index );

			// reload slot number
			for( int i = 0; i < allocateFurnitureSet.Count; i++ )
			{
				allocateFurnitureSet[ i ].SlotNumber = i;
				allocateFurnitureObjectSet[ i ].InstanceData.SlotNumber = i;
			}

			return true;
		}
		catch( Exception e )
		{
			Debug.Log( e.Message );
			Debug.Log( e.StackTrace );

			return false;
		}
	}

	// add item data
	public bool AddItemData( int id, int itemCount )
	{
		// check item type
		ItemData temp = DataManager.FindItemDataByID( id );
		
		// if item is exist
		foreach( ItemInstance element in haveItemSet )
		{
			if( element != null && element.Item != null && element.Item.ID == id )
			{
				element.Count += itemCount;
				return true;
			}
		}

		// find empty space
		for( int i = 0; i < haveItemSet.Length; i++ )
		{
			if( haveItemSet[ i ] == null || haveItemSet[ i ].Item == null || haveItemSet[ i ].Item.ID == 0 )
			{
				haveItemSet[ i ] = new ItemInstance( id, i, itemCount );
				return true;
			}
		}
		return false;
	}

	// material -> add item
	public void AddItemMaterial( int id, int itemCount )
	{
		AddItemData( id, itemCount );
	}

	// set default status
	public void SetDefaultStatus()
	{
		name = "";
		charType = "Berry";
		level = 1;
		fame = 0;
		charm = 0;
		gold = 10000;
		gem = 1000;
		haveStoreData = new StoreData( DataManager.FindStoreDataByStep( 1 ), 0 );
		haveItemSet = new ItemInstance[60];
		haveFurnitureSet = new FurnitureInstance[30];
		allocateFurnitureSet = new List<FurnitureInstance>();

		allocateFurnitureSet.Add( new FurnitureInstance( 19, 0, true, new Vector3( 2f, 0f, 2.5f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 20, 0, true, new Vector3( 5f, 0f, 6f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 21, 0, true, new Vector3( 2f, 0f, 6f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 22, 0, true, new Vector3( 4f, 0f, 0.5f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 23, 0, true, new Vector3( 1.5f, 0f, 0.5f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 24, 0, true, new Vector3( 5f, 0f, 2.5f ), Quaternion.identity ) );

		haveItemSet[ 0 ] = new ItemInstance( 1, 0, 10 );
		haveItemSet[ 1 ] = new ItemInstance( 2, 1, 10 );
		haveItemSet[ 2 ] = new ItemInstance( 3, 2, 10 );
		haveItemSet[ 3 ] = new ItemInstance( 4, 3, 10 );
		haveItemSet[ 4 ] = new ItemInstance( 5, 4, 10 );
		haveItemSet[ 5 ] = new ItemInstance( 6, 5, 10 );
		haveItemSet[ 6 ] = new ItemInstance( 7, 6, 10 );
		haveItemSet[ 7 ] = new ItemInstance( 8, 6, 10 );
		haveItemSet[ 8 ] = new ItemInstance( 9, 6, 10 );
		haveItemSet[ 9 ] = new ItemInstance( 10, 6, 10 );
	}

	// check selling item
	public FurnitureObject CheckSellItem()
	{
		foreach( FurnitureObject element in allocateFurnitureObjectSet )
		{
			if( (element.SellItem != null) )
			{
				return element;
			}
		}
		return null;
	}
}