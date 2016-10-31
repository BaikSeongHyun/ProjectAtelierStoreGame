using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemInstance
{
	// field
	[SerializeField] ItemData itemData;
	[SerializeField] int slotNumber;
	[SerializeField] int count;


	// property
	public ItemData Item { get { return itemData; } }

	public int Count { get { return count; } set { count = Mathf.Clamp( value, 1, itemData.CountLimit ); } }

	public int SlotNumber { get { return slotNumber; } set { slotNumber = value; } }

	// constructor

	public ItemInstance( ItemData data, int _slotNumber, int _count )
	{
		itemData = new ItemData( data );
		slotNumber = _slotNumber;
		count = _count;
	}

	public ItemInstance( int uid, int _slotNumber, int _count )
	{

		itemData = new ItemData( DataManager.FindItemDataByUID( uid ) );
		slotNumber = _slotNumber;
		count = _count;
	}
}