using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] UIManager mainUI;
	[SerializeField] StageUI stageUI;

	// field - store open data
	[SerializeField] float customerCycle;
	[SerializeField] int customerIndex;

	// field - information data
	[SerializeField] int probabilityOfBuyScale;
	[SerializeField] int probabilityOfFavoriteGroup;
	[SerializeField] ItemData.ItemType favoriteGroup;
	[SerializeField] CustomerAgent.BuyScale buyScale;

	// field - element object
	[SerializeField] PlayerAgent playerAgent;
	[SerializeField] KakaoAgent kakaoAgent;
	[SerializeField] CustomerAgent[] customerAgentSet;

	// field - stage data
	[SerializeField] float presentTime;
	[SerializeField] float stageTime;
	[SerializeField] List<FurnitureObject> sellFurnitureSet;

	// field selected object
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] CustomerAgent presentSelectedCustomer;
	[SerializeField] FurnitureObject presentSelectedFurniture;

	// field - ui process temp data
	[SerializeField] ItemInstance tempItemInstance;

	// property
	public FurnitureObject PresentSelectedFurniture { get { return presentSelectedFurniture; } }

	public ItemData.ItemType FavoriteGroup { get { return favoriteGroup; } }

	public CustomerAgent.BuyScale BuyScale { get { return buyScale; } }

	public int ProScale { get { return probabilityOfBuyScale; } }

	public int ProFavor { get { return probabilityOfFavoriteGroup; } }

	public List<FurnitureObject> SellFurnitureSet { get { return sellFurnitureSet; } }

	public float FreeTime { get { return ( stageTime - presentTime ); } }

	public float TimeFill { get { return( 1 - ( presentTime / stageTime ) ); } }

	// unity method
	// awake -> data initialize
	void Awake()
	{
		DataInitialize();
	}

	// public method
	// create game information
	public void DataInitialize()
	{
		manager = GetComponent<GameManager>();
		customerAgentSet = GameObject.FindWithTag( "CustomerSet" ).GetComponentsInChildren<CustomerAgent>();
		kakaoAgent = GameObject.Find( "KakaoAgent" ).GetComponent<KakaoAgent>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		stageUI = mainUI.transform.Find( "StageUI" ).GetComponent<StageUI>();
	}

	// stage pre process
	// set item price & count
	public void StagePreprocessPolicy()
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
				presentSelectedFurniture = tempSearch.GetComponent<FurnitureObject>();

				if( presentSelectedFurniture.InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
				{
					mainUI.ActivateSellItemSettingUI();				
				}
			}
		}
	}

	// create customer setting information
	public void CreateGameInformation()
	{
		// allocate probability
		switch( manager.GamePlayer.StoreData.StoreStep )
		{
			case 1:
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 100;
				break;
			case 2:				
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 90;
				break;
			case 3:				
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 80;				
				break;
		}

		// allocate data
		buyScale = CustomerAgent.ReturnBuyScale( UnityEngine.Random.Range( 1, 6 ) );
		favoriteGroup = ItemData.ReturnType( UnityEngine.Random.Range( 1, 8 ) );

		// set sell furiture object set
		sellFurnitureSet = new List<FurnitureObject>( );
		for( int i = 0; i < manager.GamePlayer.AllocateFurnitureObjectSet.Count; i++ )
		{
			if( manager.GamePlayer.AllocateFurnitureObjectSet[ i ].InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
			{
				sellFurnitureSet.Add( manager.GamePlayer.AllocateFurnitureObjectSet[ i ] );
			}
		}

		// set stage time
		stageTime = 90f;

		// set customer cycle 
		customerCycle = 3f;

		// reset stage ui
		stageUI.ResetComponent();
	}

	// stage process
	public void ActivateKakao()
	{
		kakaoAgent.GoToStore();
	}

	// custmer policy activate -> use coroutine
	public void StoreOpen()
	{
		StartCoroutine( CustomerGoStore() );
		StartCoroutine( stageUI.ActivateDrive() );
		presentTime = 0.0f;
	}

	public void StagePreprocessReturn()
	{
		kakaoAgent.GoToOffice();
		SetItemsReset();
		manager.SetStoreMode();
	}

	public void StageProcessReturn()
	{
		kakaoAgent.GoToOffice();
		SetItemsReset();		
		CustomerReset();
		manager.SetStoreMode();
	}

	public void StageProcessPolicy()
	{
		// add time check
		presentTime += Time.deltaTime;

		// exit statement
		if( presentTime >= stageTime )
		{
			manager.SetStoreStageEnd();
		}

		// stage policy
		StagePreprocessPolicy();
	}

	public void SetItemsReset()
	{
		for( int i = 0; i < sellFurnitureSet.Count; i++ )
		{
			for( int j = 0; j < sellFurnitureSet[ i ].SellItem.Length; j++ )
			{				
				if( sellFurnitureSet[ i ].SellItem[ j ] != null )
				{
					manager.GamePlayer.AddItemData( sellFurnitureSet[ i ].SellItem[ j ].Item.ID, sellFurnitureSet[ i ].SellItem[ j ].Count );
					sellFurnitureSet[ i ].SellItem[ j ] = new ItemInstance( );
				}
			}
		}
	}

	public void CustomerReset()
	{
		// reset index
		customerIndex = 0;
	
		// position reset & data reset
		for( int i = 0; i < customerAgentSet.Length; i++ )
		{
			if( customerAgentSet[ i ].PresentSequence != CustomerAgent.Sequence.Ready )
				customerAgentSet[ i ].ResetCustomerAgent();
		}
	}

	public void BuyItem( FurnitureObject sellObject, int itemSlotIndex, ref int gold )
	{
		int maxCount;
		try
		{
			maxCount = ( int ) ( gold / sellObject.SellItem[ itemSlotIndex ].SellPrice );
			maxCount = maxCount > sellObject.SellItem[ itemSlotIndex ].Count ? sellObject.SellItem[ itemSlotIndex ].Count : maxCount;
		}
		catch( DivideByZeroException e )
		{
			maxCount = sellObject.SellItem[ itemSlotIndex ].Count;
		}

		sellObject.SellItem[ itemSlotIndex ].Count -= maxCount;
		manager.GamePlayer.Gold += sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;
		gold -= sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;

		if( sellObject.SellItem[ itemSlotIndex ].Count <= 0 )
			sellObject.SellItem[ itemSlotIndex ] = null;
	}

	// coroutine section
	// customger section -> use cycle / customer go to store
	IEnumerator CustomerGoStore()
	{
		// set information
		CreateGameInformation();

		// loop customer
		while( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			// set infor
			customerAgentSet[ customerIndex % customerAgentSet.Length ].SetBuyInformation();

			// move start
			customerAgentSet[ customerIndex % customerAgentSet.Length ].ActivateCustomerAgent( customerIndex );
			customerIndex++;

			yield return new WaitForSeconds( customerCycle ); 
		}

		CustomerReset();
	}
}
