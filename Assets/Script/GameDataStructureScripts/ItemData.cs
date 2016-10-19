using UnityEngine;
using System.Collections;

public class ItemData
{
	// field
	[SerializeField] int uid;
	[SerializeField] string name;
	[SerializeField] Type itemType;
	[SerializeField] int count;
	[SerializeField] string guide;
	[SerializeField] int[] resource;
	[SerializeField] int[] resourceCount;

	// enum data
	public enum Type : int
	{
		potion = 1
	};

}