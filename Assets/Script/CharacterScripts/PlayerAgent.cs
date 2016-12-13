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
	[SerializeField] UIManager mainUI;

	[SerializeField] FurnitureObject targetObject;
	[SerializeField] AnimatorStateInfo aniInfor;
	[SerializeField] float frame;

	[SerializeField] Sequence presentSequence;

	// enum
	// player agent logic sequence
	public enum Sequence : int
	{
		WaitAnimation = 0,
		Ready = 1,
		GoToCreate = 2,
		GoToSet = 3}

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
			case Sequence.GoToCreate:
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
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToSet ) )
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
		else if( ( targetObject != null ) && ( col.gameObject == targetObject.gameObject ) && ( presentSequence == Sequence.GoToSet ) )
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
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// agent data
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
	}

	// set create form
	public void SetCreateMode()
	{
		presentSequence = Sequence.GoToCreate;
		targetObject = storeManager.PresentSelectedObject;
	}

	// item create
	public void ItemCreate()
	{		
		agentAnimator.SetTrigger( "Crafting" );
	}


	public void CreateEndEvent()
	{
		storeManager.CreateItemConfirm();

		// ui set
	}

	public void SetEndEvent()
	{

	}
}