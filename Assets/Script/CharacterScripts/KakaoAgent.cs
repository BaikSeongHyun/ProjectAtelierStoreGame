using UnityEngine;
using System.Collections;

public class KakaoAgent : AIAgent
{
	// high structure
	[SerializeField] CharacterManager charManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] UIManager mainUI;

	// field - effect
	[SerializeField] GameObject runEffect;

	// field - move points
	[SerializeField] Transform moveTarget;
	[SerializeField] Transform[] worldBoundary;
	[SerializeField] Transform storeDoor;
	[SerializeField] Transform waitingPoint;

	// field - logic
	[SerializeField] AnimatorStateInfo aniInfo;
	[SerializeField] bool sendMessage;
	[SerializeField] bool wait;
	[SerializeField] Sequence presentSequence;

	public enum Sequence : int
	{
		Ready = 1,
		GoToStore = 2,
		SendMessage = 3,
		GoToOffice = 4}

	;
	 

	// unity method
	// awake
	void Awake()
	{
		DataInitialize();
		GoToOffice();
	}

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
			case Sequence.SendMessage:
				moveAgent.ResetPath();
				break;
			case Sequence.GoToOffice:
				moveAgent.SetDestination( moveTarget.position );
				break;
		}

		aniInfo = agentAnimator.GetCurrentAnimatorStateInfo( 0 );

		if( aniInfo.IsName( "Walking" ) )
			runEffect.SetActive( true );
		else
			runEffect.SetActive( false );
	}

	//
	void OnTriggerEnter( Collider col )
	{
		if( col.gameObject.name == "StoreDoor" )
		{
			agentAnimator.SetTrigger( "SendMessage" );
			presentSequence = Sequence.SendMessage;
		}
		else if( ( col.gameObject.layer == LayerMask.NameToLayer( "WorldBoundary" ) ) && ( presentSequence != Sequence.GoToStore ) )
		{
			GoToOffice();
		}
	}


	// public method
	public override void DataInitialize()
	{
		// high structure
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
		charManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// logic component
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();

		// set points
		GameObject[] temp = GameObject.FindGameObjectsWithTag( "WorldBoundary" );
		worldBoundary = new Transform[temp.Length];
		for( int i = 0; i < worldBoundary.Length; i++ )
		{
			worldBoundary[ i ] = temp[ i ].transform;
		}	
		waitingPoint = GameObject.FindWithTag( "CustomerWaitingPoint" ).transform;

		runEffect = transform.Find( "RunEffect" ).gameObject;
	}

	public void GoToStore()
	{		
		storeDoor = GameObject.Find( "StoreDoor" ).transform;
		presentSequence = Sequence.GoToStore;
		moveTarget = storeDoor;
		transform.position = worldBoundary[ 0 ].position;
		moveAgent.enabled = true;
	}

	public void GoToOffice()
	{	
		moveAgent.enabled = true;	
		moveAgent.ResetPath();
		moveAgent.enabled = false;
		moveTarget = null;
		transform.position = waitingPoint.position;
		presentSequence = Sequence.Ready;
	}

	public void SendEndEvent()
	{
		presentSequence = Sequence.GoToOffice;
		moveTarget = worldBoundary[ UnityEngine.Random.Range( 0, worldBoundary.Length ) ];

		// ui on!
		mainUI.ChatSceneUI.SetActive( true );
		mainUI.ChatSceneUILogic.SetKakaoInformation();
	}



}