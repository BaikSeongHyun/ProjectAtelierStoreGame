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
	[SerializeField] int presentExperience;
	[SerializeField] int requireExperience;
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
		allocateFurnitureSet = new List<FurnitureInstance>( );
		allocateFurnitureObjectSet = new List<FurnitureObject>( );
	}

	// public method
	// add furniture data -> use only data
	public bool AddFurnitureData( FurnitureData data )
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null || haveFurnitureSet[ i ].Furniture.ID == 0 )
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
		int i = DataManager.FindFurnitureDataByID( id ).Step * 10;
		for( i = 0; i < ( int ) haveFurnitureSet.Length / 3; i++ )
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

		int processIndex = index + ( presentStepIndex * 10 );
		try
		{
			if( haveFurnitureSet[ processIndex ] == null || haveFurnitureSet[ processIndex ].Furniture == null || haveFurnitureSet[ processIndex ].Furniture.ID == 0 )
				return false;
			else
			{
				allocateFurnitureSet.Add( new FurnitureInstance( haveFurnitureSet[ processIndex ] ) );
				allocateFurnitureSet[ allocateFurnitureSet.Count - 1 ].AllocateInstance( allocateFurnitureSet.Count - 1 );
				haveFurnitureSet[ processIndex ] = new FurnitureInstance( );
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
		// remove item
		allocateFurnitureObjectSet[ index ].ObjectAllocateOff( this );
		allocateFurnitureObjectSet.RemoveAt( index );
		allocateFurnitureSet.RemoveAt( index );

		for( int i = 0; i < allocateFurnitureSet.Count; i++ )
		{
			allocateFurnitureSet[ i ].SlotNumber = i;
		}
		return true;		
	}

	// add item data
	public bool AddItemData( int id, int itemCount )
	{
		// if item is exist
		foreach( ItemInstance element in haveItemSet )
		{
			if( element.Item != null && element.Item.ID == id )
			{
				element.Count += itemCount;
				return true;
			}
		}

		// find empty space
		for( int i = 0; i < haveItemSet.Length / 3; i++ )
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
		name = "Default";
		charType = "Default";
		level = 1;
		fame = 0;
		charm = 0;
		gold = 1000;
		gem = 1000;
		haveStoreData = new StoreData( );
		haveItemSet = new ItemInstance[45];
		haveFurnitureSet = new FurnitureInstance[30];
		allocateFurnitureSet = new List<FurnitureInstance>( );

		allocateFurnitureSet.Add( new FurnitureInstance( 2, 0, true, new Vector3( 4f, 0f, 4f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 4, 1, true, new Vector3( 2f, 0f, 2f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 7, 1, true, new Vector3( 8f, 0f, 8f ), Quaternion.identity ) );
	}

	// check selling item
	public FurnitureObject CheckSellItem()
	{
		foreach( FurnitureObject element in allocateFurnitureObjectSet )
		{
			if( ( element.SellItem != null ) && ( element.SellItem.Item.ID != 0 ) )
			{
				return element;
			}
		}
		return null;
	}

	// for test
	public void InsertSellItem()
	{
		foreach( FurnitureObject element in allocateFurnitureObjectSet )
		{
			if( element.InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
			{
				element.SellItem = new ItemInstance( 7, 5, 100 );
			}
		}
	}

	// for test
	public void DeleteSellItem()
	{
		foreach( FurnitureObject element in allocateFurnitureObjectSet )
		{
			if( element.InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
			{
				element.SellItem = null;
			}
		}
	}

	public void InsertSellItemUseIndex( int index )
	{
		allocateFurnitureObjectSet[ index ].SellItem = new ItemInstance( 7, 5, 100 ); 
	}
}