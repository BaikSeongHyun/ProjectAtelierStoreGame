using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] CharacterManager characterManager;
	[SerializeField] UIManager mainUI;

	// game logic simple data field
	[SerializeField] bool createComplete;

	// game instance data field
	[SerializeField] float planeScale;
	[SerializeField] GameObject storeField;
	[SerializeField] Material storeFieldTexture;
	[SerializeField] GameObject reticleLine;
	[SerializeField] Image reticleLineTexture;
	[SerializeField] bool isCustomzing;
	[SerializeField] GameObject storeWall;
	[SerializeField] GameObject storeBackground;
	[SerializeField] GameObject storeNavField;
	[SerializeField] List<FurnitureObject> furnitureObjectSet;
	[SerializeField] int presentViewStorageIndex;

	// customize & store policy data field
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] FurnitureObject presentSelectedObject;

	// create data field
	[SerializeField] List<ItemData> viewItemGroup;
	[SerializeField] ItemData selectedItem;
	[SerializeField] List<ItemInstance> resourceItem;
	[SerializeField] int createCount;
	[SerializeField] int createLimitCount;
	[SerializeField] int presentIndex;
	[SerializeField] bool itemCreate;

	// property
	public bool CreateComplete { get { return createComplete; } }

	public float PlaneScale { get { return planeScale; } }

	public int StorageIndex { get { return presentViewStorageIndex; } set { presentViewStorageIndex = value; } }

	public GameObject StoreField { get { return storeField; } }

	public bool IsCustomizing { get { return isCustomzing; } set { reticleLine.SetActive( isCustomzing = value ); } }

	public FurnitureObject PresentSelectedObject { get { return presentSelectedObject; } }

	public int PresentCreateListIndex { get { return presentIndex; } set { presentIndex = value; } }

	public List<ItemData> ViewItemGroup { get { return viewItemGroup; } }

	public ItemData SelectedItem { get { return selectedItem; } }

	public List<ItemInstance> ResourceItem { get { return resourceItem; } }

	public int CreateCount { get { return createCount; } }

	public int CreateLimitCount { get { return createLimitCount; } }

	public bool ItemCreate { get { return itemCreate; } }

	// unity method
	void Awake()
	{		
		LinkComponentElement();
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

	// data & component link
	public void LinkComponentElement()
	{
		manager = GetComponent<GameManager>();
		characterManager = GetComponent<CharacterManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		storeField = GameObject.FindWithTag( "StoreField" ).gameObject;
		storeFieldTexture = storeField.GetComponent<Renderer>().material;
		reticleLine = storeField.transform.Find( "ReticleLine" ).gameObject;
		reticleLineTexture = reticleLine.GetComponent<Image>();
		storeBackground = GameObject.FindWithTag( "StoreBackground" ).gameObject;
	}

	// create & destroy store section
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
		if( Input.GetButtonDown( "LeftClick" ) && !EventSystem.current.IsPointerOverGameObject() )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentSelectedObject = tempSearch.GetComponent<FurnitureObject>();
				
				if( presentSelectedObject.InstanceData.Furniture.Function == FurnitureData.FunctionType.CreateObject )
				{					
					PullCreateItemData();
					mainUI.CreateUI.SetActive( true );
					mainUI.CreateUILogic.SetComponentElement();
				}
			}
		}
	}

	// custimize section
	// customzing store object
	public void CustomzingFurnitureObject()
	{
		if( !mainUI.CustomizeUI.activeSelf )
			return;

		// reload ray
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// clear furniture object -> when mouse button right click
		if( Input.GetButtonDown( "LeftClick" ) && ( presentSelectedObject != null ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{				
				if( hitInfo.collider.gameObject.GetComponent<FurnitureObject>() == presentSelectedObject )
					ConfirmMoveFurnitureObject();
			}
		}
		// set up furniture object => when mouse button right click
		else if( Input.GetButtonDown( "LeftClick" ) && ( presentSelectedObject == null ) )
		{
			// cast & check furniture object -> if exist -> set present object
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				// object allocate mode -> move mode
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentSelectedObject = tempSearch.GetComponent<FurnitureObject>();
				presentSelectedObject.AllocateMode = true;
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

	// allocate start
	public void AllocateStartFurnitureInstance( int index, int presentStepIndex )
	{
		if( presentSelectedObject != null )
			return;

		if( manager.GamePlayer.AllocateFurnitureInstance( index, presentStepIndex ) )
		{
			// create object
			GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/" + manager.GamePlayer.AllocateFurnitureSet[ manager.GamePlayer.AllocateFurnitureSet.Count - 1 ].Furniture.File ), 
			                                              new Vector3( planeScale / 2f, 0f, planeScale / 2f ), 
			                                              Quaternion.identity );
			presentSelectedObject = temp.GetComponent<FurnitureObject>();
			presentSelectedObject.InstanceData = manager.GamePlayer.AllocateFurnitureSet[ manager.GamePlayer.AllocateFurnitureSet.Count - 1 ];
			presentSelectedObject.LinkComponentElement();
			presentSelectedObject.AllocateMode = true;	
			presentSelectedObject.CheckAllocatePossible();

			// link player data & object
			manager.GamePlayer.AllocateFurnitureObjectSet.Add( presentSelectedObject );
		}
	}

	// confirm allocate object
	public void ConfirmMoveFurnitureObject()
	{
		presentSelectedObject.CheckAllocatePossible();

		// allocate possible
		if( presentSelectedObject.AllocatePossible )
		{
			// set allocate mode false
			presentSelectedObject.AllocateMode = false;

			// clear present object 
			presentSelectedObject = null;
		}
	}

	// cancel allocate object
	public void CancelAllocateFurnitureObject()
	{
		if( manager.GamePlayer.AddFurnitureData( presentSelectedObject.InstanceData.Furniture.ID ) )
		{
			manager.GamePlayer.DeleteAllocateFurniture( presentSelectedObject.InstanceData.SlotNumber );
		}
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
		presentSelectedObject.ChangeObjectPosition( presentSelectedObject.transform.position + moveDirection );
	}

	// allocate furniture object rotation set
	public void AllocateFurnitureObjectRotationSet( int direction )
	{
		// rotation change
		switch( direction )
		{
			case 1:
				presentSelectedObject.ChangeObjectRotation( "Left" );
				break;
			case 2:
				presentSelectedObject.ChangeObjectRotation( "Right" );
				break;
			case 3:
				presentSelectedObject.ChangeObjectRotation( "Reset" );
				break;
		}
	}

	// create section
	// pull data & step
	public void PullCreateItemData()
	{
		viewItemGroup = new List<ItemData>( );
		for( int i = 0; i < DataManager.SearchItemList.Count; i++ )
		{
			if( DataManager.SearchItemList[ i ].Step <= presentSelectedObject.InstanceData.Furniture.Step )
			{
				for( int j = 0; j < presentSelectedObject.InstanceData.Furniture.CreateItemGroupSet.Length; j++ )
				{
					if( presentSelectedObject.InstanceData.Furniture.CreateItemGroupSet[ j ] == ( int ) DataManager.SearchItemList[ i ].Type )
						viewItemGroup.Add( DataManager.SearchItemList[ i ] );
				}
			}
		}
		presentIndex = 0;
	}

	// select item
	public void SelectCreateItem( int index )
	{
		resourceItem = new List<ItemInstance>( );
		// select item
		selectedItem = viewItemGroup[ index + ( presentIndex * mainUI.CreateUILogic.ListSlotLength ) ];

		// check items ( gp pull item )
		for( int i = 0; i < manager.GamePlayer.ItemSet.Length; i++ )
		{
			for( int j = 0; j < selectedItem.ResourceIDSet.Length; j++ )
			{
				if( manager.GamePlayer.ItemSet[ i ].Item.ID == selectedItem.ResourceIDSet[ j ] )
				{
					resourceItem.Add( manager.GamePlayer.ItemSet[ i ] );
				}
			}
		}

		createLimitCount = 999;

		// set create count
		for( int i = 0; i < selectedItem.ResourceIDSet.Length; i++ )
		{
			try
			{
				for( int j = 0; j < resourceItem.Count; j++ )
				{
					if( resourceItem[ j ].Item.ID == selectedItem.ResourceIDSet[ i ] )
					{
						if( ( int ) ( resourceItem[ j ].Count / selectedItem.ResourceCountSet[ i ] ) < createLimitCount )
							createLimitCount = ( int ) ( resourceItem[ j ].Count / selectedItem.ResourceCountSet[ i ] );
					}
				}
			}
			catch( NullReferenceException e )
			{
				createCount = 0;
				createLimitCount = 0;
			}
		}
	}

	// coroutine
	public IEnumerator CreateStoreObject()
	{
		SetPlaneScale();

		// create object
		try
		{
			// set store field image & reticle line image
			storeFieldTexture.mainTexture = Resources.Load<Texture>( "Image/StoreField/StoreFieldStep" + manager.GamePlayer.StoreData.StoreStep );

			// create tilemap object - size check & texture setting
			storeField.transform.localScale = new Vector3( planeScale / 10f, planeScale / 10f, planeScale / 10f );
			storeField.transform.position = new Vector3( planeScale / 2f, 0, planeScale / 2f );

			// set reticle line image
			reticleLine.SetActive( false );

			// create store wall & background move & nav field
			switch( manager.GamePlayer.StoreData.StoreStep )
			{
				case 1:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stStep" ), new Vector3( 4.5f, 0f, 5.5f ), Quaternion.Euler( 0f, 90f, 0f ) );
					storeNavField = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/NavMeshObstacle/Step1NavField" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeBackground.transform.position = new Vector3( 0f, -0.01f, 0f );
					break;
				case 2:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stStep" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeNavField = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/NavMeshObstacle/Step2NavField" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
					storeBackground.transform.position = new Vector3( 5f, -0.01f, 5f );
					break;
				case 3:
					storeWall = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/StoreWall1stStep" ), new Vector3( planeScale / 2f, 0f, planeScale / 2f ), Quaternion.identity );
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
						furnitureObjectSet[ i ].LinkComponentElement();
					}
				}

				manager.GamePlayer.AllocateFurnitureObjectSet = furnitureObjectSet;
			}

			// make player character
			characterManager.CreatePlayerAgent();

		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			Debug.Log( "Store Create Fail -> data load fail" );
			createComplete = false;
			yield break;
		}

		createComplete = true;

		yield break;
	}

}
