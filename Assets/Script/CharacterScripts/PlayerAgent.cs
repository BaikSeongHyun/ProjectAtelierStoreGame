using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerAgent : AIAgent
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] CharacterManager charManager;
    [SerializeField] SoundManager soundManager;
	[SerializeField] UIManager mainUI;

	//
	[SerializeField] FurnitureObject targetObject;
	[SerializeField] AnimatorStateInfo aniInfor;
	[SerializeField] float frame;
	[SerializeField] Vector3 targetPosition;


	[SerializeField] Sequence presentSequence;

	// enum
	// player agent logic sequence
	public enum Sequence : int
	{
		WaitAnimation = 0,
		Ready = 1,
		MoveTarget = 2,
		MovePoint = 3,
		GoToCreate = 4,
		GoToSetting = 5}

	;

	// property

	// unity method
	// awake
	void Awake()
	{
		DataInitialize();
	}

	void Update()
	{
		switch( presentSequence )
		{
			case Sequence.WaitAnimation:
				moveAgent.ResetPath();
				break;
			case Sequence.Ready:
				moveAgent.ResetPath();
				agentAnimator.SetInteger( "State", 0 );
				break;
			case Sequence.MovePoint:
				if( ( transform.position - targetPosition ).magnitude <= 0.1 )
					presentSequence = Sequence.Ready;
				moveAgent.SetDestination( targetPosition );
				agentAnimator.SetInteger( "State", 1 );
				break;
			case Sequence.MoveTarget:
				moveAgent.SetDestination( targetObject.transform.position );
				agentAnimator.SetInteger( "State", 1 );
				break;				
			case Sequence.GoToCreate:
				moveAgent.SetDestination( targetObject.transform.position );
				agentAnimator.SetInteger( "State", 1 );
				break;
			case Sequence.GoToSetting:
				moveAgent.SetDestination( targetObject.transform.position );
				agentAnimator.SetInteger( "State", 1 );
				break;
		}
	}

	// on trigger enter -> player policy
	void OnTriggerEnter( Collider col )
	{
		if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToCreate ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
			mainUI.CreateUI.SetActive( true );
			mainUI.CreateUILogic.SetComponentElement();
		}
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToSetting ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
			mainUI.ActivateSellItemSettingUI();			
		}
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.MoveTarget ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
		}
	}

	// on trigger enter -> check stay move
	void OnTriggerStay( Collider col )
	{
		if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToCreate ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
			mainUI.CreateUI.SetActive( true );
			mainUI.CreateUILogic.SetComponentElement();
		}
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToSetting ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
			mainUI.ActivateSellItemSettingUI();
		}
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.MoveTarget ) )
		{
			moveAgent.ResetPath();
			presentSequence = Sequence.Ready;
			targetObject = null;
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
		charManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();
        soundManager = GameObject.FindWithTag("GameLogic").GetComponent<SoundManager>();
        mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// agent data
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
	}

	// set move point
	public void SetMovePoint( Vector3 position )
	{
		targetPosition = position;
		presentSequence = Sequence.MovePoint;
	}

	// move to furniture object
	public void SetFurnitureObjectPoint()
	{
		if( manager.PresentMode == GameManager.GameMode.Store )
			targetObject = storeManager.PresentSelectedFurniture;
		else if( manager.PresentMode == GameManager.GameMode.Store )
			targetObject = stageManager.PresentSelectedFurniture;

		presentSequence = Sequence.MoveTarget;
	}

	// set create form
	public void SetCreateMode()
	{
		presentSequence = Sequence.GoToCreate;
		targetObject = storeManager.PresentSelectedFurniture;
    }

	// item create
	public void ItemCreate()
	{		
		agentAnimator.SetTrigger( "Crafting" );
		presentSequence = Sequence.WaitAnimation;
        soundManager.PlayUISoundPlayer(6);
    }

	// set item setting mode
	public void SetItemSettingMode()
	{
		presentSequence = Sequence.GoToSetting;
		targetObject = stageManager.PresentSelectedFurniture;

	}

	// item setting
	public void ItemSetting()
	{
		agentAnimator.SetTrigger( "Setting" );
		presentSequence = Sequence.WaitAnimation;
        soundManager.PlayUISoundPlayer(4);
    }

	public void CreateEndEvent()
	{
		storeManager.CreateItemConfirm();
		presentSequence = Sequence.Ready;
        soundManager.PlayUISoundPlayer(6);
        // ui set (pop up)
    }

	public void SetEndEvent()
	{
		// set items
		
		presentSequence = Sequence.Ready;
	}
}