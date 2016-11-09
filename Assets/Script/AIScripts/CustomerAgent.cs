using UnityEngine;
using System.Collections;

public class CustomerAgent : AIAgent
{
	// high structure
	[SerializeField] AIManager aiManager;

	// field - data
	[SerializeField] int gold;

	// field - logic
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] ItemInstance targetItem;
	[SerializeField] PlayerData data;
	[SerializeField] Vector3 houseEnter;
	[SerializeField] Vector3 destroyPoint;
	[SerializeField] Sequence present;

	// enum state
	private enum Sequence : int
	{
		Ready = 1,
		MoveHouseEnter = 2,
		SetBuyTarget = 3,
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

	// unity standard method
	// awake
	void Awake()
	{
		DataInitialize();	
	}

	// update
	void Update()
	{
		switch( present )
		{
			case Sequence.Ready:
				// set information
				break;
			case Sequence.MoveHouseEnter:
				moveAgent.SetDestination( houseEnter );
				break;
			case Sequence.SetBuyTarget:
				
				break;
			case Sequence.Buy:
				
				break;
			case Sequence.GoToHome:
				moveAgent.SetDestination( destroyPoint );
				break;

		}
	}

	// public method
	// override -> data initialize
	public override void DataInitialize()
	{
		data = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>().GamePlayer;
		destroyPoint = GameObject.FindWithTag( "CustomerDestroyPoint" ).transform.position;
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
	}

	// override -> ai agent policy
	public override void AIAgentBehaviour()
	{
		FurnitureObject temp = data.CheckSellItem();

		if( temp != null )
		{

		}
		else
		{
			present = Sequence.GoToHome;
		}
	}

	// set item target
	public void SetBuyItemTarget()
	{
		
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
