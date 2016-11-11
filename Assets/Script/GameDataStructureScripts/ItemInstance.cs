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
	public ItemData Item { get { return itemData; } }

	public int Count { get { return count; } set { count = Mathf.Clamp( value, 0, itemData.CountLimit ); } }

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
		itemData = DataManager.FindItemDataByID( data.Item.ID );
		count = Mathf.Clamp( data.count, 0, limit );
		data.count -= count;
	}

	public ItemInstance( ItemData data, int _slotNumber, int _count )
	{
		itemData = data;
		slotNumber = _slotNumber;
		count = _count;
	}

	public ItemInstance( int id, int _count, int _sellPrice )
	{
		itemData = DataManager.FindItemDataByID( id );
		count = _count;
		sellPrice = _sellPrice;
	}
}