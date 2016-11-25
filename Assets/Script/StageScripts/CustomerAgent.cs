using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerAgent : AIAgent
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] StageManager stageManager;

	// field - data
	[SerializeField] string name;
	[SerializeField] ItemData.ItemType favoriteItemType;
	[SerializeField] BuyScale buyScale;
	[SerializeField] int gold;

	// field - logic
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] Transform waitingPoint;
	[SerializeField] Transform startPoint;
	[SerializeField] Transform storeEnterPoint;
	[SerializeField] Transform endPoint;
	[SerializeField] Transform storeInside;
	[SerializeField] Transform storeOutside;
	[SerializeField] Sequence presentSequence;
	[SerializeField] List<FurnitureObject> findObjectSet;
	[SerializeField] bool isFind;

	// enum state
	public enum Sequence : int
	{
		Ready = 1,
		GoToStore = 2,
		EnterStore = 3,
		SetTarget = 4,
		Buy = 5,
		ExitStore = 6,
		GoToHome = 7}

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
		switch( presentSequence )
		{
			case Sequence.Ready:
				moveAgent.enabled = false;
				break;
			case Sequence.GoToStore:
				moveAgent.SetDestination( storeEnterPoint.position );
				break;
			case Sequence.EnterStore:
				moveAgent.SetDestination( storeOutside.position );
				break;
			case Sequence.SetTarget:
				SequenceProcessSearchTarget();
				break;
			case Sequence.Buy:
				SequenceProcessBuy();
				break;
			case Sequence.ExitStore:
				moveAgent.SetDestination( storeInside.position );
				break;
			case Sequence.GoToHome:
				moveAgent.SetDestination( endPoint.position );
				break;
		}
	}

	// on trigger enter -> customer policy
	void OnTriggerEnter( Collider col )
	{
		switch( col.gameObject.name )
		{
			case "CustomerStoreEnterPoint":
				presentSequence = Sequence.EnterStore;
				break;
			case "CustomerEndPoint":
				ResetCustomerAgent();
				break;
			case "CustomerStoreInDoor":
				WarpStoreIn();
				break;
			case "CustomerStoreOutDoor":
				WarpStoreOut();
				break;
		}

		if( isFind && ( col.gameObject.GetComponent<FurnitureObject>() == targetObject ) )
		{				
			// check sell item & buy or no buy item
			
			
			// post process
			findObjectSet.Remove( targetObject );
			presentSequence = Sequence.SetTarget;
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
		waitingPoint = GameObject.FindWithTag( "CustomerWaitingPoint" ).transform;
		startPoint = GameObject.FindWithTag( "CustomerStartPoint" ).transform;
		storeEnterPoint = GameObject.FindWithTag( "CustomerStoreEnterPoint" ).transform;
		endPoint = GameObject.FindWithTag( "CustomerEndPoint" ).transform;

		// logic component
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
		
		//logic field
		isFind = false;
		findObjectSet = new List<FurnitureObject>( );

		// data component
		ResetCustomerAgent();
	}


	// reset information
	public void ResetCustomerAgent()
	{
		moveAgent.ResetPath();
		moveAgent.enabled = false;
		transform.position = waitingPoint.position; 
		gold = Random.Range( 1000, 5000 );
		moveAgent.speed = Random.Range( 4f, 6f );
		presentSequence = Sequence.Ready;
		isFind = false;
	}

	// move start customer agent
	public void ActivateCustomerAgent( int customerIndex )
	{
		name = "손님" + ( customerIndex + 1 );
		SetBuyInformation();
		GoToStore();
	}

	// set favorite group & buy scale
	public void SetBuyInformation()
	{
		// buy scale
		if( Random.Range( 1, 101 ) >= stageManager.ProScale )
		{
			buyScale = stageManager.BuyScale;
		}
		else
		{
			int i = ( int ) stageManager.BuyScale;
			while( i != ( int ) stageManager.BuyScale )
			{
				i = Random.Range( 1, 6 );
			}
			buyScale = CustomerAgent.ReturnBuyScale( i );
		}

		// favorite group
		if( Random.Range( 1, 101 ) >= stageManager.ProFavor )
		{
			favoriteItemType = stageManager.FavoriteGroup;
		}
		else
		{
			int i = ( int ) stageManager.FavoriteGroup;
			while( i != ( int ) stageManager.FavoriteGroup )
			{
				i = Random.Range( 1, 8 );
			}
			favoriteItemType = ItemData.ReturnType( i );
		}
	}

	// go to store enter point
	public void GoToStore()
	{
		moveAgent.enabled = false;
		transform.position = startPoint.position;
		moveAgent.enabled = true;
		presentSequence = Sequence.GoToStore;
	}

	// use warp gate -> in store
	public void WarpStoreIn()
	{
		if( presentSequence == Sequence.EnterStore )
		{
			moveAgent.ResetPath();
			moveAgent.enabled = false;
			transform.position = storeInside.position;
			moveAgent.enabled = true;
			presentSequence = Sequence.SetTarget;
		}
	}

	public void SequenceProcessSearchTarget()
	{
		// search
		if( !isFind )
		{
			Collider[] tempGroup = Physics.OverlapBox( transform.position, new Vector3( storeManager.PlaneScale, storeManager.PlaneScale, storeManager.PlaneScale ), transform.rotation, 1 << LayerMask.NameToLayer( "Furniture" ) );
			
			for( int i = 0; i < tempGroup.Length; i++ )
			{
				findObjectSet.Add( tempGroup[ i ].GetComponent<FurnitureObject>() );
			}
			isFind = true;
		}
		
		if( findObjectSet.Count != 0 )
		{
			foreach( FurnitureObject element in findObjectSet )
			{
				if( element.InstanceData.Furniture.Function != FurnitureData.FunctionType.SellObject )
				{
					findObjectSet.Remove( element );
					break;
				}
				else
				{
					targetObject = element;
					presentSequence = Sequence.Buy;
					break;
				}
			}
		}
		else
		{
			presentSequence = Sequence.ExitStore;	
		}
	}

	public void SequenceProcessBuy()
	{				
		// check & buy
		if( targetObject != null )
		{
			moveAgent.SetDestination( targetObject.transform.position );	
		}
		
		if( isFind && ( findObjectSet.Count == 0 ) )		
		// no more buy -> next sequence
		presentSequence = Sequence.ExitStore;
	}

	// use warp gate -> out store
	public void WarpStoreOut()
	{
		if( presentSequence == Sequence.ExitStore )
		{
			moveAgent.ResetPath();
			moveAgent.enabled = false;
			transform.position = storeOutside.position;
			moveAgent.enabled = true;
			presentSequence = Sequence.GoToHome;
		}
	}

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
}
