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
	[SerializeField] ItemData[] haveItemSet;
	[SerializeField] FurnitureData[] haveFurnitureSet;

	// property
	public string Name { get { return name; } }

	public int Level { get { return level; } }

	public int Fame { get { return fame; } }

	public int Charm { get { return charm; } }

	public int Gold { get { return gold; } }

	public int Gem { get { return gem; } }

	public StoreData StoreData { get { return haveStoreData; } set { haveStoreData = value; } }

	public ItemData[] ItemSet { get { return haveItemSet; } set { haveItemSet = value; } }

	public FurnitureData[] FurnitureSet { get { return haveFurnitureSet; } set { haveFurnitureSet = value; } }


	// consturctor - no parameter
	public PlayerData()
	{

	}

	// public method
	// add furniture data
	public bool AddFurnitureData(FurnitureData data)
	{
		for( int i = 0; i < haveFurnitureSet.Length; i++ )
		{
			if( haveFurnitureSet[ i ] == null )
			{
				haveFurnitureSet[ i ] = new FurnitureData( data );
				return true;
			}
		}

		return false;
	}
}