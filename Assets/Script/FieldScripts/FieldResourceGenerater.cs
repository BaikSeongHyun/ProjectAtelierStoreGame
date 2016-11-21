using UnityEngine;
using System.Collections;

public class FieldResourceGenerater : MonoBehaviour
{
	[SerializeField] float regenerateCycle;
	[SerializeField] float positionX;
	[SerializeField] float positionZ;
	[SerializeField] float timeChecker;
	[SerializeField] bool isGenerate;

	//property
	public float PositionX { get { return positionX; } set { positionX = Mathf.Clamp( value, -35f, 35f ); } }
	public float PositionZ { get { return positionZ; } set { positionZ = Mathf.Clamp( value, -35f, 35f ); } }

	void Awake()
	{
		DataInitalize();
	}

	void FixedUpdate()
	{

	}

	//public method
	public void DataInitalize()
	{
		timeChecker = 0.0f;
		isGenerate = false;
	}

	// generate Resource Item
	public void GenerateResourceItem()
	{

	}
}
