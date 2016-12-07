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

	// field - result
	[SerializeField] float playTime;
	[SerializeField] int rankProfitMoney;
	[SerializeField] int rankProfitCount;
	[SerializeField] StageResultData resultData;
	  
	// field - result reward
	[SerializeField] int touchCount;
	[SerializeField] bool[] isOpened;
	[SerializeField] bool eventStart;
	
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

	public int TouchCount { get { return touchCount; } }

	public bool[] IsOpened { get { return isOpened; } }

	public float PlayTime { get { return playTime; } }

	public StageResultData ResultData { get { return resultData; } }

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

		isOpened = new bool[20];
		for( int i = 0; i < isOpened.Length; i++ )
			isOpened[ i ] = false;
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
		stageTime = 10f;

		// set customer cycle 
		customerCycle = 3f;
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

		// reset data
		presentTime = 0.0f;
		rankProfitMoney = 0;
		rankProfitCount = 0;
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
			// set up rank infor
			SetUpRankData();

			// end game
			manager.SetStoreStageEnd();
		}

		// stage policy
		StagePreprocessPolicy();
	}

	// set up rank information
	public void SetUpRankData()
	{
		playTime = presentTime;
		resultData = DataManager.ReturnStageResultData( manager.GamePlayer.StoreData.StoreStep, rankProfitCount, rankProfitMoney );
		touchCount = resultData.RewardTouchCount;
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
		catch( NullReferenceException e )
		{
			Debug.Log( "물건이 없슴메 ㅠㅠ" );
			return;
		}

		sellObject.SellItem[ itemSlotIndex ].Count -= maxCount;
		manager.GamePlayer.Gold += sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;
		gold -= sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;

		rankProfitCount += maxCount;
		rankProfitMoney += sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;

		if( sellObject.SellItem[ itemSlotIndex ].Count <= 0 )
			sellObject.SellItem[ itemSlotIndex ] = null;
	}
	
	
	// result reward control
	// reset data
	public void ResetResultRewardData()
	{
		for( int i = 0; i < isOpened.Length; i++ )
			isOpened[ i ] = false;

		touchCount = resultData.RewardTouchCount;
		eventStart = false;
	}

	// select card
	public void SelectCard( int index, out int cardIndex )
	{
		touchCount--;
		isOpened[ index ] = true;
		cardIndex = UnityEngine.Random.Range( 0, 12 );
		if( !eventStart )
		{
			eventStart = true;		
		}

		int itemID = 0;
		int itemCount = 0;
		switch( ( cardIndex + 1 ) % 4 )
		{
			case 1:
				// blue powder
				itemID = 22;
				break;
			case 2:
				// red powder
				itemID = 23;
				break;
			case 3:
				// yellow herb
				itemID = 16;
				break;
			case 0:
				// purple herb
				itemID = 17;
				break;
		}

		switch( cardIndex / 4 )
		{
			case 0:
				itemCount = 1;
				break;
			case 1:
				itemCount = 4;
				break;
			case 2:
				itemCount = 9;
				break;			
		}

		manager.GamePlayer.AddItemData( itemID, itemCount );
	}

	// coroutine section
	// customger section -> use cycle / customer go to store
	IEnumerator CustomerGoStore()
	{		
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
