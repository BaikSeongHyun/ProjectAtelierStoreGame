using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// game player instance data
[System.Serializable]
public class PlayerData
{
	// logic data field
	[SerializeField] int UID;
	[SerializeField] string gameID;

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

	// property
	public string Name { get { return name; } }

	public int Level { get { return level; } }

	public int Fame { get { return fame; } }

	public int Charm { get { return charm; } }

	public int Gold { get { return gold; } }

	public int Gem { get { return gem; } }

	public StoreData StoreData { get { return haveStoreData; } set { haveStoreData = value; } }

	public ItemInstance[] ItemSet { get { return haveItemSet; } set { haveItemSet = value; } }

	public FurnitureInstance[] FurnitureSet { get { return haveFurnitureSet; } set { haveFurnitureSet = value; } }


	// consturctor - no parameter
	public PlayerData()
	{

	}

	// public method
	// add furniture data
	public bool AddFurnitureData( FurnitureData data )
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null )
			{
				haveFurnitureSet[ i ] = new FurnitureInstance( data );
				return true;
			}
		}

		Debug.Log( "Furniture inventory is full" );
		return false;
	}

	// add item data
	public bool AddItemData( ItemData data, int itemCount )
	{
		for( int i = 0; i < haveItemSet.Length; i++ )
		{
			// find item
			if( haveItemSet[ i ].Item.UID == data.UID )
			{
				// if find item add -> count up
				if( haveItemSet[ i ].Count + itemCount <= haveItemSet[ i ].Item.CountLimit )
				{
					haveItemSet[ i ].Count += itemCount;
					return true;
				}
			}

			// no item
			if( haveItemSet[ i ] != null )
			{
				// make item and input count
				haveItemSet[ i ] = new ItemInstance( data, i, itemCount );
			}
		}

		Debug.Log( "Item inventory is full" );
		return false;
	}
}