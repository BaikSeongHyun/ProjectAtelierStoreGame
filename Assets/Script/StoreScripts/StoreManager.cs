using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] UIManager mainUI;

	// game logic simple data field
	[SerializeField] bool createComplete;

	// game instance data field
	[SerializeField] float planeScale;
	[SerializeField] TileMap storeField;
	[SerializeField] GameObject storeWall;
	[SerializeField] GameObject storeBackground;
	[SerializeField] GameObject storeNavField;
	[SerializeField] List<FurnitureObject> furnitureObjectSet;

	// customzing data field
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] FurnitureObject presentAllocateObject;

	// property
	public bool CreateComplete { get { return createComplete; } }

	public float PlaneScale { get { return planeScale; } }

	public TileMap StoreField { get { return storeField; } }

	public FurnitureObject PresentAllocateObject { get { return presentAllocateObject; } }

	// unity method
	void Awake()
	{
		manager = GetComponent<GameManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
	}

	// public method
	// create store object -> if data load complete
	public void SetPlaneScale()
	{
		// set plane scale
		planeScale = 0.0f;

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
	}

	public bool CreateStoreObject()
	{
		SetPlaneScale();

		// create object
		try
		{
			// create tilemap object - size check & texture setting
			storeField.SetSize( manager.GamePlayer.StoreData.StoreStep );
			storeField.BuildMesh();

			// create store wall & background move & nav field
			switch( manager.GamePlayer.StoreData.StoreStep )
			{
				case 1:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stWall" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeNavField = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/NavMeshObstacle/Step1NavField" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeBackground.transform.position = new Vector3( 0f, -0.01f, 0f );
					break;
				case 2:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stWall" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeNavField = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/NavMeshObstacle/Step2NavField" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeBackground.transform.position = new Vector3( 5f, -0.01f, 5f );
					break;
				case 3:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stWall" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeNavField = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/NavMeshObstacle/Step3NavField" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeBackground.transform.position = new Vector3( 10f, -0.01f, 10f );
					break;
			}

			// create funiture object
			// set data array
			furnitureObjectSet = new List<FurnitureObject>( );
			GameObject temp;

			// make object - allocated furniture
			if( manager.GamePlayer.AllocateFurnitureSet.Count != 0 )
			{
				for( int i = 0; i < manager.GamePlayer.AllocateFurnitureSet.Count; i++ )
				{
					if( manager.GamePlayer.AllocateFurnitureSet[ i ].IsAllocated )
					{
						temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/" + manager.GamePlayer.AllocateFurnitureSet[ i ].Furniture.File ), 
						                                   manager.GamePlayer.AllocateFurnitureSet[ i ].Position, 
						                                   manager.GamePlayer.AllocateFurnitureSet[ i ].Rotation );
						furnitureObjectSet.Add( temp.GetComponent<FurnitureObject>() );
						furnitureObjectSet[ i ].InstanceData = manager.GamePlayer.AllocateFurnitureSet[ i ];
					}
				}

				manager.GamePlayer.AllocateFurnitureObjectSet = furnitureObjectSet;
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

		// wall & nav field
		Destroy( storeWall );
		Destroy( storeNavField );


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

	// store policy
	// create item
	public void StorePolicy()
	{
		// ray cast set furniture target
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			
		// target function type -> create : use create ui(object type)
		// target function type -> storage : use storage ui(object type)
		if( Input.GetButtonDown( "RightClick" ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentAllocateObject = tempSearch.GetComponent<FurnitureObject>();
				
				if( presentAllocateObject.InstanceData.Furniture.Function == FurnitureData.FunctionType.CreateObject )
				{
					Debug.Log( "Open create ui" );
				}
				else if( presentAllocateObject.InstanceData.Furniture.Function == FurnitureData.FunctionType.StorageObject )
				{
					Debug.Log( "Open Storage ui" );
				}
			}
		}
		

	}

	// customzing store object
	public void CustomzingFurnutureObject()
	{
		// reload ray
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

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
		}/*
		// present object is fleid move object -> cast store field 
		else if( presentAllocateObject != null && presentAllocateObject.InstanceData.Furniture.Allocate == FurnitureData.AllocateType.Field )
		{
			// make cast point ( field furniture & store Field )
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				// position change
				presentAllocateObject.ChangeObjectPosition( hitInfo.point, planeScale );			
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
		*/
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

	// create allocate object
	public void CreateAllocateFurnitureObject( int index )
	{		
		manager.GamePlayer.AllocateFurnitureInstance( index );

		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/" + manager.GamePlayer.AllocateFurnitureSet[ manager.GamePlayer.AllocateFurnitureSet.Count ].Furniture.ID.ToString() ), 
		                                              new Vector3( planeScale / 2f, 0f, planeScale / 2f ), 
		                                              Quaternion.identity );
	}

	// allocate furniture object position set
	public void AllocateFurnintureObjectPositionSet( int direction )
	{
		Vector3 moveDirection = Vector3.zero;
		switch( direction )
		{
			case 1:
				moveDirection = new Vector3( 0f, 0f, -0.5f );
				break;
			case 2:
				moveDirection = new Vector3( 0f, 0f, 0.5f );
				break;
			case 3:
				moveDirection = new Vector3( 0.5f, 0f, 0f );
				break;
			case 4:
				moveDirection = new Vector3( -0.5f, 0f, 0f );
				break;
		}
		presentAllocateObject.ChangeObjectPosition( presentAllocateObject.transform.position + moveDirection );
	}

	// allocate furniture object rotation set
	public void AllocateFurnitureObjectRotationSet( int direction )
	{
		// rotation change
		switch( direction )
		{
			case 1:
				presentAllocateObject.ChangeObjectRotation( "Left" );
				break;
			case 2:
				presentAllocateObject.ChangeObjectRotation( "Right" );
				break;
			case 3:
				presentAllocateObject.ChangeObjectRotation( "Reset" );
				break;
		}
	}

}
