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
		allocateFurnitureSet = new List<FurnitureInstance>( );
		allocateFurnitureObjectSet = new List<FurnitureObject>( );
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

		int processIndex = index + ( presentStepIndex * ( haveFurnitureSet.Length / 3 ) );
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
		charType = "Berry";
		level = 1;
		fame = 0;
		charm = 0;
		gold = 1000;
		gem = 1000;
		haveStoreData = new StoreData( DataManager.FindStoreDataByStep( 1 ), 0 );
		haveItemSet = new ItemInstance[60];
		haveFurnitureSet = new FurnitureInstance[30];
		allocateFurnitureSet = new List<FurnitureInstance>( );

		allocateFurnitureSet.Add( new FurnitureInstance( 3, 0, true, new Vector3( 9f, 0f, 3f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 6, 0, true, new Vector3( 9f, 0f, 7f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 9, 0, true, new Vector3( 6f, 0f, 7f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 16, 0, true, new Vector3( 9f, 0f, 0.5f ), Quaternion.identity ) );
		allocateFurnitureSet.Add( new FurnitureInstance( 24, 0, true, new Vector3( 6.5f, 0f, 3f ), Quaternion.identity ) );

		haveItemSet[ 0 ] = new ItemInstance( 16, 0, 7 );
		haveItemSet[ 1 ] = new ItemInstance( 17, 1, 6 );
		haveItemSet[ 2 ] = new ItemInstance( 10, 2, 3 );
		haveItemSet[ 3 ] = new ItemInstance( 11, 3, 3 );

	}

	// check selling item
	public FurnitureObject CheckSellItem()
	{
		foreach( FurnitureObject element in allocateFurnitureObjectSet )
		{
			if( ( element.SellItem != null ) )
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
				
			}
		}
	}

	public void InsertSellItemUseIndex( int index )
	{
		
	}

	public int SearchItem( ItemData data, int count )
	{
		//인벤토리 번호 반환합니다.
		for( int i = 0; i < ( haveItemSet.Length / 3 ); i++ )
		{
			if( haveItemSet[ i ] == null || haveItemSet[ i ].Item == null || haveItemSet[ i ].Item.ID == 0 )
			{
				Debug.Log( "pass" );
				continue;
			}
			else
			{

				if( haveItemSet[ i ].Item.ID == data.ID && haveItemSet[ i ].Count >= count )
				{

					Debug.Log( "갯수충분" );
					return i; //인벤토리 0~15번, 번호를 반환
				}
			}
		}

		return -1;
	}
}