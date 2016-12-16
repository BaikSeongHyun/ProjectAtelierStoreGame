using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CustomerAgent : AIAgent
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] StageManager stageManager;

	// field - data
	[SerializeField] string customerName;
	[SerializeField] ItemData.ItemType favoriteItemType;
	[SerializeField] BuyScale buyScale;
	[SerializeField] int gold;

	// field buy item index
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] float calculatedBuyScale;
	[SerializeField] int itemIndex;
	[SerializeField] bool alreadyBuyItems;

	// field - logic point
	[SerializeField] Transform waitingPoint;
	[SerializeField] Transform storeEnterPoint;
	[SerializeField] Transform storeDoor;
	[SerializeField] Transform[] worldBoundary;
	[SerializeField] Transform moveTarget;
	[SerializeField] Sequence presentSequence;

	// logic object
	[SerializeField] System.Random randomPoint;

	// enum state -> customer logic sequence
	public enum Sequence : int
	{
		Ready = 1,
		GoToStore = 2,
		SetTarget = 3,
		Buy = 4,
		GoToHome = 5,
		WaitAnimation = 6}

	;

	public enum BuyScale : int
	{
		Default = 0,
		Smaller = 1,
		Small = 2,
		Standard = 3,
		Large = 4,
		Larger = 5}

	;

	// property
	public Sequence PresentSequence { get { return presentSequence; } }

	// unity standard method
	// awake
	void Awake()
	{
		DataInitialize();	
	}

	// update
	void Update()
	{
		// customer logic sequence
		switch( presentSequence )
		{
			case Sequence.Ready:
				moveAgent.enabled = false;
				break;
			case Sequence.GoToStore:
				moveAgent.SetDestination( moveTarget.position );
				presentState = AgentState.Walk;
				break;
			case Sequence.SetTarget:
				SequenceProcessSearchTarget();
				break;
			case Sequence.Buy:
				SequenceProcessBuy();
				break;
			case Sequence.GoToHome:
				SequenceProcessGoToHome();
				break;
			case Sequence.WaitAnimation:
				moveAgent.ResetPath();
				break;
		}
	}

				                       
	// on trigger enter -> customer policy
	void OnTriggerEnter( Collider col )
	{		
		if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) )
		{
			moveAgent.ResetPath();
			agentAnimator.SetTrigger( "PickUp" );
			presentSequence = Sequence.WaitAnimation;
		}
		if( col.gameObject.name == "CustomerStoreEnterPoint" && ( PresentSequence != Sequence.GoToHome ) )
		{
			moveTarget = storeDoor;
		}
		else if( col.gameObject.name == "StoreDoor" && ( PresentSequence != Sequence.GoToHome ) )
		{
			moveTarget = null;
			moveAgent.ResetPath();
			agentAnimator.SetTrigger( "SearchItem" + UnityEngine.Random.Range( 1, 3 ) );
			presentSequence = Sequence.WaitAnimation;

		}
		else if( ( col.gameObject.layer == LayerMask.NameToLayer( "WorldBoundary" ) ) && ( PresentSequence == Sequence.GoToHome ) )
		{
			GoToHome();
		}
	}

	// on trigger enter -> only use buy sequence
	void OnTriggerStay( Collider col )
	{
		// trigger stay bug -> don't move customer
		if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && presentSequence == Sequence.Buy )
		{
			moveAgent.ResetPath();
			agentAnimator.SetTrigger( "PickUp" );
			presentSequence = Sequence.WaitAnimation;
		}
	}

	// public method
	// override -> data initialize
	public override void DataInitialize()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();

		// set points
		GameObject[] temp = GameObject.FindGameObjectsWithTag( "WorldBoundary" );
		worldBoundary = new Transform[temp.Length];
		for( int i = 0; i < worldBoundary.Length; i++ )
		{
			worldBoundary[ i ] = temp[ i ].transform;
		}	
		waitingPoint = GameObject.FindWithTag( "CustomerWaitingPoint" ).transform;
		storeEnterPoint = GameObject.FindWithTag( "CustomerStoreEnterPoint" ).transform;

		// logic component
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();

		// data component reset
		ResetCustomerAgent();
	}

	// reset information
	public void ResetCustomerAgent()
	{
		moveAgent.enabled = true;
		moveAgent.ResetPath();
		moveAgent.enabled = false;
		transform.position = waitingPoint.position; 
		gold = UnityEngine.Random.Range( 500, 1000 );
		moveAgent.speed = UnityEngine.Random.Range( 4f, 6f );
		presentSequence = Sequence.Ready;		
	}

	// move start customer agent
	public void ActivateCustomerAgent( int customerIndex )
	{
		name = "손님" + ( customerIndex + 1 );
		GoToStore();
	}

	// set favorite group & buy scale
	public void SetBuyInformation()
	{
		// buy scale
		if( UnityEngine.Random.Range( 1, 101 ) >= stageManager.ProScale )
		{
			buyScale = stageManager.BuyScale;
		}
		else
		{
			int i = ( int ) stageManager.BuyScale;
			while( i != ( int ) stageManager.BuyScale )
			{
				i = UnityEngine.Random.Range( 1, 6 );
			}
			buyScale = CustomerAgent.ReturnBuyScale( i );
		}

		// favorite group
		if( UnityEngine.Random.Range( 1, 101 ) >= stageManager.ProFavor )
		{
			favoriteItemType = stageManager.FavoriteGroup;
		}
		else
		{
			int i = ( int ) stageManager.FavoriteGroup;
			while( i != ( int ) stageManager.FavoriteGroup )
			{
				i = UnityEngine.Random.Range( 1, 8 );
			}
			favoriteItemType = ItemData.ReturnType( i );
		}

		calculatedBuyScale = BuyScaleSetFloatType( buyScale );

		storeDoor = GameObject.Find( "StoreDoor" ).transform;
		moveTarget = storeEnterPoint;
	}

	// go to store enter point
	public void GoToStore()
	{
		moveAgent.enabled = false;
		transform.position = worldBoundary[ UnityEngine.Random.Range( 0, worldBoundary.Length ) ].position;
		moveAgent.enabled = true;
		presentSequence = Sequence.GoToStore;
	}

	// search item
	public void SequenceProcessSearchTarget()
	{
		// set random index & point
		int randomIndex;
		randomPoint = new System.Random( );

		// check sell item set
		for( int i = 0; i < stageManager.SellFurnitureSet.Count; i++ )
		{
			randomIndex = randomPoint.Next( 0, stageManager.SellFurnitureSet.Count );
			// favorite item type
			for( int j = 0; j < stageManager.SellFurnitureSet[ randomIndex ].SellItem.Length; j++ )
			{
				try
				{
					if( favoriteItemType == stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].Item.Type )
					{
						if( ( stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].SellPrice <= gold ) && ( stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].SellPrice <= stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].Item.Price * ( calculatedBuyScale + 0.1f ) ) )
						{
							targetObject = stageManager.SellFurnitureSet[ randomIndex ];
							itemIndex = j;
							presentSequence = Sequence.Buy;
							return;
						}
						else
						{
							Debug.Log( "this Item is so expensive" );
						}
					}
				}
				catch( NullReferenceException e )
				{
					//Debug.Log( "No item this slot" );
				}
			}
		}

		// reset ramdom point
		randomPoint = new System.Random( );

		// check sell item set
		for( int i = 0; i < stageManager.SellFurnitureSet.Count; i++ )
		{
			randomIndex = randomPoint.Next( 0, stageManager.SellFurnitureSet.Count );

			// normal item type
			for( int j = 0; j < stageManager.SellFurnitureSet[ randomIndex ].SellItem.Length; j++ )
			{
				try
				{
					if( ( stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].SellPrice <= gold ) && ( stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].SellPrice <= stageManager.SellFurnitureSet[ randomIndex ].SellItem[ j ].Item.Price * ( calculatedBuyScale ) ) )
					{
						targetObject = stageManager.SellFurnitureSet[ randomIndex ];
						itemIndex = j;
						presentSequence = Sequence.Buy;
						return;
					}
					else
					{
						Debug.Log( "this Item is so expensive" );
					}
				}
				catch( NullReferenceException e )
				{	
					//Debug.Log( "No item this slot" );
				}
			}
		}

		// set no item opinion
		if( !alreadyBuyItems )
			;
		else
			Debug.Log( "No Items : " + name );
		
		moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, worldBoundary.Length ) ];
		presentSequence = Sequence.GoToHome;
	}

	public void SequenceProcessBuy()
	{				
		// check & buy
		if( targetObject != null )
		{
			moveAgent.SetDestination( targetObject.transform.position );	
		}
		else
		{
			moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, worldBoundary.Length ) ];
			presentSequence = Sequence.GoToHome;
		}
	}

	public void SequenceProcessGoToHome()
	{
		if( manager.PresentMode != GameManager.GameMode.StoreOpen )
		{
			ResetCustomerAgent();
		}
		else
		{
			moveAgent.SetDestination( moveTarget.position );
			presentState = AgentState.Walk;
		}
	}

	// warp waiting point
	public void GoToHome()
	{
		presentSequence = Sequence.Ready;
		moveTarget = null;
		moveAgent.enabled = false;
		transform.position = waitingPoint.position;
		presentSequence = Sequence.Ready;
	}

	// animation event method
	// after search
	public void SearchEndEvent()
	{
		// check target
		SequenceProcessSearchTarget();
	}

	//after pick up
	public void PickUpEndEvent()
	{
		// buy item
		stageManager.BuyItem( targetObject, itemIndex, ref gold );

		// check next target
		SequenceProcessSearchTarget();
	}

	// static method
	// return buy scale
	public static BuyScale ReturnBuyScale( int _buyScale )
	{
		BuyScale returnType = BuyScale.Default;
		switch( _buyScale )
		{
			case 1:
				returnType = CustomerAgent.BuyScale.Smaller;
				break;
			case 2:
				returnType = CustomerAgent.BuyScale.Small;
				break;
			case 3:
				returnType = CustomerAgent.BuyScale.Standard;
				break;
			case 4:
				returnType = CustomerAgent.BuyScale.Large;
				break;
			case 5:
				returnType = CustomerAgent.BuyScale.Larger;
				break;				
		}

		return returnType;
	}

	// return calculate buy scale
	public static float BuyScaleSetFloatType( BuyScale scale )
	{
		float result = 1f;

		switch( scale )
		{
			case BuyScale.Smaller:
				result = 1.05f;
				break;
			case BuyScale.Small:
				result = 1.10f;
				break;
			case BuyScale.Standard:
				result = 1.20f;
				break;
			case BuyScale.Large:
				result = 1.30f;
				break;
			case BuyScale.Larger:
				result = 1.50f;
				break;
			case BuyScale.Default:
				result = 1.0f;
				break;
		}

		return result;
	}

	// return buy scale string
	public static string ReturnBuyScaleString( BuyScale scale )
	{	
		switch( scale )
		{
			case BuyScale.Smaller:
				return "많이 싸게";
			case BuyScale.Small:
				return "조금 싸게";
			case BuyScale.Standard:
				return "평범하게";
			case BuyScale.Large:
				return "조금 비싸게";
			case BuyScale.Larger:
				return "대박! 비싸게";
			case BuyScale.Default:
				return "안사요";
		}

		return null;
	}
}
