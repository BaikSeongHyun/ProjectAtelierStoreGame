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
	[SerializeField] int money;
	[SerializeField] StoreData haveStoreData;
	[SerializeField] ItemData[] haveItemSet;
	[SerializeField] FurnitureData[] haveFurnitureSet;

	// property
	public string Name { get { return name; } }

	public int Money { get { return money; } }

	public StoreData StoreData { get { return haveStoreData; } set { haveStoreData = value; } }

	public ItemData[] ItemSet { get { return haveItemSet; } set { haveItemSet = value; } }

	public FurnitureData[] FurnitureSet { get { return haveFurnitureSet; } set { haveFurnitureSet = value; } }


	// consturctor - no parameter
	public PlayerData()
	{

	}
}