using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// game logic simple data field
	[SerializeField] GameMode presentGameMode;

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
	[SerializeField] CameraControl cameraControl;

	// enum state
	public enum GameMode : int
	{
		Start = 1,
		Loading = 2,
		Store = 3,
		StoreCustomizing = 4,
		Village = 5,
		Field = 6}

	;

	// property
	public PlayerData GamePlayer { get { return player; } set { player = value; } }

	public GameMode PresentMode { get { return presentGameMode; } }

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
		// main ui component update
		mainUI.UIUpdate();

		switch( presentGameMode )
		{
			case GameMode.StoreCustomizing:
				storeManager.CustomzingFurnutureObject();
				break;
			case GameMode.Field:
				fieldManager.FieldProcess();
				break;
		}
	}

	// late update -> process camera logic
	void LateUpdate()
	{
		cameraControl.SetCameraPosition();
	}


	// customize method (private & public)

	// private method
	// Link distribute game logic manager
	private void LinkLogicElement()
	{
		aiManager = GetComponent<AIManager>();
		characterManager = GetComponent<CharacterManager>();
		fieldManager = GetComponent<FieldManager>();
		networkContoller = GetComponent<NetworkController>();
		storeManager = GetComponent<StoreManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		cameraControl = Camera.main.gameObject.GetComponent<CameraControl>();
	}

	// game manager data initialize
	private void DataInitailize()
	{
		// game instance data field
		// player = new PlayerData();

		// network data field;
		receiveQueue = new PacketQueue();
		sendQueue = new PacketQueue();

		// set mainUI
		presentGameMode = GameMode.Start;
		mainUI.UIModeChange();
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
		Debug.Log( "Game data loading process" );
		return storeManager.CreateStoreObject();
	}

	// go to field
	public bool CheckFieldDataLoading()
	{
		Debug.Log( "Field data loading process" );
		return fieldManager.CreateField();
	}

	// set ui -> use present game mode
	public void SetUI()
	{
		mainUI.UIModeChange();
	}

	// start store customizing modwe
	public void SetCutomizeingMode()
	{
		presentGameMode = GameMode.StoreCustomizing;
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
				presentGameMode = GameMode.Store;
				mainUI.UIModeChange();
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
