using UnityEngine;
using UnityEditor;
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
	[SerializeField] GameObject storeWall;
	[SerializeField] GameObject storeBackground;
	[SerializeField] FurnitureObject[] furnitureObjectSet;

	// customzing data field
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] FurnitureObject presentAllocateObject;

	// property
	public bool CreateComplete { get { return createComplete; } }

	public TileMap StoreField { get { return storeField; } }

	public FurnitureObject PresentAllocateObject { get { return presentAllocateObject; } }

	// unity method
	void Awake()
	{
		manager = GetComponent<GameManager>();
	}

	// public method
	// create store object -> if data load complete
	public bool CreateStoreObject()
	{
		// create object
		try
		{
			// create tilemap object
			GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreField" ), Vector3.zero, Quaternion.identity );
			storeField = temp.GetComponent<TileMap>();
			storeField.SetSize( manager.GamePlayer.StoreData.StoreStep );
			storeField.BuildMesh();

			// create wall & backgroubd
			switch( manager.GamePlayer.StoreData.StoreStep )
			{
				case 1:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stWall" ), new Vector3( 5f, 0f, 5f ), Quaternion.identity );
					storeBackground = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreBackground" ), new Vector3( 0f, -0.01f, 0f ), Quaternion.identity );
					break;
				case 2:
					
					break;
				case 3:
					
					break;
			}

			GameObjectUtility.SetStaticEditorFlags( storeField.gameObject, StaticEditorFlags.NavigationStatic );
	
			NavMeshBuilder.BuildNavMesh();

			// create funiture object
			// set data array
			furnitureObjectSet = new FurnitureObject[manager.GamePlayer.FurnitureSet.Length];

			// make object
			for( int i = 0; i < manager.GamePlayer.FurnitureSet.Length; i++ )
			{
				if( manager.GamePlayer.FurnitureSet[ i ].IsAllocated )
				{
					temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/" + manager.GamePlayer.FurnitureSet[ i ].Furniture.UID.ToString() ), 
					                                   manager.GamePlayer.FurnitureSet[ i ].Position, 
					                                   manager.GamePlayer.FurnitureSet[ i ].Rotation );
					furnitureObjectSet[ i ] = temp.GetComponent<FurnitureObject>();
					furnitureObjectSet[ i ].InstanceData = manager.GamePlayer.FurnitureSet[ i ];
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

	// destroy all store object
	public void ClearStoreObject()
	{
		// destroy object
		// field
		Destroy( storeField.gameObject );

		// wall & back ground
		Destroy( storeWall );
		Destroy( storeBackground );

		// furniture object
		foreach( FurnitureObject element in furnitureObjectSet )
		{
			if( element != null )
				Destroy( element.gameObject );
		}

		// create set false
		createComplete = false;
	}

	// recreate store object -> data clear & create
	public void RecreateStoreObject()
	{
		ClearStoreObject();
		CreateStoreObject();
	}

	// customzing store object
	public void CustomzingFurnutureObject()
	{
		// reload ray
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// set plane scale
		float planeScale = 0.0f;

		switch( manager.GamePlayer.StoreData.StoreStep )
		{
			case 1:
				planeScale = 10f;
				break;
			case 2:
				planeScale = 15f;
				break;
			case 3:
				planeScale = 20f;
				break;
		}

		// clear furniture object -> when mouse button right click
		if( Input.GetButtonDown( "RightClick" ) && ( presentAllocateObject != null ) )
		{
			ConfirmAllocateFurnitureObject();
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
		else if( presentAllocateObject != null && presentAllocateObject.InstanceData.Furniture.Allocate == FurnitureData.AllocateType.Field )
		{
			// make cast point ( field furniture & store Field )
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point, planeScale );

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
		else if( presentAllocateObject != null && presentAllocateObject.InstanceData.Furniture.Allocate == FurnitureData.AllocateType.Wall )
		{
			// set layer -> StoreWallLeft & StoreWallRight
			int layer = 1 << LayerMask.NameToLayer( "StoreWallLeft" );
			layer |= 1 << LayerMask.NameToLayer( "StoreWallRight" );

			// make cast point ( wall furniture & store wall)
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, layer ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point, planeScale );

				// set rotation by direction
				if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallLeft" ) )
					presentAllocateObject.ChangeObjectRotation( "WallLeft" );
				else if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer( "StoreWallRight" ) )
					presentAllocateObject.ChangeObjectRotation( "WallRight" );
			}
		}
	}

	// confirm allocate object
	public void ConfirmAllocateFurnitureObject()
	{
		// allocate possible
		if( presentAllocateObject.AllocatePossible )
		{
			// set allocate mode false
			presentAllocateObject.AllocateMode = false;

			// clear present object 
			presentAllocateObject = null;
		}
	}
}
