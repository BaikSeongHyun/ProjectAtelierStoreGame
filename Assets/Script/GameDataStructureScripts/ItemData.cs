using UnityEngine;
using System.Collections;

public class ItemData
{
	// field
	[SerializeField] int uid;
	[SerializeField] string name;
	[SerializeField] Type itemType;
	[SerializeField] string guide;
	[SerializeField] int countLimit;
	[SerializeField] int[] resource;
	[SerializeField] int[] resourceCount;

	// property
	public int UID { get { return UID; } }

	public int CountLimit { get { return countLimit; } }

	// enum data
	public enum Type : int
	{
		potion = 1}

	;

	// constructor - default
	public ItemData()
	{

	}

	// constructor - all parameter
	// use xml data format
	public ItemData( int _uid, string _name, int _itemType, string _guide, int _countLimit )
	{
		uid = _uid;
		name = _name;
		guide = _guide;
		countLimit = _countLimit;

		// set type -> use id
	}

	// constructor - self parameter
	public ItemData( ItemData data )
	{
		uid = data.uid;
		name = data.name;
		itemType = data.itemType;
		guide = data.guide;
		countLimit = data.countLimit;
		resource = data.resource;
		resourceCount = data.resourceCount;
	}
}