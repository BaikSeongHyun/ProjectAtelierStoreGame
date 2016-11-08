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

	// constructor - default
	public StoreData()
	{
		storeStep = 1;
	}

	// constructor - step number
	public StoreData( int _step )
	{
		storeStep = _step;
	}

}
