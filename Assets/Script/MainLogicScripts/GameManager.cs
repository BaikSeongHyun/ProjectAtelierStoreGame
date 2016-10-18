using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// logic simple data field


	// game instance data field
	[SerializeField] PlayerData player;

	// network data field
	[SerializeField] PacketQueue receiveQueue;
	[SerializeField] PacketQueue sendQueue;

	// control logic field
	[SerializeField] AIManager aiManager;
	[SerializeField] CharacterManager characterManager;
	[SerializeField] FieldManager fieldManager;
	[SerializeField] NetworkController networkContoller;
	[SerializeField] StoreManager storeManager;
	[SerializeField] UIManager mainUI;

	// property
	public PlayerData GamePlayer { get { return player; } set { player = value; } }

	// unity mono behaviour method
	// awake
	void Awake()
	{
		LinkLogicElement();
		DataInitailize();	
	}

	// fixed update -> process network logic
	void FixedUpdate()
	{

	}

	// update -> process game logic
	void Update()
	{

	}

	// late update -> process camera logic
	void LateUpdate()
	{

	}


	// customize method (private & public)

	// private method
	// game manager data initialize
	private void DataInitailize()
	{
		// game instance data field
		player = new PlayerData();

		// network data field;
		receiveQueue = new PacketQueue();
		sendQueue = new PacketQueue();
	}

	// Link distribute game logic manager
	private void LinkLogicElement()
	{
		aiManager = GetComponent<AIManager>();
		characterManager = GetComponent<CharacterManager>();
		fieldManager = GetComponent<FieldManager>();
		networkContoller = GetComponent<NetworkController>();
		storeManager = GetComponent<StoreManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
	}

	// public method
	// game start -> for client test
	public void GameStart()
	{
		StartCoroutine( GameStartLoadingProcess() );
	}

	// check game data loading
	// if game data all loading (return true)
	public bool CheckGameDataLoading()
	{
	
		return true;
	}

	// coroutine section
	// game start loading Process
	IEnumerator GameStartLoadingProcess()
	{
		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
			{
				// set main ui state -> loading state
				yield return 1.0f;
			}
			// loading game data success -> start game
			else
			{
				// set main ui state -> store state
				yield break;
			}
		}
	}

	// loading store mode process
	IEnumerator StoreLoadingProcess()
	{
		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
			{
				// set main ui state -> loading state
				yield return 1.0f;
			}
			// loading game data success -> start game
			else
			{
				// set main ui state -> store state
				yield break;
			}
		}
	}

	// loading field mode process
	IEnumerator FieldLoadingProcess()
	{
		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
			{
				// set main ui state -> loading state
				yield return 1.0f;
			}
			// loading game data success -> start game
			else
			{
				// set main ui state -> store state
				yield break;
			}
		}
	}
}
