using UnityEngine;
using System.Collections;

public class CustomizingLogic : MonoBehaviour
{
	// field
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] Player player;
	[SerializeField] FurnitureObject presentAllocateObject;

	// unity method
	// awake
	void Awake()
	{
		presentAllocateObject = null;
	}

	//update
	void Update()
	{
		// reload ray
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// player character move store field
		if( presentAllocateObject == null && Input.GetButtonDown( "LeftClick" ) )
		{
			// make cast point
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				// & move player character
				player.MovePosition( hitInfo.point );
			}
		}
		// clear furniture object -> when mouse button right click
		else if( Input.GetButtonDown( "RightClick" ) && ( presentAllocateObject != null ) )
		{
			// allocate possible
			if( presentAllocateObject.AllocatePossible )
			{
				// clear present object 
				presentAllocateObject = null;
			}
		}
		// set up furniture object => when mouse button right click
		else if( Input.GetButtonDown( "RightClick" ) && ( presentAllocateObject == null ) )
		{
			// cast & check furniture object -> if exist -> set present object
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				// object allocate mode -> move mode
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentAllocateObject = tempSearch.GetComponent<FurnitureObject>();
				presentAllocateObject.AllocateMode = true;
			}
		}
		// present object is fleid move object -> cast store field 
		else if( presentAllocateObject != null && presentAllocateObject.ObjectData.Allocate == FurnitureData.AllocateType.Field )
		{
			// make cast point ( field furniture & store Field )
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point, hitInfo.collider.gameObject );

				// rotation change
				if( Input.GetKeyDown( KeyCode.E ) )
					presentAllocateObject.ChangeObjectRotation( "Left" );
				else if( Input.GetKeyDown( KeyCode.T ) )
					presentAllocateObject.ChangeObjectRotation( "Right" );
				else if( Input.GetKeyDown( KeyCode.R ) )
					presentAllocateObject.ChangeObjectRotation( "Reset" );
			}
		}
		// present object is wall move object -> cast store wall 
		else if( presentAllocateObject != null && presentAllocateObject.ObjectData.Allocate == FurnitureData.AllocateType.Wall )
		{
			// set layer -> StoreWallLeft & StoreWallRight
			int layer = 1 << LayerMask.NameToLayer( "StoreWallLeft" );
			layer |= 1 << LayerMask.NameToLayer( "StoreWallRight" );

			// make cast point ( wall furniture & store wall)
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, layer ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point, hitInfo.collider.gameObject );

				// set rotation by direction
				if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallLeft" ) )
					presentAllocateObject.ChangeObjectRotation( "WallLeft" );
				else if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallRight" ) )
					presentAllocateObject.ChangeObjectRotation( "WallRight" );
			}
		}
	}
}
