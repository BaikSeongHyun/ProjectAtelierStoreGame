using UnityEngine;
using System.Collections;

public class PlayerAgent : AIAgent
{
	// unity stand method
	// awake
	void Awake()
	{
		DataInitialize();
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfo;

		if( Input.GetButtonDown( "LeftClick" ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				moveAgent.SetDestination( hitInfo.point );
			}
		}
	}
}
