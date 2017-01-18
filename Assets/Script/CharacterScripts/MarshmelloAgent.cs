using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MarshmelloAgent : AIAgent
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] CharacterManager charManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] UIManager mainUI;

	// field - status
	[SerializeField] int presentClick;
	[SerializeField] int clickRequire;
	[SerializeField] bool rest;

	// field - effect
	[SerializeField] GameObject runEffect;
	[SerializeField] GameObject throwEffect;

	// field - move points
	[SerializeField] Transform moveTarget;
	[SerializeField] Transform[] worldBoundary;
	[SerializeField] Transform storeDoor;
	[SerializeField] Transform waitingPoint;

	// field target object
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] int targetIndex;
	[SerializeField] ItemObject throwItemObject;
	[SerializeField] int throwItemIndex;
	[SerializeField] bool throwFirst;
	[SerializeField] int throwCount;

	// field - logic
	[SerializeField] AnimatorStateInfo AnimatorClipInfo;
	[SerializeField] Sequence presentSequence;

	// property
	public bool RestMarshmello { get { return rest; } }

	public Sequence PresentSequence { get { return presentSequence; } }

	public int PresentClick
	{
		get { return presentClick; }
		set
		{
			presentClick = value;
			CheckClick();
		}
	}

	public float presentClickFill { get { return ( float ) (clickRequire - presentClick) / ( float ) (clickRequire); } }

	// agent sequence
	public enum Sequence : int
	{
		Ready = 1,
		GoToStore = 2,
		GoToItems = 3,
		ThrowItems = 4,
		GoToHome = 5,
		WaitAnimation = 6}

	;

	// unity method
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
				moveAgent.SetDestination( moveTarget.position );
				break;
			case Sequence.GoToItems:
				moveAgent.SetDestination( moveTarget.position );
				break;
			case Sequence.ThrowItems:
				moveAgent.ResetPath();
				break;
			case Sequence.GoToHome:
				moveAgent.SetDestination( moveTarget.position );			
				break;
			case Sequence.WaitAnimation:
				moveAgent.ResetPath();
				break;
		}
	}

	// target policy - enter
	void OnTriggerEnter( Collider col )
	{
		if( (col.gameObject.name == "StoreDoor") && (presentSequence == Sequence.GoToStore) )
		{
			SearchItemTarget(); 
		}
		else if( (col.gameObject.layer == LayerMask.NameToLayer( "WorldBoundary" )) && (presentSequence != Sequence.GoToStore) )
		{
			GoToHome();
		}
	}

	// target policy - stay
	void OnTriggerStay( Collider col )
	{
		if( (targetObject != null) && (col.gameObject == targetObject.gameObject) && (presentSequence == Sequence.GoToItems) )
		{
			// item move field
			stageManager.ThrowSellItem( targetIndex, throwItemIndex );

			if( !throwFirst )
			{
				agentAnimator.SetTrigger( "FirstThrow" );
				throwFirst = true;
			}
			else
			{
				agentAnimator.SetTrigger( "Throw" );
			}

			// set state
			presentSequence = Sequence.ThrowItems;
		}
	}

	// public method
	// data set
	public override void DataInitialize()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
		charManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// logic component
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();

		// set move points
		GameObject[] temp = GameObject.FindGameObjectsWithTag( "WorldBoundary" );
		worldBoundary = new Transform[temp.Length];
		for( int i = 0; i < worldBoundary.Length; i++ )
		{
			worldBoundary[ i ] = temp[ i ].transform;
		}	
		waitingPoint = GameObject.FindWithTag( "CustomerWaitingPoint" ).transform;

		// set effect object

		// set start sequence
		presentSequence = Sequence.Ready;

		// data reset
		ResetTargetData();
		ResetLoopData();

		// position reset
		GoToHome();
	}

	// pinky go home (ready point)
	public void GoToHome()
	{
		moveAgent.enabled = true;	
		moveAgent.ResetPath();
		moveAgent.enabled = false;
		moveTarget = null;
		transform.position = waitingPoint.position;
		presentSequence = Sequence.Ready;
		ResetTargetData();
	}

	// pinky go store
	public void GoToStore()
	{
		storeDoor = GameObject.Find( "StoreDoor" ).transform;
		presentSequence = Sequence.GoToStore;
		moveTarget = storeDoor;
		transform.position = worldBoundary[ 0 ].position;
		moveAgent.enabled = true;
		throwCount = manager.GamePlayer.StoreData.StoreStep;
	}

	// search throw itme target
	public bool SearchItemTarget()
	{		
		// target initialize
		targetObject = null;
				
		for( int i = 0; i < stageManager.SellFurnitureSet.Count; i++ )
		{
			for( int j = 0; j < stageManager.SellFurnitureSet[ i ].SellItem.Length; j++ )
			{
				try
				{
					// find item & go to items
					if( (stageManager.SellFurnitureSet[ i ].SellItem[ j ] != null) && (stageManager.SellFurnitureSet[ i ].SellItem[ j ].Item.ID != 0) )
					{
						// set target object
						targetObject = stageManager.SellFurnitureSet[ i ];
						targetIndex = i;

						// set throw item
						throwItemObject = stageManager.SellFurnitureSet[ i ].SellItemObject[ j ];
						throwItemIndex = j;

						// set destination
						moveTarget = targetObject.transform;

						// confirm go to item
						presentSequence = Sequence.GoToItems;
						return true;
					}
				}
				catch( NullReferenceException e )
				{
					// item slot empty
				}
			}
		}

		// no items -> go home
		if( presentSequence != Sequence.ThrowItems )
		{
			// set destination
			moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, 2 ) ];

			// no more item in store
			presentSequence = Sequence.GoToHome;
		}
		return false;
	}

	// set click information
	public void CheckClick()
	{
		if( presentClick >= clickRequire )
		{
			rest = true;
			GoToHome();
		}
	}

	// reset target data
	public void ResetTargetData()
	{
		targetObject = null;
		targetIndex = 0;
		throwItemObject = null;
		throwItemIndex = 0;
		throwFirst = false;
	}

	// reset loop data
	public void ResetLoopData()
	{
		rest = false;
		presentClick = 0;
		clickRequire = manager.GamePlayer.StoreData.StoreStep * 10;
	}


	// animatoer event method
	// throw itmes
	public void ThrowItemForcedEvent()
	{
		try
		{
			throwItemObject.ThrowItem();
			throwCount--;
		}
		catch( NullReferenceException e )
		{
			// item has fallen
		}
		catch( MissingReferenceException e )
		{
			// item has fallen
		}		
	}

	// item throw end
	public void ThrowEndEvent()
	{
		// search target
		if( presentSequence != Sequence.Ready )
			SearchItemTarget();
		
		if( throwCount == 0 )
		{
			moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, 2 ) ];
			presentSequence = Sequence.GoToHome;
		}
		
		if( targetObject == null )
		{
			moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, 2 ) ];
			presentSequence = Sequence.GoToHome;	
		}
	}
}
