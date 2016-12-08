using UnityEngine;
using System.Collections;

[System.Serializable]
public class FieldData
{
	// field data
	[SerializeField] int step;
	[SerializeField] float createTime;
	[SerializeField] int acquireExperience;
	[SerializeField] int objectMaxCount;


	// property
	public int Step { get { return step; } }

	public float CreateTime { get { return createTime; } }

	public int AcquireExperience { get { return acquireExperience; } }

	public int ObjectMaxCount { get { return objectMaxCount; } }


	// constructor - no parameter
	public FieldData()
	{
		step = 0;
		createTime = Mathf.Infinity;
		acquireExperience = 0;
		objectMaxCount = 0;
	}

	// constructor - all parameter
	public FieldData( int _step, int _createTime, int _acquireExperience, int _objectMaxCount )
	{
		step = _step;
		createTime = _createTime;
		acquireExperience = _acquireExperience;
		objectMaxCount = _objectMaxCount;
	}

}
