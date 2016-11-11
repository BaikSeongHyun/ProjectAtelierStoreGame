using UnityEngine;
using System.Collections;

public class CustomerAgent : AIAgent
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] AIManager aiManager;

	// field - data
	[SerializeField] int gold;

	// field - logic
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] ItemInstance targetItem;
	[SerializeField] Transform startPoint;
	[SerializeField] Transform storeEnterPoint;
	[SerializeField] Transform storeInside;
	[SerializeField] Transform storeOutside;
	[SerializeField] Transform endPoint;
	[SerializeField] Sequence presentSequence;

	// enum state
	public enum Sequence : int
	{
		Ready = 1,
		MoveStoreEnter = 2,
		GoToStore = 3,
		Buy = 4,
		GoToHome = 5}

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
				// set information
				break;
			case Sequence.MoveStoreEnter:
				moveAgent.SetDestination( storeEnterPoint.position );
				break;
			case Sequence.GoToStore:				
				break;
			case Sequence.Buy:
				SequenceProcessBuy();
				break;
			case Sequence.GoToHome:
				SequenceProcessGoToHome();
				break;
		}
	}

	// public method
	// override -> data initialize
	public override void DataInitialize()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		aiManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<AIManager>();
		startPoint = GameObject.FindWithTag( "CustomerStartPoint" ).transform;
		storeEnterPoint = GameObject.FindWithTag( "CustomerStoreEnterPoint" ).transform;
		endPoint = GameObject.FindWithTag( "CustomerEndPoint" ).transform;
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
		gold = Random.Range( 100, 550 );
		moveAgent.speed = Random.Range( 3.5f, 4.5f );
		presentSequence = Sequence.Ready;
	}

	// override -> ai agent policy
	public override void AIAgentBehaviour()
	{

	}

	// set buy target
	public void CheckStoreItem()
	{
		targetObject = manager.GamePlayer.CheckSellItem();
		// item does not exist
		if( targetObject == null )
		{
			presentSequence = Sequence.GoToHome;
			return;
		}

		//
		targetItem = targetObject.SellItem;
		if( presentSequence != Sequence.Buy )
			presentSequence = Sequence.GoToStore;

	}

	public void MoveStart()
	{
		presentSequence = Sequence.MoveStoreEnter;
		moveAgent.SetDestination( storeEnterPoint.position );
	}

	// go to store enter point
	public void GoToStore()
	{
		moveAgent.ResetPath();
		transform.position = storeInside.position;
		presentSequence = Sequence.Buy;
	}

	// use warp gate -> in store
	public void WarpInStore()
	{
		moveAgent.ResetPath();
		transform.position = storeInside.position;
		presentSequence = Sequence.Buy;
	}

	// use warp gate -> out store
	public void WarpOutStore()
	{
		moveAgent.ResetPath();
		transform.position = storeOutside.position;
		presentSequence = Sequence.GoToHome;
	}

	// buy item instance -> check gold and buy item
	public void BuyItemInstance( ItemInstance data )
	{
		int buyLimit = ( int ) ( gold / data.SellPrice );
		manager.GamePlayer.Gold += gold;
		gold = 0;
		data.Count = 0;
		presentSequence = Sequence.GoToHome;
	}

	public void SequenceProcessBuy()
	{
		moveAgent.SetDestination( targetObject.transform.position );
	}

	// go to home
	public void SequenceProcessGoToHome()
	{
		// inside house
		// move outside

		// outside house
		moveAgent.SetDestination( endPoint.position );
	}


	// reset information
	public void ResetCustomerAgent()
	{
		moveAgent.ResetPath();
		transform.position = startPoint.position; 
		gold = Random.Range( 1000, 5000 );
		moveAgent.speed = Random.Range( 3f, 4f );
		presentSequence = Sequence.Ready;
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
