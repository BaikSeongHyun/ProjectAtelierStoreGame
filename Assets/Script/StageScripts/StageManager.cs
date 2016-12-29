using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] CharacterManager charManager;
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
	[SerializeField] ItemObject tempItemObject;

	// field - sell log data
	[SerializeField] List<int> sellItem;
	[SerializeField] List<int> sellGold;

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

	// sell log data
	public List<int> SellItem { get { return sellItem; } }

	public List<int> SellGold { get { return sellGold; } }

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
		// high structure 
		manager = GetComponent<GameManager>();
		charManager = GetComponent<CharacterManager>();
		;
		customerAgentSet = GameObject.FindWithTag( "CustomerSet" ).GetComponentsInChildren<CustomerAgent>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		stageUI = mainUI.transform.Find( "StageUI" ).GetComponent<StageUI>();

		isOpened = new bool[20];
		for( int i = 0; i < isOpened.Length; i++ )
			isOpened[ i ] = false;

		// sell log data
		sellItem = new List<int>( );
		sellGold = new List<int>( );
	}

	// stage pre process
	// set item price & count
	public void StageItemSellRegisterPolicy()
	{
		// ray cast set furniture target
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// target function type -> create : use create ui(object type)
		// target function type -> storage : use storage ui(object type)
		if( Input.GetButtonDown( "LeftClick" ) && !EventSystem.current.IsPointerOverGameObject( Input.GetTouch( 0 ).fingerId ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentSelectedFurniture = tempSearch.GetComponent<FurnitureObject>();

				if( presentSelectedFurniture.InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
				{
					charManager.PlayerableCharacter.SetItemSettingMode();	
				}
				else
				{
					charManager.PlayerableCharacter.SetFurnitureObjectPoint();
				}
			}
			else if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
			{
				charManager.PlayerableCharacter.SetMovePoint( hitInfo.point );
			}
			manager.SoundManager.PlayUISoundPlayer(4);
		}

		ItemAcquire();
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
		favoriteGroup = ItemData.ReturnType( UnityEngine.Random.Range( 2, 8 ) );

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
		stageTime = manager.GamePlayer.StoreData.StageTime;

		// set customer cycle 
		customerCycle = 3f;
	}

	// stage process
	// custmer policy activate -> use coroutine
	public void StoreOpen()
	{
		StartCoroutine( CustomerGoStore() );
		StartCoroutine( PinkyGoToStore() );
		StartCoroutine( stageUI.ActivateDrive() );

		// reset data
		presentTime = 0.0f;
		rankProfitMoney = 0;
		rankProfitCount = 0;
	}

	// marshmello throw item -> logic
	public void ThrowSellItem( int targetIndex, int throwItemIndex )
	{
		sellFurnitureSet[ targetIndex ].SellItem[ throwItemIndex ] = new ItemInstance( );
		sellFurnitureSet[ targetIndex ].SellItemObject[ throwItemIndex ] = null;
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

		// attack marshmello
		if( charManager.MarshmelloActivate && charManager.DamageMarshmello() )
		{
			manager.SoundManager.PlayUISoundPlayer(4);
			return;
		}

		// item acquire
		if( ItemAcquire() )			
		{	
			manager.SoundManager.PlayUISoundPlayer(4);
			return;
		}

		// stage policy -> sell setting
		StageItemSellRegisterPolicy();
	}

	// acquire items
	public bool ItemAcquire()
	{
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		if( Input.GetButtonDown( "LeftClick" ) && !EventSystem.current.IsPointerOverGameObject( Input.GetTouch( 0 ).fingerId ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Item" ) ) )
			{
				GameObject tempSearch = hitInfo.collider.gameObject;

				if( tempSearch.name == "InfoBubble(Clone)" )
					tempItemObject = tempSearch.GetComponentInParent<ItemObject>();
				else
					return false;
				
				if( tempItemObject.IsField )
				{
					tempItemObject.AcquireItems();
					return true;
				}
			}
		}

		return false;
	}

	// set up rank information
	public void SetUpRankData()
	{
		playTime = presentTime;
		resultData = DataManager.ReturnStageResultData( manager.GamePlayer.StoreData.StoreStep, rankProfitCount, rankProfitMoney );
		touchCount = resultData.RewardTouchCount;
	}

	public void SetDataReset()
	{
		for( int i = 0; i < sellFurnitureSet.Count; i++ )
		{
			for( int j = 0; j < sellFurnitureSet[ i ].SellItem.Length; j++ )
			{				
				if( ( sellFurnitureSet[ i ].SellItem[ j ] != null ) && ( sellFurnitureSet[ i ].SellItem[ j ].Item != null ) && ( sellFurnitureSet[ i ].SellItem[ j ].Item.ID == 0 ) )
				{
					manager.GamePlayer.AddItemData( sellFurnitureSet[ i ].SellItem[ j ].Item.ID, sellFurnitureSet[ i ].SellItem[ j ].Count );
					sellFurnitureSet[ i ].SellItem[ j ] = new ItemInstance( );
				}
			}
		}

		// layer item -> all delete
		GameObject[] tempArray = FindObjectsOfType<GameObject>();
		for( int i = 0; i < tempArray.Length; i++ )
		{
			try
			{
				if( tempArray[ i ].layer == LayerMask.NameToLayer( "Item" ) )
				{
					Destroy( tempArray[ i ] );
				}
			}
			catch( NullReferenceException e )
			{
				// object already break
			}
		}

		// log data reset
		sellItem.Clear();
		sellGold.Clear();
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

		charManager.PinkyAgent.GoToHome();
	}

	public void BuyItem( FurnitureObject sellObject, int itemSlotIndex, ref int gold )
	{
		int maxCount;
		try
		{
			// set maximum count
			maxCount = ( int ) ( gold / sellObject.SellItem[ itemSlotIndex ].SellPrice );
			maxCount = maxCount > sellObject.SellItem[ itemSlotIndex ].Count ? sellObject.SellItem[ itemSlotIndex ].Count : maxCount;

			// change count
			maxCount = UnityEngine.Random.Range( 1, maxCount + 1 );

			// buy items
			sellObject.SellItem[ itemSlotIndex ].Count -= maxCount;
			manager.GamePlayer.Gold += sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;
			gold -= sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;

			rankProfitCount += maxCount;
			rankProfitMoney += sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount;

			// add log data
			sellItem.Add( sellObject.SellItem[ itemSlotIndex ].Item.ID );
			sellGold.Add( sellObject.SellItem[ itemSlotIndex ].SellPrice * maxCount );

			if( SellItem.Count > 5 )
			{
				sellItem.RemoveAt( 0 );
				sellGold.RemoveAt( 0 );
			}

			stageUI.SetComponentElement();

			if( sellObject.SellItem[ itemSlotIndex ].Count <= 0 )
			{
				sellObject.SellItem[ itemSlotIndex ] = null;
			}

			sellObject.SellItemObject[ itemSlotIndex ].SetComponentElement();
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

	// select reward card
	public void SelectCard( int index, out int itemID, out int itemCount, out int rare )
	{
		touchCount--;
		isOpened[ index ] = true;

		if( !eventStart )
		{
			eventStart = true;		
		}
		itemID = 0;
		itemCount = 0;
		rare = 0;

		// set rare grade
		int rareSelect = UnityEngine.Random.Range( 1, 101 );

		if( rareSelect <= 60 )
			rare = 1;
		else if( rareSelect <= 90 )
			rare = 2;
		else
			rare = 3;

		// set item count
		switch( rare )
		{
			case 1:
				itemCount = UnityEngine.Random.Range( 1, 7 );
				break;
			case 2:
				itemCount = UnityEngine.Random.Range( 1, 4 );
				break;
			case 3:
				itemCount = 1;
				break;
		}

		// set item id
		switch( rare )
		{
			case 1:
				itemID = UnityEngine.Random.Range( 1, 8 );
				break;
			case 2:
				itemID = UnityEngine.Random.Range( 8, 24 );
				break;
			case 3:
				itemID = UnityEngine.Random.Range( 25, 40 );
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

	IEnumerator PinkyGoToStore()
	{
		// loop pinky
		while( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			yield return new WaitForSeconds( 25f );

			// if time has come -> go to store
			// activate marshmello
			if( ( !charManager.PinkyAgent.RestMarshmello ) && ( manager.PresentMode == GameManager.GameMode.StoreOpen ) )
			{
				charManager.ActivateMarshMello();
			}
		}
	}
}
