using UnityEngine;
using System;
using System.Collections;

public class StoreManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// game logic simple data field
	[SerializeField] bool createComplete;

	// game instance data field
	[SerializeField] TileMap storeField;
	[SerializeField] FurnitureObject[] furnitureObjectSet;

	// customzing data field
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] FurnitureObject presentAllocateObject;

	// property
	public bool CreateComplete { get { return createComplete; } }

	// unity method
	void Awake()
	{
		manager = GetComponent<GameManager>();
	}

	//public method
	//create store object -> if data load complete
	public bool CreateStoreObject()
	{
		try
		{
			// create tilemap object
			GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/TileMap" ), Vector3.zero, new Quaternion( 0f, 0f, 0f, 0f ) );
			storeField = temp.GetComponent<TileMap>();
			storeField.SetSize( manager.GamePlayer.StoreData.StoreStep );
			storeField.BuildMesh();

			// create funiture object
			// set data array
			furnitureObjectSet = new FurnitureObject[manager.GamePlayer.FurnitureSet.Length];

			// make object
			for( int i = 0; i < manager.GamePlayer.FurnitureSet.Length; i++ )
			{
				if( manager.GamePlayer.FurnitureSet[ i ].IsAllocated )
				{
					temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/" + manager.GamePlayer.FurnitureSet[ i ].UID.ToString() ), 
					                                   manager.GamePlayer.FurnitureSet[ i ].Position, 
					                                   manager.GamePlayer.FurnitureSet[ i ].Rotation );
					furnitureObjectSet[ i ] = temp.GetComponent<FurnitureObject>();
					furnitureObjectSet[ i ].Furniture = manager.GamePlayer.FurnitureSet[ i ];
				}
			}
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			Debug.Log( "Store Create Fail -> data load fail" );
			createComplete = false;
			return false;
		}

		createComplete = true;
		return true;
	}

	// customzing store object
	public void CustomzingFurnutureObject()
	{
		// reload ray
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// clear furniture object -> when mouse button right click
		if( Input.GetButtonDown( "RightClick" ) && ( presentAllocateObject != null ) )
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
		else if( presentAllocateObject != null && presentAllocateObject.Furniture.Allocate == FurnitureData.AllocateType.Field )
		{
			// make cast point ( field furniture & store Field )
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point );

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
		else if( presentAllocateObject != null && presentAllocateObject.Furniture.Allocate == FurnitureData.AllocateType.Wall )
		{
			// set layer -> StoreWallLeft & StoreWallRight
			int layer = 1 << LayerMask.NameToLayer( "StoreWallLeft" );
			layer |= 1 << LayerMask.NameToLayer( "StoreWallRight" );

			// make cast point ( wall furniture & store wall)
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, layer ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point );

				// set rotation by direction
				if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallLeft" ) )
					presentAllocateObject.ChangeObjectRotation( "WallLeft" );
				else if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallRight" ) )
					presentAllocateObject.ChangeObjectRotation( "WallRight" );
			}
		}
	}
}
