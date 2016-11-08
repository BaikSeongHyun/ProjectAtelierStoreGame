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

	// property
	public string Name { get { return name; } set { name = value; } }

	public int Level { get { return level; } set { level = value; } }

	public int Fame { get { return fame; } set { fame = value; } }

	public int Charm { get { return charm; } set { charm = value; } }

	public int Gold { get { return gold; } set { gold = value; } }

	public int Gem { get { return gem; } set { gem = value; } }

	public StoreData StoreData { get { return haveStoreData; } set { haveStoreData = value; } }

	public ItemInstance[] ItemSet { get { return haveItemSet; } set { haveItemSet = value; } }

	public FurnitureInstance[] FurnitureSet { get { return haveFurnitureSet; } set { haveFurnitureSet = value; } }

	public List<FurnitureInstance> AllocateFurnitureSet { get { return allocateFurnitureSet; } }

	// consturctor - no parameter
	public PlayerData()
	{
		level = 1;
		name = "";
		allocateFurnitureSet = new List<FurnitureInstance>( );
	}

	// public method
	// add furniture data -> use only data
	public bool AddFurnitureData( FurnitureData data )
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null )
			{
				haveFurnitureSet[ i ] = new FurnitureInstance( data, i );
				return true;
			}
		}

		Debug.Log( "Furniture inventory is full" );
		return false;
	}

	// add furniture data -> move instance data
	public bool AddFurnitureData( FurnitureInstance data )
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null )
			{
				haveFurnitureSet[ i ] = new FurnitureInstance( data, i );
				haveFurnitureSet[ i ].IsAllocated = false;
				return true;
			}
		}

		Debug.Log( "Furniture inventory is full" );
		return false;
	}

	// allocate furniture instance
	public bool AllocateFurnitureInstance( int index )
	{	
		try
		{	
			allocateFurnitureSet.Add( new FurnitureInstance( haveFurnitureSet[ index ] ) );
			allocateFurnitureSet[ allocateFurnitureSet.Count - 1 ].AllocateInstance( allocateFurnitureSet.Count );
			haveFurnitureSet[ index ] = null;
			return true;
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
		try
		{
			allocateFurnitureSet.RemoveAt( index );
			return true;
		}
		catch( Exception e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			return false;
		}
	}

	// add item data
	public bool AddItemData( ItemData data, int itemCount )
	{
		for( int i = 0; i < haveItemSet.Length; i++ )
		{
			//Debug.Log(haveItemSet.Length);
			if( ( haveItemSet[ i ] != null ) && ( haveItemSet[ i ].Count == 0 ) )
			{
				//Debug.Log("빈 공간에 새로 생성");
				haveItemSet[ i ] = new ItemInstance( data, i, itemCount );
				break;
			}
			else if( haveItemSet[ i ].Item.ID == data.ID )
			{
				if( haveItemSet[ i ].Count + itemCount <= haveItemSet[ i ].Item.CountLimit )
				{
					//Debug.Log("이미 있는 공간에 추가");
					haveItemSet[ i ].Count += itemCount;
					break;
				}
				else
				{
					haveItemSet[ i ].Count = haveItemSet[ i ].Item.CountLimit;
					if( i + 1 == haveItemSet.Length )
					{
						//Debug.Log("full! 남은거 버려진다.");
						itemCount = 0;
						break;
					}
					else
					{
						//Debug.Log("최대수량초과" + i);
						itemCount = haveItemSet[ i ].Count + itemCount - haveItemSet[ i ].Item.CountLimit;
						continue;
					}
				}
			}
		}

		return false;
	}

	// material -> add item
	public void AddItemMaterial( int uid, int itemCount )
	{
		ItemData itemData = new ItemData( DataManager.FindItemDataByID( uid ) );

		AddItemData( itemData, itemCount );
	}

	// set default status
	public void SetDefaultStatus()
	{
		haveStoreData = new StoreData( );
		haveItemSet = new ItemInstance[16];
		haveFurnitureSet = new FurnitureInstance[16];
		allocateFurnitureSet = new List<FurnitureInstance>( );
	}
}