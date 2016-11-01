using UnityEngine;
using System.Collections;

[System.Serializable]
public class StoreData
{
	// logic data field
	[SerializeField] int uid;

	// game data field
	[SerializeField] int storeStep;

	// property
	public int StoreStep { get { return storeStep; } set { storeStep = value; } }

}
