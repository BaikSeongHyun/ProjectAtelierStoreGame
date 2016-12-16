using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemInstance
{
	// field
	[SerializeField] int uid;
	[SerializeField] ItemData itemData;
	[SerializeField] int sellPrice;
	[SerializeField] int slotNumber;
	[SerializeField] int count;


	// property
	public ItemData Item { get { return itemData; } set { itemData = value; } }

	public int Count { get { return count; } set { count = Mathf.Clamp( value, 0, itemData.CountLimit ); } }

	public int ResourceCount { get { return count; } set { count = value; } }

	public int SellPrice { get { return sellPrice; } set { sellPrice = value; } }

	public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } }

	// constructor
	public ItemInstance()
	{
		itemData = null;
		slotNumber = 0;
		count = 0;
	}

	public ItemInstance( ItemInstance data, int limit )
	{
		itemData = new ItemData( DataManager.FindItemDataByID( data.Item.ID ) );
		count = Mathf.Clamp( data.count, 0, limit );
		data.count -= count;
	}

	public ItemInstance( ItemData data, int _slotNumber, int _count )
	{
		itemData = data;
		slotNumber = _slotNumber;
		count = _count;
	}

	public ItemInstance( int id, int _slotNumber, int _count )
	{		
		itemData = new ItemData( DataManager.FindItemDataByID( id ) );
		slotNumber = _slotNumber;
		count = _count;
	}

	// public method
	// item divide -> use register sell slot
	public void RegisterSellItems( int _count )
	{
		count -= _count;

		if( count <= 0 )
		{
			itemData = null;
			slotNumber = 0;
			count = 0;
		}
	}
}