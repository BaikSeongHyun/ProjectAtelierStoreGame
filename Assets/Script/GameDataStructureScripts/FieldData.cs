using UnityEngine;
using System.Collections;

[System.Serializable]
public class FieldData
{
	// field data
	[SerializeField] int id;
	[SerializeField] float waitingTime;
	[SerializeField] int horizontalLength;
	[SerializeField] int verticalLength;
	[SerializeField] int resetCost;
	[SerializeField] int checkNumber;

	// property
	public int CheckNumber { get { return checkNumber; } }
	public float WaitingTime { get { return waitingTime; } }

	// constructor - no parameter
	public FieldData()
	{
		id = 0;
		waitingTime = 0;
		horizontalLength = 0;
		verticalLength = 0;
		resetCost = 0;
		checkNumber = 0;
	}

	// constructor - all parameter
	public FieldData( int _id, float _waitingTime, int _horizontalLength, int _verticalLength, int _resetCost, int _checkNumber )
	{
		id = _id;
		waitingTime = _waitingTime;
		horizontalLength = _horizontalLength;
		verticalLength = _verticalLength;
		resetCost = _resetCost;
		checkNumber = _checkNumber;
	}

}
